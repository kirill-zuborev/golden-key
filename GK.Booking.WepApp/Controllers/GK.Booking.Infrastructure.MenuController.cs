using System;
using System.Web.Mvc;
using GK.Booking.AL;
using GK.Booking.Filters;

namespace GK.Booking.WebApp
{
	public class MenuController : Controller
	{
		private MenuApplication _menuApp;

		public MenuController(MenuApplication menuApplication)
		{
			if (menuApplication == null)
			{
				throw new ArgumentNullException(nameof(menuApplication));
			}

			_menuApp = menuApplication;
		}

		[HttpGet]
		[AbortWhenBookingDisabled]
		public ActionResult Index()
		{
			return Json(_menuApp.GetMenu(), JsonRequestBehavior.AllowGet);
		}
	}
}