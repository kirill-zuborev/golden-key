using System.Web.Mvc;
using System.Web.Routing;

namespace GK.Booking.Infrastructure
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.MapRoute("Home", "",
				new
				{
					controller = "Page",
					action = "Home"
				});

			routes.MapRoute("Login", "Login",
				new
				{
					controller = "Page",
					action = "Login"
				});

			routes.MapRoute("Link", "Link",
				new
				{
					controller = "Page",
					action = "Link"
				});

			routes.MapRoute("Kitchen", "Kitchen",
				new
				{
					controller = "Page",
					action = "Kitchen"
				});

			routes.MapRoute("Manager", "Manager",
				new
				{
					controller = "Page",
					action = "Manager"
				});

			routes.MapRoute("Admin", "Admin",
				new
				{
					controller = "Page",
					action = "Admin"
				});

			routes.MapRoute("NotFound", "NotFound",
				new
				{
					controller = "Page",
					action = "NotFound"
				});

			routes.MapRoute("Error", "Error",
				new
				{
					controller = "Page",
					action = "Error"
				});

			routes.MapRoute(
				name: "DefaultApi",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}