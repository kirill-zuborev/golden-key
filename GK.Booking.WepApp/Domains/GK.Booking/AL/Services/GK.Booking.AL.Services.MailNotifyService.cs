using System;
using System.Text;
using System.Net;
using System.ComponentModel;
using System.Net.Mail;
using GK.Booking.Infrastructure.Configuration;
using GK.Booking.Models.Exceptions;

namespace GK.Booking.AL.Services
{
	public class MailNotifyService : INotifyService
	{
		private readonly IConfig _config;

		private SmtpClient _mailClient;

		public MailNotifyService(IConfig config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("Config is not initialized");
			}

			_config = config;

			InitMailClient();
		}

		public void SendMessage(string subject, string message, string destAddress)
		{
			if (_config.AllowMail.Equals(true))
			{
				var mailMessage = new MailMessage(_config.MailSetting.SmtpFrom, destAddress)
				{
					Subject = subject,
					Body = message,
					BodyEncoding = Encoding.UTF8,
					SubjectEncoding = Encoding.UTF8,
					IsBodyHtml = false
				};

				Send(mailMessage);
			}
		}

		public void SendMessage(string subject, string message, string[] destAddressCollection)
		{
			if (_config.AllowMail.Equals(true))
			{
				var mailMessage = new MailMessage
				{
					From = new System.Net.Mail.MailAddress(_config.MailSetting.SmtpFrom),
					Subject = subject,
					Body = message,
					BodyEncoding = Encoding.UTF8,
					SubjectEncoding = Encoding.UTF8,
					IsBodyHtml = false,
					Priority = MailPriority.Normal
				};

				for (int i = 0; i < destAddressCollection.Length; i++)
				{
					mailMessage.To.Add(destAddressCollection[i]);
				}

				Send(mailMessage);
			}
		}

		private void InitMailClient()
		{
			try
			{
				_mailClient = new SmtpClient
				{
					Host = _config.MailSetting.SmtpServer,
					Port = _config.MailSetting.SmtpPort,
					EnableSsl = _config.MailSetting.EnableSsl,
					DeliveryMethod = SmtpDeliveryMethod.Network,
					UseDefaultCredentials = false
				};
				_mailClient.Credentials = new NetworkCredential(_config.MailSetting.SmtpUserName, _config.MailSetting.SmtpPassword);
			}
			catch (ApplicationException ex)
			{
				throw new ConfigurationException("Сan not configure smtp mail client", ex);
			}
		}

		private void Send(MailMessage message)
		{
			try
			{
				_mailClient.Send(message);
			}
			catch (InvalidOperationException ex)
			{
				throw new MessageNotSentException(string.Format("Current state does not allow to send message (Application state problem)"), ex);
			}
			catch (SmtpException ex)
			{
				throw new MessageNotSentException(string.Format("Failed to connect to the SMTP server (Conection or credentials problem)"), ex);
			}
		}

		public void Dispose()
		{
			_mailClient.Dispose();
		}

		private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
		{
			_mailClient.Dispose();
		}
	}
}