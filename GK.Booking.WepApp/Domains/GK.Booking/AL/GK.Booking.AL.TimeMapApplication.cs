namespace GK.Booking.AL
{
	using System;

	using Services;
	using Infrastructure.Tools;


	public class TimeMapApplication
	{
		private readonly IConfigService _configService;
		private readonly ITimeMapService _timeMapService;

		public TimeMapApplication(ITimeMapService timeMapService, IConfigService configService)
		{
			if (timeMapService == null)
			{
				throw new ArgumentNullException(nameof(timeMapService));
			}

			_timeMapService = timeMapService;

			if (configService == null)
			{
				throw new ArgumentNullException(nameof(configService));
			}

			_configService = configService;
		}

		public TimeMapDTO GetTimeMap()
		{
			try
			{
				int timeZoneOffset = _configService.GetClientTimeShiftMinutes();
				var timeMap = _timeMapService.GetTimeMap(timeZoneOffset);

				return timeMap;
			}
			catch (ApplicationException ex)
			{
				throw new ApplicationException("Exception while creating Time Map", ex);
			}
		}

		public bool IsTimeMapTrayAvilable(string startDate, string endDate)
		{
			var startTime = DateTimeHelper.GetAsApplicationTime(startDate, _configService.GetClientTimeShiftMinutes());
			var endTime = DateTimeHelper.GetAsApplicationTime(endDate, _configService.GetClientTimeShiftMinutes());

			try
			{
				return _timeMapService.IsTimeMapTrayAvilable(startTime, endTime);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw new ApplicationException("Requested Start Date value greather than End Date value", ex);
			}
			catch (ApplicationException ex)
			{
				throw new ApplicationException("Exception while checking Time Map Tray state.", ex);
			}
		}
	}
}