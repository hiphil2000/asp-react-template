using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using JWT.Algorithms;
using JWT.Builder;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ReactTest.API.Libraries
{
	public class JwtHelper
	{
		public static string CreateToken()
		{
			var token = JwtBuilder.Create()
				.WithAlgorithm(GetAlgorithm(GetSecret()))
				.WithSecret(GetSecret())
				.ExpirationTime(DateTimeOffset.UtcNow.AddMinutes(5).ToUnixTimeSeconds())
				.AddClaim("role", "test")
				.MustVerifySignature()
				.Encode();

			return token;
		}

		public static T DecodeToken<T>(string token)
		{
			var payload = JwtBuilder.Create()
				.WithAlgorithm(GetAlgorithm(GetSecret()))
				.WithSecret(GetSecret())
				.MustVerifySignature()
				.Decode<T>(token);

			return payload;
		}

		private static string GetSecret()
		{
			return WebConfigurationManager.AppSettings["JwtSecret"];
		}

		private static IJwtAlgorithm GetAlgorithm(string key)
		{
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