using System;
using System.Collections.Generic;
using ReactTest.API.Libraries;
using ReactTest.API.Models.Enums;

namespace ReactTest.API.Models.Auth
{
	public class JwtPayload
	{
		public long Issuer { get; set; }
		
		public DateTime ExpirationTime { get; set; }
		
		public DateTime IssuedAt { get; set; }
		
		public Guid JwtId { get; set; }
		
		public string Subject { get; set; }
		
		public Role Role { get; set; }

		public JwtPayload()
		{
		}

		public JwtPayload(Dictionary<string, object> payload)
		{
			Issuer = long.Parse(payload[JwtClaims.Issuer].ToString());
			IssuedAt = ParseDateTime(long.Parse(payload[JwtClaims.IssuedAt].ToString())).ToLocalTime();
			ExpirationTime = ParseDateTime(long.Parse(payload[JwtClaims.ExpirationTime].ToString())).ToLocalTime();
			JwtId = Guid.Parse(payload[JwtClaims.JwtId].ToString());
			Subject = payload[JwtClaims.Subject].ToString();
		}

		private static DateTime ParseDateTime(long javaTimestamp)
		{
			var datetime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			return datetime.AddSeconds(javaTimestamp);
		}
	}
}