using System;
using GK.Booking.AL.Services;
using GK.Booking.Infrastructure.Tools;
using GK.Booking.WepApp.Domains.GK.Booking.Resources;

namespace GK.Booking.AL
{
	public class ContactApplication
	{
		private readonly IContactService _contactService;

		public ContactApplication(IContactService contactService)
		{
			if (contactService == null)
			{
				throw new ArgumentNullException(nameof(contactService));
			}

			_contactService = contactService;
		}

		public bool IsPhoneLocked(string phoneNumber)
		{
			try
			{
				return _contactService.IsPhoneLocked(PhoneNumberHelper.GetNumberWithCountryCode(phoneNumber));
			}
			catch (ArgumentException ex)
			{
				throw new BookingDomainValidationException(ErrorCodesConst.INVALID_PHONE_NUMBER, Messages_RU.INVALID_PHONE_NUMBER, ex);
			}
			catch (ApplicationException ex)
			{
				throw new ApplicationException("IsPhoneLocked exception in Contact Service", ex);
			}
		}

		public void LockPhone(string phoneNumber)
		{
			try
			{
				_contactService.LockPhone(phoneNumber);
			}
			catch (ArgumentException ex)
			{
				throw new BookingDomainValidationException(ErrorCodesConst.INVALID_PHONE_NUMBER, Messages_RU.INVALID_PHONE_NUMBER, ex);
			}
			catch (ApplicationException ex)
			{
				throw new ApplicationException("LockPhone exception in Contact Service", ex);
			}
		}

		public void UnlockPhone(string phoneNumber)
		{
			try
			{
				_contactService.UnlockPhone(phoneNumber);
			}
			catch (ArgumentException ex)
			{
				throw new BookingDomainValidationException(ErrorCodesConst.INVALID_PHONE_NUMBER, Messages_RU.INVALID_PHONE_NUMBER, ex);
			}
			catch (InvalidOperationException)
			{
				return; //When phone number was not locked, there is no sense say about it
			}
			catch (ApplicationException ex)
			{
				throw new ApplicationException("UnlockPhone exception in Contact Service", ex);
			}
		}

		public LockedPhonesDTO GetLockedPhones(int pageNumber, int pageSize, string searchString)
		{
			try
			{
				return _contactService.GetLockedPhones(pageNumber, pageSize, searchString);
			}
			catch(ApplicationException ex)
			{
				throw new ApplicationException("GetLockedPhones exception in Contact Service", ex);
			}
		}

		public void Dispose()
		{
			_contactService.Dispose();
		}
	}
}