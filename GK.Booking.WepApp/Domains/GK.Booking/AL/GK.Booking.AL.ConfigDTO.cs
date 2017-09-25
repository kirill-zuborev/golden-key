using System;

namespace GK.Booking.AL
{
	public class ConfigDTO
	{
		public DateTime WorkingStartTime { get; set; }
		public DateTime WorkingEndTime { get; set; }
		public decimal MaxOrderCost { get; set; }
		public bool IsWorkingDayEnd { get; set; }
	}
}