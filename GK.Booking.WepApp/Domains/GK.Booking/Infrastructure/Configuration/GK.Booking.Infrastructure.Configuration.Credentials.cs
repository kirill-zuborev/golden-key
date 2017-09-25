using System.Configuration;

namespace GK.Booking.Infrastructure.Configuration
{
	public class Credentials : ConfigurationSection
	{
		[ConfigurationProperty("user", IsRequired = true)]
		public string User
		{
			get
			{
				return this["user"] as string;
			}
			set
			{
				this["user"] = value;
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

	}
}