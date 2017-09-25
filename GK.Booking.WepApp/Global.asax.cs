using GK.Booking.WepApp.App_Start;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GK.Booking.Infrastructure
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			UnityConfig.RegisterComponents();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
		}
	}
}