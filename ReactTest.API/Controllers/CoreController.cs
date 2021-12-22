using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ReactTest.API.Filters;
using ReactTest.API.Models;
using ReactTest.API.Models.Responses;
using ReactTest.API.Libraries;

namespace ReactTest.API.Controllers
{
	[RoleBasedAuthentication(IsWhiteList = false, DenyList = new []{ Role.Anonymous })]
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

		[HttpGet]
		[ActionName("GetCommonCodes")]
		public GetCommonCodeResponse GetCommonCodes(string groupId)
		{
			var payload = this.GetJwtPayload();
			
			return new GetCommonCodeResponse()
			{
				GroupId = groupId,
				CodeList = Codes.Where(c => c.GroupId.Equals(groupId)).ToList()
			};
		} 
	}
}