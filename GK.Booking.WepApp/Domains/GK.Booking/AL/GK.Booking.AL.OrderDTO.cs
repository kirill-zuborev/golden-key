namespace GK.Booking.AL
{
	using System.Collections.Generic;

	public class OrderDTO
	{
		public int OrderID { get; set; }
		public string CustomerPhoneNumber { get; set; }
		public string CustomerSecretCode { get; set; }
		public string TargetStartTime { get; set; }
		public string TargetEndTime { get; set; }
		public string CreationTime { get; set; }
		public decimal TotalPrice { get; set; }
		public IEnumerable<OrderLineDTO> OrderLines { get; set; }
		public string OrderStatus { get; set; }
		public int OrderLifeTimeMilliseconds { get; set; }
	}
}