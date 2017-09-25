using System;
using System.Runtime.Serialization;

namespace GK.Booking.Models
{
	[Serializable]
	public class CanNotGetMenuException : ApplicationException
	{
		public CanNotGetMenuException()
		{
		}

		public CanNotGetMenuException(string message) : base(message)
		{
		}

		public CanNotGetMenuException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected CanNotGetMenuException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}