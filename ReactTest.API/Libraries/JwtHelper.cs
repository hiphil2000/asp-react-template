﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Web.Configuration;
using IMSWeb.Controllers.Database;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Exceptions;
using JWT.Serializers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using ReactTest.API.Models;
using ReactTest.API.Models.Auth;

namespace ReactTest.API.Libraries
{
	/// <summary>
	/// JWT 관련 기능의 Helper Class입니다.
	/// </summary>
	public static class JwtHelper
	{
		#region Constants

		public static class Constants
		{
			public const string AccessToken = "access_token";
			public const string RefreshToken = "refresh_token";
		}

		#endregion
		
		#region Properties

		/// <summary>
		/// AccessToken의 만료 일자입니다.
		/// </summary>
		private static DateTime AccessTokenExpiryMin
		{
			get
			{
				var expiry = WebConfigurationManager.AppSettings["AccessTokenExpiryTimeMin"];
				return DateTime.Now.AddMinutes(int.Parse(expiry));
			}
		}

		/// <summary>
		/// RefreshToken의 만료 일자입니다.
		/// </summary>
		private static DateTime RefreshTokenExpiryMin
		{
			get
			{
				var expiry = WebConfigurationManager.AppSettings["RefreshTokenExpiryTimeMin"];
				return DateTime.Now.AddMinutes(int.Parse(expiry));
			}
		}

		/// <summary>
		/// Jwt 암호화 Secret을 얻습니다.
		/// appsettings > JwtSecret
		/// </summary>
		private static string Secret => WebConfigurationManager.AppSettings["JwtSecret"];

		/// <summary>
		/// Jwt 암호화 알고리즘을 얻습니다.
		/// appsettings > JwtAlgorithm
		/// </summary>
		private static string Algorithm => WebConfigurationManager.AppSettings["JwtAlgorithm"].ToLower();

		#endregion

		#region Public Methods

		#region 토큰 발행 및 해독

		/// <summary>
		/// AccessToken을 생성합니다.
		/// </summary>
		/// <param name="userNo">대상 사용자 고유 번호</param>
		/// <returns></returns>
		public static string CreateAccessToken(long userNo)
		{
			var token = CreateTokenBase(userNo)
				.ExpirationTime(AccessTokenExpiryMin)
				.Subject(Constants.AccessToken)
				.Encode();

			RegisterToken(token);

			return token;
		}

		/// <summary>
		/// RefreshToken을 생성합니다.
		/// </summary>
		/// <param name="userNo">대상 사용자 고유 번호</param>
		/// <returns></returns>
		public static string CreateRefreshToken(long userNo)
		{
			var token = CreateTokenBase(userNo)
				.ExpirationTime(RefreshTokenExpiryMin)
				.Subject(Constants.RefreshToken)
				.Encode();

			RegisterToken(token);

			return token;
		}
		
		/// <summary>
		/// 토큰을 해독합니다.
		/// </summary>
		/// <param name="token">토큰</param>
		/// <returns></returns>
		public static JwtPayload DecodeToken(string token)
		{
			try
			{
				var payload = JwtBuilder.Create()
					.WithAlgorithm(GetAlgorithm(Secret))
					.WithSecret(Secret)
					.MustVerifySignature()
					.Decode<Dictionary<string, object>>(token);
				
				return new JwtPayload(payload);
			}
			catch (TokenExpiredException e)
			{
				return null;
			}

		}

		#endregion

		#region 토큰 쿠키 생성

		/// <summary>
		/// 토큰의 쿠키 값를 생성합니다.
		/// </summary>
		/// <param name="type">토큰 타입(JwtHelper.Constants)</param>
		/// <param name="token">토큰 값</param>
		/// <returns></returns>
		public static CookieHeaderValue CreateTokenCookie(string type, string token)
		{
			var expiry = DecodeToken(token).ExpirationTime;
			
			return new CookieHeaderValue(type, token)
			{
				Path = "/",
				Expires = expiry,
				HttpOnly = true,
				Secure = true
			};
		}

