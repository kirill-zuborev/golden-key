using System;
using System.Web.Mvc;
using GK.Booking.AL;
using GK.Booking.Filters;
using GK.Booking.ApplicationControl;
using GK.Booking.WepApp.Domains.GK.Booking.Resources;

namespace GK.Booking.Infrastructure
{
	public class ConfigurationController : Controller
	{
		private readonly ConfigurationApplication _configApp;
		private readonly ApplicationState _appState;

		public ConfigurationController(ConfigurationApplication configurationApplication, ApplicationState appState)
		{
			if (configurationApplication == null)
			{
				throw new ArgumentNullException(nameof(configurationApplication));
			}

			_configApp = configurationApplication;

			if (appState == null)
			{
				throw new ArgumentNullException(nameof(appState));
			}

			_appState = appState;
		}

		[HttpGet]
		[HandleExceptions]
		[AbortWhenBookingDisabled]
		public ActionResult GetApplicationParameters()
		{
			return Json(_configApp.GetApplicationParameters(), JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		[HandleExceptions]
		public ActionResult GetBookingClientStatus()
		{
			return Json(new BookingClientStatusDTO(_appState.IsBookingClientEnabled, _appState.DisabledReasonText), JsonRequestBehavior.AllowGet);
		}

		[Authorize]
		[HandleExceptions]
		public ActionResult DisableBookingClient()
		{
			_appState.SetBookingClientDisabled(Messages_RU.DISABLED_FROM_ADMIN_SCREEN);

			return Json(_appState.DisabledReasonText);
		}

		[Authorize]
		[HandleExceptions]
		public void EnableBookingClient()
		{
			_appState.SetBookingClientEnabled();
		}

		private class BookingClientStatusDTO
		{
			public bool IsBookingClientEnabled { get; set; }
			public string DisabledReasonText { get; set; }

			public BookingClientStatusDTO()
			{ }

			public BookingClientStatusDTO(bool isBookingClientEnabled, string disabledReasonText)
			{
				IsBookingClientEnabled = isBookingClientEnabled;
				DisabledReasonText = disabledReasonText;
			}
		}
	}
}