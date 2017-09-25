using System;
using System.Linq;
using System.Text.RegularExpressions;
using GK.Booking.Models;
using GK.Booking.Infrastructure.Tools;
using GK.Booking.Models.Exceptions;

namespace GK.Booking.AL.Services
{
	public class ContactService : IContactService
	{
		private readonly IContactUnitOfWork _contactDatabase;
		private const int PHONE_NUMBER_LENGTH = 12;

		public ContactService(IContactUnitOfWork contactDatabase)
		{
			if (contactDatabase == null)
			{
				throw new ArgumentNullException(nameof(contactDatabase));
			}

			_contactDatabase = contactDatabase;
		}

		public void LockPhone(string phoneNumber)
		{
			if(!IsCorectPhoneNumber(phoneNumber))
			{
				throw new ArgumentException("Phone number is incorrect");
			}

			var lockedPhone = _contactDatabase.LockedPhones.Find(phone => phone.PhoneNumber == phoneNumber).FirstOrDefault();

			if(lockedPhone == null)
			{
				lockedPhone = new LockedPhone
				{
					PhoneNumber = phoneNumber,
					LockDate = DateTimeHelper.GetCurrentApplicationDateTimeValue(),
					IsLocked = true
				};

				_contactDatabase.LockedPhones.Create(lockedPhone);
			}
			else
			{
				lockedPhone.IsLocked = true;
				lockedPhone.LockDate = DateTimeHelper.GetCurrentApplicationDateTimeValue();

				_contactDatabase.LockedPhones.Update(lockedPhone);
			}

			_contactDatabase.Save();
		}

		public bool IsPhoneLocked(string phoneNumber)
		{
			if (!IsCorectPhoneNumber(phoneNumber))
			{
				throw new IncorrectPhoneNumberException("Phone number is incorrect");
			}

			var lockedPhone = _contactDatabase.LockedPhones.Find(phone => phone.PhoneNumber == phoneNumber
																	&& phone.IsLocked == true).FirstOrDefault();

			bool result = (lockedPhone != null);
			return result;
		}

		public void UnlockPhone(string phoneNumber)
		{
			if (!IsCorectPhoneNumber(phoneNumber))
			{
				throw new ArgumentException("Phone number is incorrect");
			}

			var lockedPhone = _contactDatabase.LockedPhones.Find(phone => phone.PhoneNumber == phoneNumber
																	&& phone.IsLocked == true).FirstOrDefault();

			if (lockedPhone == null)
			{
				throw new InvalidOperationException("Phone is not locked");
			}
			else
			{
				lockedPhone.IsLocked = false;

				_contactDatabase.LockedPhones.Update(lockedPhone);
			}

			_contactDatabase.Save();
		}

		public LockedPhonesDTO GetLockedPhones(int pageNumber, int pageSize, string searchString)
		{
			int skipRows = (pageNumber - 1) * pageSize;

			var lockedPhones = _contactDatabase.LockedPhones
				.Find(phone => phone.PhoneNumber.Contains(searchString) && phone.IsLocked == true)
				.OrderByDescending(phone => phone.LockDate)
				.Skip(skipRows)
				.Take(pageSize)
				.Select(phone =>
					new LockedPhoneDTO { PhoneNumber = phone.PhoneNumber, LockDate = phone.LockDate }
				).ToList();

			var result = new LockedPhonesDTO
			{
					PhonesList = lockedPhones,
					TotalCount = _contactDatabase.LockedPhones.Find(phone => 
						phone.PhoneNumber.Contains(searchString) && phone.IsLocked == true).Count()
			};

			return result;
		}

		public void Dispose()
		{
			_contactDatabase.Dispose();
		}

		private bool IsCorectPhoneNumber(string phoneNumber)
		{
			Regex regex = new Regex(@"^[0-9 ]+$");
			return regex.IsMatch(phoneNumber) 
				&& phoneNumber.Length == PHONE_NUMBER_LENGTH;
		}
	}
}