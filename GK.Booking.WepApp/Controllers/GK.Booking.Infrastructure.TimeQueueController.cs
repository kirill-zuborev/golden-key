using System;
using System.Web.Mvc;
using System.Web;
using System.Net;
using GK.Booking.AL;

namespace GK.Booking.WebApp
{
    public class TimeQueueController : Controller
    {
        private readonly TimeQueueApplication _timeQueueApp;
        private const string TIME_MAP_TRAY_EMPTY = "Empty";
        private const string TIME_MAP_TRAY_FULL = "Full";

        public TimeQueueController(TimeQueueApplication timeQueueApp)
        {
            if (timeQueueApp == null)
            {
                throw new ArgumentNullException("timeQueueApplication");
            }

            _timeQueueApp = timeQueueApp;
        }

        [HttpGet]
        public ActionResult GetTimeMap()
        {
            try
            {
                return Json(_timeQueueApp.GetTimeMap(), JsonRequestBehavior.AllowGet);
            }
            catch(ApplicationException)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        public ActionResult CheckTimeMapTrayState(DateTime startDate, DateTime endDate)
        {
            try
            {
                if(_timeQueueApp.IsTimeMapTrayEmpty(startDate, endDate))
                {
                    return Json(TIME_MAP_TRAY_EMPTY);
                }
                else
                {
                    return Json(TIME_MAP_TRAY_FULL);
                }
            }
            catch (ApplicationException)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _timeQueueApp.Dispose();
            base.Dispose(disposing);
        }
    }
}