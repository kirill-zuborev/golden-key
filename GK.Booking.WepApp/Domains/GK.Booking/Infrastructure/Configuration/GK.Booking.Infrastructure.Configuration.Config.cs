using System.Configuration;
using System.Linq;

namespace GK.Booking.Infrastructure.Configuration
{
	public class Config : IConfig
	{
		public TimeMapTraySize TimeMapTraySize
		{
			get
			{
				return (TimeMapTraySize)ConfigurationManager.GetSection("timeMapTraySize");
			}
		}

		public CookingDelayTime CookingDelayTime
		{
			get
			{
				return (CookingDelayTime)ConfigurationManager.GetSection("cookingDelayTime");
			}
		}

		public WorkingDayStartTime WorkingDayStartTime
		{
			get
			{
				return (WorkingDayStartTime)ConfigurationManager.GetSection("workingDayStartTime");
			}
		}

		public WorkingDayEndTime WorkingDayEndTime
		{
			get
			{
				return (WorkingDayEndTime)ConfigurationManager.GetSection("workingDayEndTime");
			}
		}

		public OrderExpireTime OrderExpireTime
		{
			get
			{
				return (OrderExpireTime)ConfigurationManager.GetSection("orderExpireTime");
			}
		}

		public MaxOrderCost MaxOrderCost
		{
			get
			{
				return (MaxOrderCost)ConfigurationManager.GetSection("maxOrderCost");
			}
		}

		public MailSetting MailSetting
		{
			get
			{
				return (MailSetting)ConfigurationManager.GetSection("mailConfig");
			}
		}

		public SmsSettings SmsSettings
		{
			get
			{
				return (SmsSettings)ConfigurationManager.GetSection("smsConfig");
			}
		}

		public IQueryable<MailAddress> DevGroupMailAddresses
		{
			get
			{
				MailAddressesConfigSection configSource = (MailAddressesConfigSection)ConfigurationManager.GetSection("mailAddressesConfig");
				return configSource.DevGroup.OfType<MailAddress>().AsQueryable<MailAddress>();
			}
		}

		public IQueryable<MailAddress> AdminGroupMailAddresses
		{
			get
			{
				MailAddressesConfigSection configSource = (MailAddressesConfigSection)ConfigurationManager.GetSection("mailAddressesConfig");
				return configSource.AdminGroup.OfType<MailAddress>().AsQueryable<MailAddress>();
			}
		}

		public IQueryable<TimeMapRule> TimeMapRules
		{
			get
			{
				TimeMapRulesConfigSection configSource = (TimeMapRulesConfigSection)ConfigurationManager.GetSection("timeMapRules");
				return configSource.TimeMapRules.OfType<TimeMapRule>().AsQueryable<TimeMapRule>();
			}
		}

		public Credentials Credentials
		{
			get
			{
				return (Credentials)ConfigurationManager.GetSection("credentials");
			}
		}

		public bool AllowMail
		{
			get
			{
				return bool.Parse(ConfigurationManager.AppSettings["AllowMail"]);
			}
		}

		public ClientTimeZoneShift ClientTimeZoneShift
		{
			get
			{
				return (ClientTimeZoneShift)ConfigurationManager.GetSection("clientTimeZoneShift");
			}
		}
	}
}