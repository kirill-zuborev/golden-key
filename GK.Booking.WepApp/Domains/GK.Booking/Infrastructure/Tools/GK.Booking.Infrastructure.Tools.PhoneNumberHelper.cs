namespace GK.Booking.Infrastructure.Tools
{
	public static class PhoneNumberHelper
	{
		private const string COUNTRY_CODE = "375";
		public static string GetNumberWithCountryCode(string phoneNumber)
		{
			return string.Format("{0}{1}", COUNTRY_CODE, phoneNumber);
		}
	}
}