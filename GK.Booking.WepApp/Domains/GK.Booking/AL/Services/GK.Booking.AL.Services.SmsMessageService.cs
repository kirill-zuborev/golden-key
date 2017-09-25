using System;
using System.Net;
using System.Web;
using System.Text;
using System.IO;
using GK.Booking.Infrastructure.Configuration;
using GK.Booking.Models.Exceptions;

namespace GK.Booking.AL.Services
{
	public class SmsMessageService : IMessageService
	{
		private readonly IConfig _config;

		public SmsMessageService(IConfig config)
		{
			if (config == null)
			{
				throw new ArgumentNullException(nameof(config));
			}

			_config = config;
		}

		public void Send(string designation, string sender, string message)
		{
			SendMessage(designation, sender, message);
		}

		private void SendMessage(string designation, string sender, string message)
		{
			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(_config.SmsSettings.ServiceUri + _config.SmsSettings.ApiFunction);

			webRequest.ContentType = "application/x-www-form-urlencoded";
			webRequest.Method = "POST";
			webRequest.KeepAlive = false;
			webRequest.PreAuthenticate = false;

			string postData = string.Format("user={0}&password={1}&recipient={2}&message={3}&sender={4}",
						_config.SmsSettings.UserLogin,
						_config.SmsSettings.Password,
						designation,
						HttpUtility.UrlEncode(message),
						HttpUtility.UrlEncode(sender));

			var ascii = new ASCIIEncoding();
			byte[] byteArray = ascii.GetBytes(postData);
			webRequest.ContentLength = byteArray.Length;

			using (Stream dataStream = webRequest.GetRequestStream())
			{
				dataStream.Write(byteArray, 0, byteArray.Length);
				dataStream.Close();
			}

			WebResponse webResponse = webRequest.GetResponse();

			using (Stream responceStream = webResponse.GetResponseStream())
			{
				Encoding enc = System.Text.Encoding.UTF8;
				StreamReader loResponseStream = new
						StreamReader(webResponse.GetResponseStream(), enc);

				string response = loResponseStream.ReadToEnd();

				//Service return positive value when request success
				int responseCode = 0;

				int.TryParse(response, out responseCode);

				if (responseCode < 0)
				{
					switch (responseCode)
					{
						case -1:
							throw new LowBalanceException();
						case -2:
							throw new ClientConfigurationException("'userLogin' or 'password'");
						case -3:
							throw new ClientConfigurationException("'textTemplate'");
						case -4:
							throw new IncorrectDestinationNumberException();
						case -5:
							throw new ClientConfigurationException("'sender'");
						case -6:
							throw new ClientConfigurationException("'userLogin'");
						case -7:
							throw new ClientConfigurationException("'password'");
						default:
							throw new OtherServiceException();
					}
				}
			}
		}
	}
}