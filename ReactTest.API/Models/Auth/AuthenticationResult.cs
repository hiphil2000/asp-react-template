using System.Runtime.Serialization;
using ReactTest.API.Models.Auth;

namespace ReactTest.API.Models.Responses
{
	/// <summary>
	/// 인증 결과 모델입니다.
	/// </summary>
	[DataContract]
	public class AuthenticationResult
	{
		/// <summary>
		/// 성공 여부입니다.
		/// </summary>
		[DataMember] public bool Success { get; set; }
		
		/// <summary>
		/// 발행된 토큰 상세입니다.
		/// </summary>
		[DataMember] public TokenModel Token { get; set; }
		
		/// <summary>
		/// 로그인 유저의 기본 정보입니다.
		/// </summary>
		[DataMember] public UserModel User { get; set; }
		
		/// <summary>
		/// 에러 메시지입니다.
		/// </summary>
		[DataMember] public string Message { get; set; }
	}
}