using System.Web.Http;
using ReactTest.API.Libraries;

namespace ReactTest.API.Controllers
{
	public class AuthController : ApiController
	{
		[HttpGet]
		[ActionName("CreateSession")]
		public string CreateSession()
		{
			return JwtHelper.CreateToken();
		}
	}
}