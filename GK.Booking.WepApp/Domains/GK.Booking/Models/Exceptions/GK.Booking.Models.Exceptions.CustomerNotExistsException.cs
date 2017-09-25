using System;

namespace GK.Booking.Models.Exceptions
{
	[Serializable]
	internal class CustomerNotExistsException : ApplicationException
	{
		public CustomerCode CustomerCode { get; set; }

		public CustomerNotExistsException(CustomerCode customerCode)
		{
			this.CustomerCode = customerCode;
		}
	}
}