using System.Configuration;

namespace GK.Booking.Infrastructure.Configuration
{
	public class SmsSettings : ConfigurationSection
	{
		[ConfigurationProperty("textTemplate", IsRequired = true)]
		public string TextTemplate
		{
			get
			{
				return this["textTemplate"] as string;
			}
			set
			{
				this["textTemplate"] = value;
			}
		}

		[ConfigurationProperty("sender", IsRequired = true)]
		public string Sender
		{
			get
			{
				return this["sender"] as string;
			}
			set
			{
				this["sender"] = value;
			}
		}
		[ConfigurationProperty("userLogin", IsRequired = true)]
		public string UserLogin
		{
			get
			{
				return this["userLogin"] as string;
			}
			set
			{
				this["userLogin"] = value;
			}
		}

		[ConfigurationProperty("password", IsRequired = true)]
		public string Password
		{
			get
			{
				return this["password"] as string;
			}
			set
			{
				this["password"] = value;
			}
		}

		[ConfigurationProperty("serviceUri", IsRequired = true)]
		public string ServiceUri
		{
			get
			{
				return this["serviceUri"] as string;
			}
			set
			{
				this["serviceUri"] = value;
			}
		}

		[ConfigurationProperty("apiFunction", IsRequired = true)]
		public string ApiFunction
		{
			get
			{
				return this["apiFunction"] as string;
			}
			set
			{
				this["apiFunction"] = value;
			}
		}
	}
}