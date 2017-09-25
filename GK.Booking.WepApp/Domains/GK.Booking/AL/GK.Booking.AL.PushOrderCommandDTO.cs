namespace GK.Booking.AL
{
	using System.Collections.Generic;

	public class PushOrderCommandDTO
	{
		public string PhoneNumber { get; set; }
		public string TargetStartTime { get; set; }
		public string TargetEndTime { get; set; }
		public IEnumerable<string> MenuItemsNames { get; set; }
	}
}