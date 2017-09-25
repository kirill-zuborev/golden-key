namespace GK.Booking.Infrastructure.Tools
{
	using System;

	public static class DateTimeHelper
	{
		public static DateTime ToApplicationDateTime(DateTime dateTime)
		{
			return dateTime.ToUniversalTime();
		}

		public static TimeSpan ExtractApplicationTime(DateTime dateTime)
		{
			return dateTime.ToUniversalTime().TimeOfDay;
		}

		public static DateTime GetCurrentApplicationDateTimeValue()
		{
			return DateTime.UtcNow;
		}

		public static TimeSpan GetCurrentApplicationTimeValue()
		{
			return DateTime.UtcNow.TimeOfDay;
		}

		public static DateTime GetWithApplicationKind(DateTime dateTime)
		{
			return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
		}

		public static DateTime GetWithTodayDate(TimeSpan timeSpan)
		{
			DateTime currentDate = DateTimeHelper.GetCurrentApplicationDateTimeValue();
			DateTime returnedDateTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

			return DateTime.SpecifyKind(returnedDateTime, DateTimeKind.Utc);
		}

		public static DateTime GetStartDateOfToday()
		{
			DateTime today = DateTime.UtcNow;
			TimeSpan dayStartTime = TimeSpan.Parse("00:00:00");

			return new DateTime(today.Year, today.Month, today.Day, dayStartTime.Hours, dayStartTime.Minutes, dayStartTime.Seconds, DateTimeKind.Utc);
		}

		public static DateTime GetEndDateOfToday()
		{
			DateTime today = DateTime.UtcNow;
			TimeSpan dayEndTime = TimeSpan.Parse("23:59:59");

			return new DateTime(today.Year, today.Month, today.Day, dayEndTime.Hours, dayEndTime.Minutes, dayEndTime.Seconds, DateTimeKind.Utc);
		}

		public static DateTime RoundTo(DateTime date, int toMinutes)
		{
			TimeSpan time;

			time = (date.Subtract(DateTime.MinValue)).Add(new TimeSpan(0, toMinutes, 0));
			DateTime returnedDateTime = DateTime.MinValue.Add(new TimeSpan(0, (((int)time.TotalMinutes) / toMinutes) * toMinutes, 0));
			return GetWithApplicationKind(returnedDateTime);
		}

		public static TimeSpan RoundTo(TimeSpan time, int toMinutes)
		{
			return new TimeSpan(0, (((int)time.TotalMinutes) / toMinutes) * toMinutes, 0);
		}

		public static string GetAsUserView(TimeSpan time, int userTimeOffset)
		{
			return time.Add(new TimeSpan(0, userTimeOffset, 0)).ToString(@"hh\:mm");
		}

		public static string GetAsUserView(DateTime date, int userTimeOffset)
		{
			return date.Add(new TimeSpan(0, userTimeOffset, 0)).ToString(@"hh\:mm");
		}

		public static TimeSpan GetAsApplicationTime(string time, int userTimeOffset)
		{
			TimeSpan convertedTime;
			if (!TimeSpan.TryParse(time, out convertedTime))
				throw new ArgumentException($"Incorrect format of input time string.");

			return convertedTime.Add(new TimeSpan(0, -userTimeOffset, 0));
		}
	}
}