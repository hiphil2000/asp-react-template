using System.Runtime.Serialization;

namespace ReactTest.API.Models
{
	[DataContract]
	public class CommonCodeModel
	{
		/// <summary>
		/// CommonCode의 GroupID입니다.
		/// </summary>
		[DataMember] public string GroupId { get; set; }
		
		/// <summary>
		/// CommonCode의 CodeID입니다.
		/// </summary>
		[DataMember] public string CodeId { get; set; }
		
		/// <summary>
		/// CommonCode의 상세 이름입니다.
		/// </summary>
		[DataMember] public string CodeName { get; set; }
		
		/// <summary>
		/// CommonCode의 설명입니다.
		/// </summary>
		[DataMember] public string Description { get; set; }
		
		/// <summary>
		/// CommonCode의 정렬 순서입니다.
		/// </summary>
		[DataMember] public string DisplayOrder { get; set; }
		
		/// <summary>
		/// 커스텀 값 1입니다.
		/// </summary>
		[DataMember] public string Custom1 { get; set; }
		
		/// <summary>
		/// 커스텀 값 2입니다.
		/// </summary>
		[DataMember] public string Custom2 { get; set; }
		
		/// <summary>
		/// 커스텀 값 3입니다.
		/// </summary>
		[DataMember] public string Custom3 { get; set; }
		
		/// <summary>
		/// 커스텀 값 4입니다.
		/// </summary>
		[DataMember] public string Custom4 { get; set; }
		
		/// <summary>
		/// 커스텀 값 5입니다.
		/// </summary>
		[DataMember] public string Custom5 { get; set; }
		
		/// <summary>
		/// CommonCode의 사용 여부입니다.
		/// </summary>
		[DataMember] public bool useYn { get; set; }
	}
}