using System.Linq;

namespace GK.Booking.Infrastructure.Configuration
{
	public interface IConfig
	{
		CookingDelayTime CookingDelayTime { get; }
		TimeMapTraySize TimeMapTraySize { get; }
		WorkingDayStartTime WorkingDayStartTime { get; }
		WorkingDayEndTime WorkingDayEndTime { get; }
		OrderExpireTime OrderExpireTime { get; }
		MaxOrderCost MaxOrderCost { get; }
		MailSetting MailSetting { get; }
		SmsSettings SmsSettings { get; }
		IQueryable<MailAddress> DevGroupMailAddresses { get; }
		IQueryable<MailAddress> AdminGroupMailAddresses { get; }
		IQueryable<TimeMapRule> TimeMapRules { get; }
		Credentials Credentials { get; }
		bool AllowMail { get; }
		ClientTimeZoneShift ClientTimeZoneShift { get; }
	}
}