		public static CookieHeaderValue[] CreateTokenCookies(string accessToken, string refreshToken)
		{
			return new[]
			{
				CreateTokenCookie(Constants.AccessToken, accessToken),
				CreateTokenCookie(Constants.RefreshToken, refreshToken)
			};
		}

		#endregion

		#region Database

		/// <summary>
		/// DB에 토큰을 등록합니다.
		/// </summary>
		/// <param name="payload">대상 토큰 Payload</param>
		/// <param name="signature">대상 토큰 Signature</param>
		public static void RegisterToken(JwtPayload payload, string signature)
		{
			using (var db = new SqlService())
			{
				db.AddParameter("@UserNo", DbType.Int32, payload.Issuer);
				db.AddParameter("@TokenId", DbType.String, payload.JwtId.ToString());
				db.AddParameter("@Subject", DbType.String, payload.Subject);
				db.AddParameter("@IssuedAt", DbType.DateTime2, payload.IssuedAt);
				db.AddParameter("@ExpirationAt", DbType.DateTime2, payload.ExpirationTime);
				db.AddParameter("@Signature", DbType.String, signature);

				_ = db.ExecuteQueryDataSet("uSP_AddToken", commandType: CommandType.StoredProcedure);
			}
		}

		/// <summary>
		/// DB에 해당 토큰을 사용안함 처리합니다.
		/// </summary>
		/// <param name="token">대상 토큰</param>
		public static void DestroyToken(string token)
		{
			var decoded = DecodeToken(token);

			using (var db = new SqlService())
			{
				db.AddParameter("@TokenId", DbType.String, decoded.JwtId.ToString());

				_ = db.ExecuteQuery("uSP_RemoveToken", commandType: CommandType.StoredProcedure);
			}
		}

		/// <summary>
		/// DB에 해당 토큰이 사용중인지 여부를 확인합니다.
		/// </summary>
		/// <param name="tokenId"></param>
		/// <returns></returns>
		public static bool IsUsingToken(string tokenId)
		{
			using (var db = new SqlService())
			{
				db.AddParameter("@TokenId", DbType.String, tokenId);

				var result = db.ExecuteQueryDataSet(
					"uSP_GetToken", commandType: CommandType.StoredProcedure);

				if (result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
				{
					return !result.Tables[0].Rows[0].GetDbBool("DestroyYn");
				}

				return false;
			}
		}

		#endregion

		#endregion

		#region Private Functions

		/// <summary>
		/// 기본 토큰 형태를 생성합니다.
		/// </summary>
		/// <param name="userNo">대상 사용자 고유 번호</param>
		/// <returns></returns>
		private static JwtBuilder CreateTokenBase(long userNo)
		{
			var secret = Secret;

			return JwtBuilder.Create()
				.WithAlgorithm(GetAlgorithm(secret))
				.WithSecret(secret)
				.IssuedAt(DateTime.Now)
				.Issuer(userNo.ToString())
				.Id(Guid.NewGuid());
		}
		
		/// <summary>
		/// Jwt 암호화 알고리즘을 얻습니다.
		/// </summary>
		/// <param name="key">RSA 알고리즘에 사용될 키</param>
		/// <returns></returns>
		private static IJwtAlgorithm GetAlgorithm(string key)
		{
			switch (Algorithm)
			{
				case "sha256":
					return new HMACSHA256Algorithm();
				case "sha384":
					return new HMACSHA384Algorithm();
				case "sha512":
					return new HMACSHA512Algorithm();
				case "rs256":
					return new RS256Algorithm(RSA.Create());
				case "rs384":
					return new RS384Algorithm(RSA.Create());
				case "rs512":
					return new RS512Algorithm(RSA.Create());
				case "rs1024":
					return new RS1024Algorithm(RSA.Create());
				default:
					return new NoneAlgorithm();
			}
		}

		/// <summary>
		/// DB에 토큰을 등록합니다.
		/// </summary>
		/// <param name="token">대상 토큰 문자열</param>
		private static void RegisterToken(string token)
		{
			var decoded = DecodeToken(token);
			RegisterToken(decoded, token.Split('.')[2]);
		}

		#endregion
	}
}