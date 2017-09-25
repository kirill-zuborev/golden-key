using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GK.Booking.Models
{
	public class CustomerCode
	{
		public string Code { get; set; }

		public CustomerCode() { }

		public CustomerCode(string code)
		{
			if (string.IsNullOrWhiteSpace(code))
			{
				throw new ArgumentNullException("code");
			}

			Code = code;
		}
	}
}