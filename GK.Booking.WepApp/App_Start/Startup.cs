using System;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using System.Web;

[assembly: OwinStartup(typeof(GK.Booking.WepApp.App_Start.Startup))]
namespace GK.Booking.WepApp.App_Start
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = "ApplicationCookie",
				LoginPath = new PathString("/Login"),
				LogoutPath = new PathString("/Login"),
				ExpireTimeSpan = TimeSpan.FromDays(1),
				ReturnUrlParameter = "ReturnUrl",
				Provider = new CookieAuthenticationProvider
				{
					OnApplyRedirect = redirectContext =>
					{
						if (!IsAjaxRequest(redirectContext.Request))
						{
							redirectContext.Response.Redirect(redirectContext.RedirectUri);
						}
					}
				}
			});
		}

		private static bool IsAjaxRequest(IOwinRequest request)
		{
			IReadableStringCollection query = request.Query;
			if (query != null)
			{
				if (query["X-Requested-With"] == "XMLHttpRequest")
				{
					return true;
				}
			}

			IHeaderDictionary headers = request.Headers;
			if (headers != null)
			{
				if (headers["X-Requested-With"] == "XMLHttpRequest")
				{
					return true;
				}
			}

			return false;
		}
	}
}