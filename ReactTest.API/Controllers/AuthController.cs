using System.Web;
using System.Web.Http;

namespace ReactTest.API.Controllers
{
	public class AuthController : ApiController
	{
		[HttpGet]
		public void CreateSession()
		{
			var context = HttpContext.Current;
			if (context.Session != null)
			{
				return;
			}
			
			
		}
	}
}