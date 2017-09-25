using System;
using System.Configuration;

namespace GK.Booking.Infrastructure.Configuration
{
	public class TimeSizeConfigBase : ConfigurationSection
	{
		[ConfigurationProperty("hours", IsRequired = false, DefaultValue = 0)]
		public int Hours
		{
			get
			{
				return (int)this["hours"];
			}
		}

		[ConfigurationProperty("minutes", IsRequired = false, DefaultValue = 0)]
		public int Minutes
		{
			get
			{
				return (int)this["minutes"];
			}
		}

		[ConfigurationProperty("seconds", IsRequired = false, DefaultValue = 0)]
		public int Seconds
		{
			get
			{
				return (int)this["seconds"];
			}
		}

		public TimeSpan ToTimeSpan()
		{
			return new TimeSpan(Hours, Minutes, Seconds);
		}
	}
}