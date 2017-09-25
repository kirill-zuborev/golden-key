namespace GK.Booking.AL.Services
{
	using System;

	using Models;

	public interface ITimeMapService
	{
		TimeMapDTO GetTimeMap(int timeZoneOffset);
		bool IsTimeMapTrayAvilable(TimeSpan startDate, TimeSpan endDate);
		void RegisterCreatedOrder(Order order);
		void UnegisterCreatedOrder(Order order);
		void UpdateOrderItemStatus(Order order);
	}
}
