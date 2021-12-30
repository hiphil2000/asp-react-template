using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ReactTest.API.Filters;
using ReactTest.API.Models;
using ReactTest.API.Models.Responses;
using ReactTest.API.Libraries;
using ReactTest.API.Models.Enums;

namespace ReactTest.API.Controllers
{
	[RoleBasedAuthentication(
		IsWhiteList = false
		// DenyList = new[] { Role.Anonymous }
	)]
	public class CoreController : ApiController
	{
		private static readonly List<CommonCodeModel> Codes = new List<CommonCodeModel>()
		{
			new CommonCodeModel()
			{
				GroupId = "G001",
				CodeId = "001",
				CodeName = "Code1"
			},
			new CommonCodeModel()
			{
				GroupId = "G001",
				CodeId = "002",
				CodeName = "Code2"
			},
			new CommonCodeModel()
			{
				GroupId = "G001",
				CodeId = "003",
				CodeName = "Code3"
			},
			new CommonCodeModel()
			{
				GroupId = "G002",
				CodeId = "001",
				CodeName = "2Code1"
			},
			new CommonCodeModel()
			{
				GroupId = "G002",
				CodeId = "002",
				CodeName = "2Code2"
			},
			new CommonCodeModel()
			{
				GroupId = "G002",
				CodeId = "003",
				CodeName = "2Code3"
			},
		};

		private static readonly MenuModel[] MenuList;

		private static readonly Dictionary<Role, List<MenuModel>>
			RoleMenuList = new Dictionary<Role, List<MenuModel>>();

		static CoreController()
		{
			MenuList = new[]
			{
				new MenuModel()
				{
					MenuId = "HomePage",
					MenuName = "Home",
					DisplayNo = 0,
					MenuPath = "/",
					MenuYn = true,
					PageYn = true,
					UseYn = true,
					VisibleYn = true
				},
				new MenuModel()
				{
					MenuId = "TestPage",
					MenuName = "Test Page",
					DisplayNo = 0,
					MenuPath = "/test_page",
					PageYn = true,
					UseYn = true,
					VisibleYn = true
				},
				new MenuModel()
				{
					MenuId = "AdminPage",
					MenuName = "Admin Page",
					DisplayNo = 0,
					MenuPath = "/admin_page",
					PageYn = true,
					UseYn = true,
					VisibleYn = true
				},
			};

			RoleMenuList.Add(Role.Anonymous, new List<MenuModel>());
			RoleMenuList.Add(Role.User, new List<MenuModel>());
			RoleMenuList.Add(Role.Admin, new List<MenuModel>());

			RoleMenuList[Role.Anonymous].Add(MenuList[0]);
			RoleMenuList[Role.User].Add(MenuList[0]);
			RoleMenuList[Role.Admin].Add(MenuList[0]);

			RoleMenuList[Role.User].Add(MenuList[1]);
			RoleMenuList[Role.Admin].Add(MenuList[1]);
		}

		public CoreController()
		{
		}

		/// <summary>
		/// 공통 코드를 조회합니다.
		/// </summary>
		/// <param name="groupId">공통 코드 그룹 ID</param>
		/// <returns></returns>
		[HttpGet]
		[ActionName("GetCommonCodes")]
		public GetCommonCodeResponse GetCommonCodes(string groupId)
		{
			return new GetCommonCodeResponse()
			{
				GroupId = groupId,
				CodeList = Codes.Where(c => c.GroupId.Equals(groupId)).ToList()
			};
		}

		/// <summary>
		/// 권한에 따른 메뉴 목록을 조회합니다.
		/// </summary>
		/// <returns>조회된 메뉴 목록</returns>
		[HttpGet]
		[ActionName("Menus")]
		[AllowAnonymous]
		public IEnumerable<MenuModel> GetMenus()
		{
			var payload = this.GetJwtPayload();

			return RoleMenuList[payload?.Role ?? Role.Anonymous];
		}
	}
}