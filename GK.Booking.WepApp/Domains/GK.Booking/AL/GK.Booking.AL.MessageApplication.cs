using System;
using GK.Booking.AL.Services;
using GK.Booking.Models.Exceptions;
using GK.Booking.WepApp.Domains.GK.Booking.Resources;

namespace GK.Booking.AL
{
	public class MessageApplication
	{
		private readonly IConfigService _configService;
		private readonly INotifyService _notifyService;

		public MessageApplication(IConfigService configService, INotifyService notifyService)
		{
			if (configService == null)
			{
				throw new ArgumentNullException(nameof(configService));
			}

			_configService = configService;

			if (notifyService == null)
			{
				throw new ArgumentNullException(nameof(notifyService));
			}

			_notifyService = notifyService;
		}

		public void NotifyAdminGroup(string subject, string message)
		{
			try
			{
				_notifyService.SendMessage(subject, message, _configService.GetAdminGroupMailAddresses());
			}
			catch (ConfigurationException ex)
			{
				throw new ApplicationException("Error in Smtp client configuration.", ex);
			}
			catch (MessageNotSentException ex)
			{
				throw new BookingDomainValidationException(ErrorCodesConst.MESSAGE_NOT_SENT, Messages_RU.MESSAGE_NOT_SENT, ex);
			}
			catch (ApplicationException ex)
			{
				throw new ApplicationException("Exception occurred while sending mail to Admin group.", ex);
			}
		}

		public void NotifyDevGroup(string subject, string message)
		{
			try
			{
				_notifyService.SendMessage(subject, message, _configService.GetDevGroupMailAddresses());
			}
			catch (ConfigurationException ex)
			{
				throw new ApplicationException("Error in Smtp client configuration.", ex);
			}
			catch (MessageNotSentException ex)
			{
				throw new BookingDomainValidationException(ErrorCodesConst.MESSAGE_NOT_SENT, Messages_RU.MESSAGE_NOT_SENT, ex);
			}
			catch (ApplicationException ex)
			{
				throw new ApplicationException("Exception occurred while sending mail to Dev group.", ex);
			}
		}

		public void Dispose()
		{
			_notifyService.Dispose();
		}
	}
}