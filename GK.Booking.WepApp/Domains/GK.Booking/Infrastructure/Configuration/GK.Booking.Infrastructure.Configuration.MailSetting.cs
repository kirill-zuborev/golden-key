using System.Configuration;

namespace GK.Booking.Infrastructure.Configuration
{
	public class MailSetting : ConfigurationSection
	{
		[ConfigurationProperty("SmtpServer", IsRequired = true)]
		public string SmtpServer
		{
			get
			{
				return this["SmtpServer"] as string;
			}
			set
			{
				this["SmtpServer"] = value;
			}
		}

		[ConfigurationProperty("SmtpPort", IsRequired = false, DefaultValue = "25")]
		public int SmtpPort
		{
			get
			{
				return (int)this["SmtpPort"];
			}
			set
			{
				this["SmtpPort"] = value;
			}
		}

		[ConfigurationProperty("SmtpUserName", IsRequired = true)]
		public string SmtpUserName
		{
			get
			{
				return this["SmtpUserName"] as string;
			}
			set
			{
				this["SmtpUserName"] = value;
			}
		}

		[ConfigurationProperty("SmtpPassword", IsRequired = true)]
		public string SmtpPassword
		{
			get
			{
				return this["SmtpPassword"] as string;
			}
			set
			{
				this["SmtpPassword"] = value;
			}
		}

		[ConfigurationProperty("SmtpFrom", IsRequired = true)]
		public string SmtpFrom
		{
			get
			{
				return this["SmtpFrom"] as string;
			}
			set
			{
				this["SmtpFrom"] = value;
			}
		}

		[ConfigurationProperty("EnableSsl", IsRequired = false, DefaultValue = "false")]
		public bool EnableSsl
		{
			get
			{
				return (bool)this["EnableSsl"];
			}
			set
			{
				this["EnableSsl"] = value;
			}
		}
	}
}