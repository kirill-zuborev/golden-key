using System;
using System.Web.Mvc;
using GK.Booking.AL;
using GK.Booking.Filters;

namespace GK.Booking.WebApp
{
	public class ContactController : Controller
	{
		private readonly ContactApplication _contactApp;
		private const string PHONE_LOCKED = "locked";
		private const string PHONE_NOT_LOCKED = "notLocked";

		public ContactController(ContactApplication contactApp)
		{
			if (contactApp == null)
			{
				throw new ArgumentNullException(nameof(contactApp));
			}

			_contactApp = contactApp;
		}

		[HandleExceptions]
		[AbortWhenBookingDisabled]
		public ActionResult CheckPhoneStatus(string phoneNumber)
		{
			if (_contactApp.IsPhoneLocked(phoneNumber))
			{
				return Json(PHONE_LOCKED);
			}
			else
			{
				return Json(PHONE_NOT_LOCKED);
			}
		}

		[Authorize]
		[HandleExceptions]
		public ActionResult LockPhone(string phoneNumber)
		{
			_contactApp.LockPhone(phoneNumber);

			return Json(PHONE_LOCKED);
		}

		[Authorize]
		[HandleExceptions]
		public ActionResult UnlockPhone(string phoneNumber)
		{
			_contactApp.UnlockPhone(phoneNumber);

			return Json(PHONE_NOT_LOCKED);
		}

		[Authorize]
		[HandleExceptions]
		public ActionResult GetLockedPhones(int pageNumber, int pageSize, string searchString)
		{
			var lockedPhones = _contactApp.GetLockedPhones(pageNumber, pageSize, searchString);

			return Json(lockedPhones);
		}

		protected override void Dispose(bool disposing)
		{
			_contactApp.Dispose();
			base.Dispose(disposing);
		}
	}
}