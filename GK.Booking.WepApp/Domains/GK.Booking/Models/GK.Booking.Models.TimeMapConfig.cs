using System;
using System.Collections.Generic;
using GK.Booking.Infrastructure.Configuration;

namespace GK.Booking.Models
{
	public class TimeMapConfig
	{
		public TimeSpan WorkingDayStart { get; private set; }
		public TimeSpan WorkingDayEnd { get; private set; }
		public TimeSpan TraySize { get; private set; }
		public TimeSpan CookingDelay { get; private set; }
		public TimeSpan OrderLifeTime { get; private set; }
		public List<TimeMapRule> TimeRules { get; private set; }

		public TimeMapConfig(TimeSpan workingDayStart, TimeSpan workingDayEnd, TimeSpan traySize, TimeSpan cookingDelay, TimeSpan orderLifeTime, IReadOnlyCollection<TimeMapRule> timeRules)
		{
			if (WorkingDayStart > WorkingDayEnd)
			{
				throw new ArgumentException("WorkingDayStart must be greater than WorkingDayEnd in TimeMapConfig");
			}

			WorkingDayStart = workingDayStart;
			WorkingDayEnd = workingDayEnd;
			TraySize = traySize;
			CookingDelay = cookingDelay;
			OrderLifeTime = orderLifeTime;
			TimeRules = new List<TimeMapRule>(timeRules);
		}
	}
}