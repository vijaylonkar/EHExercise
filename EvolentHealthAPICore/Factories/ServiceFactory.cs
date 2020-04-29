using EvolentHealthAPICore.BusinessLogic.Interfaces;
using EvolentHealthAPICore.BusinessLogic.Services;
using Microsoft.Practices.Unity;
using Repository.Interfaces;
using Repository.Services;

namespace EvolentHealthAPICore.Factories
{
	public class ServiceFactory
	{
		private readonly UnityContainer container;
		public ServiceFactory(string repositoryType)
		{
			this.container = new UnityContainer();
			this.container.RegisterType<ILogger, Logger>();
			switch (repositoryType.ToLower())
			{
				case "sql":
					this.container.RegisterType<IContactRepository, ContactSQLDB>();
					break;
				case "json":
					container.RegisterType<IContactRepository, ContactJsonDB>();
					break;
			}
			this.container.RegisterType<IContact, Contact>();
		}

		public T Resolve<T>()
		{
			return this.container.Resolve<T>();
		}

	}
}
