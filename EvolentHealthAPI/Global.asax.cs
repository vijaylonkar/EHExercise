using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Repository.Services;

namespace EvolentHealthAPI
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			string loggingConfigFileLocation = HttpContext.Current.Server.MapPath("/EvolentHealthAPI/") + "ConfigData\\log4NetConfig.xml";
			if (System.IO.File.Exists(loggingConfigFileLocation))
			{
				new Logger().ConfigLogging(loggingConfigFileLocation);
			}
		}
	}
}
