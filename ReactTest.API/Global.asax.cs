using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Newtonsoft.Json.Serialization;

namespace ReactTest.API
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			
			// Json Response를 사용하도록 합니다.
			GlobalConfiguration.Configuration.Formatters.Clear();

			var jsonFormatter = new JsonMediaTypeFormatter()
			{
				SerializerSettings =
				{
					ContractResolver = new CamelCasePropertyNamesContractResolver()
				}
			};
			GlobalConfiguration.Configuration.Formatters.Add(jsonFormatter);
		}
	}
}
