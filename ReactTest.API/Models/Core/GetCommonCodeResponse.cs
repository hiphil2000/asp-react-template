using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ReactTest.API.Models.Responses
{
	[DataContract]
	public class GetCommonCodeResponse
	{
		/// <summary>
		/// 요청한 GroupID 입니다.
		/// </summary>
		[DataMember] public string GroupId { get; set; }

		/// <summary>
		/// 조회된 Code 목록입니다.
		/// </summary>
		[DataMember] public List<CommonCodeModel> CodeList { get; set; } = new List<CommonCodeModel>();
	}
}