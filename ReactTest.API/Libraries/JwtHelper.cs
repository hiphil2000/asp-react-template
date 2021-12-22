using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Web.Configuration;
using JWT.Algorithms;
using JWT.Builder;
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
		/// <summary>
		/// 사용자 토큰을 생성합니다.
		/// </summary>
		/// <returns></returns>
		public static string CreateToken(UserModel userModel)
		{
			var token = JwtBuilder.Create()
				.WithAlgorithm(GetAlgorithm(GetSecret()))
				.WithSecret(GetSecret())
				.ExpirationTime(DateTimeOffset.UtcNow.AddMinutes(5).ToUnixTimeSeconds())
				.AddClaim(JwtClaims.Role, userModel.Role.ToString())
				.AddClaim(JwtClaims.UserNo, userModel.UserNo)
				.Encode();

			return token;
		}

		/// <summary>
		/// 토큰을 해독합니다.
		/// </summary>
		/// <param name="token">토큰</param>
		/// <returns></returns>
		public static JwtPayload DecodeToken(string token)
		{
			var payload = JwtBuilder.Create()
				.WithAlgorithm(GetAlgorithm(GetSecret()))
				.WithSecret(GetSecret())
				.MustVerifySignature()
				.Decode<Dictionary<string, object>>(token);

			return new JwtPayload(payload);
		}

		/// <summary>
		/// Jwt 암호화 Secret을 얻습니다.
		/// </summary>
		/// <returns></returns>
		private static string GetSecret()
		{
			// appsettings > JwtSecret
			return WebConfigurationManager.AppSettings["JwtSecret"];
		}

		/// <summary>
		/// Jwt 암호화 알고리즘을 얻습니다.
		/// </summary>
		/// <param name="key">RSA 알고리즘에 사용될 키</param>
		/// <returns></returns>
		private static IJwtAlgorithm GetAlgorithm(string key)
		{
			// appsettings > JwtAlgorithm
			var algorithm = WebConfigurationManager.AppSettings["JwtAlgorithm"].ToLower();
			switch (algorithm)
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
	}
}