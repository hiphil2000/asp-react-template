using System;
using System.Collections.Generic;
using ReactTest.API.Libraries;

namespace ReactTest.API.Models.Auth
{
	public class JwtPayload
	{
		public long UserNo { get; set; }
		
		public Role Role { get; set; }

		public JwtPayload(Dictionary<string, object> payload)
		{
			Enum.TryParse<Role>((string)payload[JwtClaims.Role], out var role);
			
			UserNo = (long)payload[JwtClaims.UserNo];
			Role = role;
		}
	}
}