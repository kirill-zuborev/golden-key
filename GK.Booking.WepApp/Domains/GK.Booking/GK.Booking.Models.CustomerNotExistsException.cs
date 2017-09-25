using System;
using System.Runtime.Serialization;

namespace GK.Booking.Models
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