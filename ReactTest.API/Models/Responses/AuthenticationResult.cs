using System.Runtime.Serialization;

namespace ReactTest.API.Models.Responses
{
	/// <summary>
	/// 인증 결과 모델입니다.
	/// </summary>
	[DataContract]
	public class AuthenticationResult
	{
		[DataMember] public bool Success { get; set; }
		[DataMember] public string Token { get; set; }
		[DataMember] public string Message { get; set; }
	}
}