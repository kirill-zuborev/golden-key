using GK.Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GK.Booking.Infrastructure
{
	public class CustomersList : ICustomersList
	{
		public bool IsExist(CustomerCode costomerCode)
		{
			return true;
		}
	}
}