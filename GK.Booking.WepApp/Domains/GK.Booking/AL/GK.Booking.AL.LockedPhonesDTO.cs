using System;
using System.Collections.Generic;

namespace GK.Booking.AL
{
	public class LockedPhonesDTO
	{
		public List<LockedPhoneDTO> PhonesList { get; set; }
		public int TotalCount { get; set; }
	}

	public class LockedPhoneDTO
	{
		public string PhoneNumber { get; set; }
		public DateTime LockDate { get; set; }
	}
}