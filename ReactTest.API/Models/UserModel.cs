using System.Runtime.Serialization;
using ReactTest.API.Models.Enums;

namespace ReactTest.API.Models
{
	[DataContract]
	public class UserModel
	{
		/// <summary>
		/// 사용자의 고유 번호입니다.
		/// </summary>
		[DataMember] public long UserNo { get; set; }
		
		/// <summary>
		/// 사용자의 로그인 ID입니다.
		/// </summary>
		[DataMember] public string UserId { get; set; }
		
		/// <summary>
		/// 사용자의 로그인 비밀번호입니다.
		/// </summary>
		public string UserPassword { get; set; }
		
		/// <summary>
		/// 사용자의 이름입니다.
		/// </summary>
		[DataMember] public string UserName { get; set; }

		/// <summary>
		/// 사용자의 역할입니다.
		/// </summary>
		[DataMember] public Role Role { get; set; }
	}
}