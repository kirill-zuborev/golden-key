using System;
using GK.Booking.AL.Services;

namespace GK.Booking.AL
{
	public class ConfigurationApplication
	{
		private readonly IConfigService _configService;

		public ConfigurationApplication(IConfigService configService)
		{
			if (configService == null)
			{
				throw new ArgumentNullException(nameof(configService));
			}

			_configService = configService;
		}

		public ConfigDTO GetApplicationParameters()
		{
			try
			{
				return _configService.GetApplicationParameters();
			}
			catch (ApplicationException ex)
			{
				throw new ApplicationException("Config service error", ex);
			}
		}
	}
}