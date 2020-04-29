using System.Web.Http;
using EvolentHealthAPI.Factories;
using EvolentHealthAPICore.Interfaces;
using EvolentHealthAPICore.Services;
using Microsoft.Practices.Unity;
using Repository.Interfaces;
using Repository.Services;

namespace EvolentHealthAPI
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			var container = new UnityContainer();
			container.RegisterType<ILogger, Logger>(new HierarchicalLifetimeManager());
			container.RegisterType<ICore, Core>(new HierarchicalLifetimeManager());
			config.DependencyResolver = new UnityResolver(container);

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
