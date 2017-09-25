using System.Web.Mvc;
using System.Configuration;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using GK.Booking.Infrastructure;
using GK.Booking.Infrastructure.Configuration;
using GK.Booking.Infrastructure.Repositories.Orders;
using GK.Booking.Infrastructure.Repositories.Contacts;
using GK.Booking.Models;
using GK.Booking.AL.Services;
using GK.Booking.ApplicationControl;

namespace GK.Booking.WepApp.App_Start
{
	public static class UnityConfig
	{
		public static void RegisterComponents()
		{
			var container = new UnityContainer();

			// register all your components with the container here
			// it is NOT necessary to register your controllers

			// e.g. container.RegisterType<ITestService, TestService>();

			container
				.RegisterInstance<ICustomersList>(new CustomersList())
				.RegisterInstance(new MenuRepository().Get());

			container
				.RegisterType<TimeMapFactory>(new ContainerControlledLifetimeManager())
				.RegisterType<ApplicationState>(new ContainerControlledLifetimeManager())
				.RegisterType<IConfig, Config>()
				.RegisterType<IOrderUnitOfWork, EFOrderUnitOfWork>(
					new InjectionConstructor(ConfigurationManager.ConnectionStrings["OrderContext"].ConnectionString))
				.RegisterType<IContactUnitOfWork, EFContactUnitOfWork>(
					new InjectionConstructor(ConfigurationManager.ConnectionStrings["ContactContext"].ConnectionString))
				.RegisterType<IOrderService, OrderService>()
				.RegisterType<IContactService, ContactService>()
				.RegisterType<ITimeMapService, TimeMapService>()
				.RegisterType<IConfigService, ConfigService>()
				.RegisterType<IMessageService, SmsMessageService>()
				.RegisterType<INotifyService, MailNotifyService>()
				.RegisterType<IMenuService, MenuService>();

			DependencyResolver.SetResolver(new UnityDependencyResolver(container));
		}
	}
}