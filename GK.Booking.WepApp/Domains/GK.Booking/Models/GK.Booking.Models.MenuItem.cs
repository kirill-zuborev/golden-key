using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GK.Booking.Models
{
	public class MenuItem
	{
		public string Name { get; private set; }

		public string Desc { get; private set; }

		public decimal Price { get; private set; }

		public MenuItem(string name, decimal price)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentNullException(nameof(name));
			}

			Name = name;

			Price = price;
		}
	}
}