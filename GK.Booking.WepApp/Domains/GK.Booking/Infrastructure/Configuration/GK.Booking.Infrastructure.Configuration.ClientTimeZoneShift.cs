namespace GK.Booking.Infrastructure.Configuration
{
	using System;
	using System.Configuration;

	public class ClientTimeZoneShift : ConfigurationSection
	{
		[ConfigurationProperty("value", IsRequired = true)]
		public int ShiftMinutes
		{
			get
			{
				int timeZoneShift;

				if (!int.TryParse(this["value"].ToString(), out timeZoneShift))
					throw new FormatException("Incorrect clientTimeZoneShift config value");

				return timeZoneShift * 60;
			}
		}
	}
}