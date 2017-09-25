using System;
using System.Collections.Generic;
using System.Linq;

namespace GK.Booking.Models
{
	public class Order
	{
		public int OrderId { get; set; }
		public string CustomerPhoneNumber { get; set; }
		public string CustomerSecretCode { get; set; }
		public DateTime TargetStartDate { get; set; }
		public DateTime TargetEndDate { get; set; }
		public DateTime CreationDate { get; set; }
		public string OrderStatus { get; set; }
		public string OrderIdentifier { get; set; }
		public IEnumerable<OrderLine> OrderLines { get; set; }

		public decimal GetTotalPrice()
		{
			if (OrderLines != null)
			{
				return OrderLines.Sum(l => l.Price);
			}

			return 0;
		}

		public DateTime GetOrderExpirationDate(TimeSpan orderLifeTime)
		{
			return CreationDate.Add(orderLifeTime);
		}
	}
}