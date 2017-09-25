using System;

namespace GK.Booking.AL
{
	public class BookingDomainValidationException : ApplicationException
	{
		public int ExceptionCode { get; }

		public BookingDomainValidationException(int exceptionCode)
		{
			ExceptionCode = exceptionCode;
		}

		public BookingDomainValidationException(int exceptionCode, string message) : base(message)
		{
			ExceptionCode = exceptionCode;
		}

		public BookingDomainValidationException(int exceptionCode, string message, Exception innerException) : base(message, innerException)
		{
			ExceptionCode = exceptionCode;
		}
	}
}