using System;

namespace GK.Booking.Models
{
	public class Price
	{
		public int Value { get; private set; }
		public Price(int value)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("Price can not be negative.");
			}

			Value = value;
		}
	}
}