using System;

namespace GK.Booking.Infrastructure
{
	public class DataBaseOperationException : ApplicationException
	{
		public DataBaseOperationException()
		{
		}

		public DataBaseOperationException(string message) : base(message)
		{
		}

		public DataBaseOperationException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}