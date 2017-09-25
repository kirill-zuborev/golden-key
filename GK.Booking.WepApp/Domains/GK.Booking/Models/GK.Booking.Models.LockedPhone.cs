using System;

namespace GK.Booking.Models
{
	public class LockedPhone
	{
		public int LockedPhoneId { get; set; }
		public string PhoneNumber { get; set; }
		public DateTime LockDate { get; set; }
		public bool IsLocked { get; set; }
	}
}