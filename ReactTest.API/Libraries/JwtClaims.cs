namespace ReactTest.API.Libraries
{
	public class JwtClaims
	{
		private const string PublicClaimBase = "https://react-test.com/jwt_claims/";
		
		#region Public Claims

		public const string Role = PublicClaimBase + "role";
		public const string UserNo = PublicClaimBase + "user_no";

		#endregion
	}
}