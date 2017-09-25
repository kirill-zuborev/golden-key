namespace GK.Booking.WebApp
{
	using System;
	using System.Web.Mvc;

	using AL;
	using Filters;

	public class TimeMapController : Controller
	{
		private readonly TimeMapApplication _timeMapApp;
		private const string TIME_MAP_TRAY_AVILABLE = "Avilable";
		private const string TIME_MAP_TRAY_FULL = "Full";

		public TimeMapController(TimeMapApplication timeQueueApp)
		{
			if (timeQueueApp == null)
			{
				throw new ArgumentNullException(nameof(timeQueueApp));
			}

			_timeMapApp = timeQueueApp;
		}

		[HttpGet]
		[HandleExceptions]
		[AbortWhenBookingDisabled]
		public ActionResult GetTimeMap()
		{
			return Json(_timeMapApp.GetTimeMap(), JsonRequestBehavior.AllowGet);
		}

		[HandleExceptions]
		[AbortWhenBookingDisabled]
		public ActionResult CheckTimeMapTrayState(string startTime, string endTime)
		{
			if (_timeMapApp.IsTimeMapTrayAvilable(startTime, endTime))
			{
				return Json(TIME_MAP_TRAY_AVILABLE);
			}
			else
			{
				return Json(TIME_MAP_TRAY_FULL);
			}
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}