using System.Runtime.Serialization;

namespace ReactTest.API.Models
{
	/// <summary>
	/// 메뉴 모델입니다.
	/// </summary>
	[DataContract]
	public class MenuModel
	{
		/// <summary>
		/// 메뉴의 ID
		/// </summary>
		[DataMember] public string MenuId { get; set; }
		
		/// <summary>
		/// 부모 메뉴 ID
		/// </summary>
		[DataMember] public string ParentId { get; set; }
		
		/// <summary>
		/// 메뉴의 이름
		/// </summary>
		[DataMember] public string MenuName { get; set; }
		
		/// <summary>
		/// 메뉴의 경로
		/// </summary>
		[DataMember] public string MenuPath { get; set; }
		
		/// <summary>
		/// 메뉴 표시 순서
		/// </summary>
		[DataMember] public int DisplayNo { get; set; }
		
		/// <summary>
		/// 메뉴 여부
		/// </summary>
		[DataMember] public bool MenuYn { get; set; }
		
		/// <summary>
		/// 페이지 여부
		/// </summary>
		[DataMember] public bool PageYn { get; set; }
		
		/// <summary>
		/// 사용 여부
		/// </summary>
		[DataMember] public bool UseYn { get; set; }
		
		/// <summary>
		/// 표시 여부
		/// </summary>
		[DataMember] public bool VisibleYn { get; set; }
	}
}