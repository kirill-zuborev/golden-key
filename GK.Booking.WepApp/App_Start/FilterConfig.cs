using GK.Booking.Filters;
using System.Web.Mvc;

namespace GK.Booking.WepApp.App_Start
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleExceptions());
		}
	}
}