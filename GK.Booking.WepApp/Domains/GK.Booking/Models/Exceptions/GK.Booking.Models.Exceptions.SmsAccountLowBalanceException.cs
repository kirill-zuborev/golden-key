using System;

namespace GK.Booking.Models.Exceptions
{
	public class SmsAccountLowBalanceException : ApplicationException
	{
		public SmsAccountLowBalanceException()
		{
		}

		public SmsAccountLowBalanceException(string message) : base(message)
		{
		}

		public SmsAccountLowBalanceException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}