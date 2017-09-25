using System;
using System.Linq;
using GK.Booking.Infrastructure.Configuration;
using GK.Booking.Infrastructure.Tools;

namespace GK.Booking.AL.Services
{
	public class ConfigService : IConfigService
	{
		private readonly IConfig _config;

		public ConfigService(IConfig config)
		{
			if (config == null)
			{
				throw new ArgumentNullException(nameof(config));
			}

			_config = config;
		}

		public ConfigDTO GetApplicationParameters()
		{
			TimeSpan bookingStopTime = _config.WorkingDayEndTime.ToTimeSpan() - _config.CookingDelayTime.ToTimeSpan();
			bool isWorkingDayEnd = (DateTimeHelper.GetCurrentApplicationDateTimeValue().TimeOfDay >= bookingStopTime);

			return new ConfigDTO
			{
				WorkingStartTime = _config.WorkingDayStartTime.GetWithTodayDate(),
				WorkingEndTime = _config.WorkingDayEndTime.GetWithTodayDate(),
				MaxOrderCost = _config.MaxOrderCost.Value,
				IsWorkingDayEnd = isWorkingDayEnd
			};
		}

		public string[] GetAdminGroupMailAddresses()
		{
			return _config.AdminGroupMailAddresses.Select(t => t.Address).ToArray<string>();
		}

		public string[] GetDevGroupMailAddresses()
		{
			return _config.DevGroupMailAddresses.Select(t => t.Address).ToArray<string>();
		}

		public int GetClientTimeShiftMinutes()
		{
			return _config.ClientTimeZoneShift.ShiftMinutes;
		}
	}
}