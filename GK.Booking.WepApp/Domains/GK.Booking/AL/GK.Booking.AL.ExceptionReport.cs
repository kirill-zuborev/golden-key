using System;
using GK.Booking.Infrastructure.Tools;

namespace GK.Booking.AL
{
	public class ExceptionReport
	{
		public string ReportData { get; }

		public ExceptionReport(string userAgentData, string requestedUrl, string reffererUrl, string exceptionMessage, string stackTraceData, DateTime exceptionTime)
		{
			ReportData = string.Format(
				"Exception time: {0}\n"
				+ "Generated at: {1}"
				+ "User agent: {2}\n"
				+ "Requested Url: {3}\n"
				+ "Refferer Url: {4}\n"
				+ "---------------------------------------------------------------------------------------------------------------\n\n"
				+ "Exception message: {5}\n\n"
				+ "Stack trace:\n{6}"
				, exceptionTime, DateTimeHelper.GetCurrentApplicationDateTimeValue(), userAgentData, requestedUrl, reffererUrl, exceptionMessage, stackTraceData);
		}
	}
}