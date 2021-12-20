using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.SessionState;
using Newtonsoft.Json.Serialization;
using ReactTest.API.Filters;

namespace ReactTest.API
{
	public class WebApiApplication : HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			
			// Session을 사용하도록 설정합니다.
			HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);

			#region Configure Json Response

			// Json Response를 사용하도록 합니다.
			GlobalConfiguration.Configuration.Formatters.Clear();

			var jsonFormatter = new JsonMediaTypeFormatter
			{
				SerializerSettings =
				{
					ContractResolver = new CamelCasePropertyNamesContractResolver()
				}
			};
			GlobalConfiguration.Configuration.Formatters.Add(jsonFormatter);
			
			#endregion

			#region Configure JWT

			GlobalConfiguration.Configuration.Filters.Add(new TokenAuthenticationAttribute());

			#endregion
		}
	}
}