using System;
using GK.Booking.Infrastructure.Tools;

namespace GK.Booking.Infrastructure.Configuration
{
	public class WorkingDayStartTime : TimeSizeConfigBase
	{
		public DateTime GetWithTodayDate()
		{
			return DateTimeHelper.GetWithTodayDate(new TimeSpan(Hours, Minutes, Seconds));
		}
	}
}