namespace GK.Booking.AL.Services
{
	public interface IConfigService
	{
		ConfigDTO GetApplicationParameters();

		string[] GetAdminGroupMailAddresses();

		string[] GetDevGroupMailAddresses();

		int GetClientTimeShiftMinutes();
	}
}