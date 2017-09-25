using System.Configuration;

namespace GK.Booking.Infrastructure.Configuration
{
	public class MaxOrderCost : ConfigurationSection
	{
		[ConfigurationProperty("value", IsRequired = false, DefaultValue = "0")]
		public decimal Value
		{
			get
			{
				return (decimal)this["value"];
			}
		}
	}
}