using System;

namespace GK.Booking.Models
{
	public class OrderLine
	{
		public int OrderLineID { get; set; }
		public int OrderId { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public Order Order { get; set; }

		public OrderLine()
		{

		}

		public OrderLine(string name, decimal price)
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