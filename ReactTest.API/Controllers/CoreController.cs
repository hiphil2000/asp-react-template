using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ReactTest.API.Models;
using ReactTest.API.Models.Responses;

namespace ReactTest.API.Controllers
{
	public class CoreController : ApiController
	{
		private static List<CommonCodeModel> Codes = new List<CommonCodeModel>()
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
			var session = HttpContext.Current.Session;
			
			return new GetCommonCodeResponse()
			{
				GroupId = groupId,
				CodeList = Codes.Where(c => c.GroupId.Equals(groupId)).ToList()
			};
		} 
	}
}