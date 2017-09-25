using System;
using System.Net;
using System.Web.Mvc;
using System.Threading.Tasks;
using GK.Booking.AL;
using GK.Booking.Filters;

namespace GK.Booking.Infrastructure
{
	public class NotifyController : Controller
	{
		private readonly MessageApplication _messageApp;

		private const string ERROR_MESSAGE_SUBJECT = "Error in Golden Key app";
		private const string FEEDBACK_MESSAGE_SUBJECT = "Feedback about the Golden Key app";

		public NotifyController(MessageApplication messageApp)
		{
			if (messageApp == null)
			{
				throw new ArgumentNullException(nameof(messageApp));
			}

			_messageApp = messageApp;
		}

		[HandleExceptions]
		public async Task<ActionResult> NotifyFeedback(string messageData)
		{
			await Task.Run(() => _messageApp.NotifyAdminGroup(FEEDBACK_MESSAGE_SUBJECT, messageData));

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		protected override void Dispose(bool disposing)
		{
			_messageApp.Dispose();
			base.Dispose(disposing);
		}
	}
}