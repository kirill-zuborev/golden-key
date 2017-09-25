using System;

namespace GK.Booking.Models.Exceptions
{
	public class SmsServiceRequestErrorException : ApplicationException
	{
		public int ExceptionCode { get; }

		public SmsServiceRequestErrorException(int exceptionCode)
		{
			ExceptionCode = exceptionCode;
		}

		public SmsServiceRequestErrorException(int exceptionCode, string message) : base(message)
		{
			ExceptionCode = exceptionCode;
		}
	}
}