using System.Text;
using System.Web.Mvc;
using System.Net;
using GK.Booking.AL;
using GK.Booking.Infrastructure.Tools;

namespace GK.Booking.Filters
{
	public class HandleExceptions : FilterAttribute, IExceptionFilter
	{
		public void OnException(ExceptionContext exceptionContext)
		{
			if (!exceptionContext.ExceptionHandled)
			{
				if (exceptionContext.Exception is BookingDomainValidationException)
				{
					BookingDomainValidationException ex = exceptionContext.Exception as BookingDomainValidationException;
					exceptionContext.Result = new JsonResult()
					{
						ContentType = "application/json;",
						Data = new ErrorInfoDTO { ErrorCode = ex.ExceptionCode, ErrorMessage = ex.Message },
						ContentEncoding = Encoding.UTF8
					};

					exceptionContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
				}
				else
				{
					var request = exceptionContext.RequestContext.HttpContext.Request;
					ExceptionReport report = new ExceptionReport(
						request.UserAgent,
						request.Url.ToString(),
						request.UrlReferrer.ToString(),
						exceptionContext.Exception.Message,
						exceptionContext.Exception.StackTrace,
						DateTimeHelper.GetCurrentApplicationDateTimeValue());


					var messageApp = DependencyResolver.Current.GetService<MessageApplication>();
					messageApp.NotifyDevGroup("Error in Golden Key application", report.ReportData);

					exceptionContext.Result = new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
				}

				exceptionContext.ExceptionHandled = true;
			}
		}
	}
}