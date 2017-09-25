namespace GK.Booking.WebApp
{
	using System;
	using System.Web.Mvc;

	using AL;
	using Filters;

	public class OrdersController : Controller
	{
		private readonly BookingApplication _bookingApp;
		private readonly MenuApplication _menuApp;

		private const string OPERATION_SUCCESS_RESPONCE = "Done";
		private const string OPERATION_FAIL_RESPONCE = "Fail";

		public OrdersController(BookingApplication bookingApplication, MenuApplication menuApplication)
		{
			if (bookingApplication == null)
			{
				throw new ArgumentNullException(nameof(bookingApplication));
			}

			_bookingApp = bookingApplication;

			if (menuApplication == null)
			{
				throw new ArgumentNullException(nameof(menuApplication));
			}

			_menuApp = menuApplication;
		}

		[HttpPost]
		[HandleExceptions]
		[AbortWhenBookingDisabled]
		public ActionResult PushOrder(PushOrderCommandDTO pushOrderCommand)
		{
			var pushedOrder = _bookingApp.PushOrder(pushOrderCommand);
			return Json(pushedOrder, JsonRequestBehavior.AllowGet);
		}

		[HandleExceptions]
		[AbortWhenBookingDisabled]
		public ActionResult ConfirmOrder(int orderId, string customerSecretCode)
		{
			if (_bookingApp.ConfirmOrder(orderId, customerSecretCode) == true)
			{
				return Json(OPERATION_SUCCESS_RESPONCE);
			}
			else
			{
				return Json(OPERATION_FAIL_RESPONCE);
			}
		}

		[Authorize]
		[HandleExceptions]
		public ActionResult ConfirmOrderToReady(int orderId)
		{
			_bookingApp.ConfirmOrderToReady(orderId);

			return Json(OPERATION_SUCCESS_RESPONCE);
		}

		[Authorize]
		[HandleExceptions]
		public ActionResult ReadyOrderToConfirm(int orderId)
		{
			_bookingApp.ReadyOrderToConfirm(orderId);

			return Json(OPERATION_SUCCESS_RESPONCE);
		}

		[Authorize]
		[HandleExceptions]
		public ActionResult CompleteOrder(int orderId)
		{
			_bookingApp.CompleteOrder(orderId);

			return Json(OPERATION_SUCCESS_RESPONCE);
		}

		[Authorize]
		[HandleExceptions]
		public ActionResult DenyOrder(int orderId)
		{
			_bookingApp.DenyOrder(orderId);

			return Json(OPERATION_SUCCESS_RESPONCE);
		}

		[HttpGet]
		[Authorize]
		[HandleExceptions]
		public ActionResult GetTodayActiveOrders()
		{
			return Json(_bookingApp.GetTodayActiveOrders(), JsonRequestBehavior.AllowGet);
		}

		protected override void Dispose(bool disposing)
		{
			_bookingApp.Dispose();
			base.Dispose(disposing);
		}
	}
}