using System.Net;
using System.Web;
using System.Web.Mvc;
using GK.Booking.ApplicationControl;

namespace GK.Booking.Filters
{
	public class AbortWhenBookingDisabled : ActionFilterAttribute
	{
		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			HttpResponseBase response = filterContext.HttpContext.Response;
			var appState = DependencyResolver.Current.GetService<ApplicationState>();

			if (!appState.IsBookingClientEnabled)
			{
				filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
			}
		}
	}
}