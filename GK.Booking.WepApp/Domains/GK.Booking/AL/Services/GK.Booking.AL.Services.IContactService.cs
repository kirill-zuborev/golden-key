namespace GK.Booking.AL.Services
{
	public interface IContactService
	{
		void LockPhone(string phoneNumber);
		bool IsPhoneLocked(string phoneNumber);
		void UnlockPhone(string phoneNumber);
		LockedPhonesDTO GetLockedPhones(int pageNumber, int pageSize, string searchString);
		void Dispose();
	}
}
