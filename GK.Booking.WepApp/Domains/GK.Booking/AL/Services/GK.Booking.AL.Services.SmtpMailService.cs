using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace GK.Booking.AL.Services
{
	public class SmtpMailService : IMessageService
	{
		public void Send(string designation, string sender, string message)
		{
			try
			{
				var client = new SmtpClient("smtp.yandex.ru", 587);
				client.EnableSsl = true;
				var msg = new MailMessage("just.dzmitry@yandex.ru", designation);

				msg.Subject = sender;
				msg.Body = message;
				msg.SubjectEncoding = Encoding.UTF8;
				msg.BodyEncoding = Encoding.UTF8;
				msg.Priority = MailPriority.High;
				msg.IsBodyHtml = false;

				client.Credentials = new NetworkCredential("just.dzmitry", "5447Dzmitry5447");
				client.Send(msg);
			}
			catch (Exception ex)
			{
				throw new Exception("", ex);
			}
		}
	}
}