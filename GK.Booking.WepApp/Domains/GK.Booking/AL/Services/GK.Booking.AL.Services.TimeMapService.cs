namespace GK.Booking.AL.Services
{
	using System;
	using System.Linq;

	using Models;
	using Infrastructure.Tools;

	public class TimeMapService : ITimeMapService
	{
		private readonly TimeMapFactory _timeMapFactory;

		public TimeMapService(TimeMapFactory timeMapFactory)
		{
			if (timeMapFactory == null)
			{
				throw new ArgumentNullException(nameof(timeMapFactory));
			}

			_timeMapFactory = timeMapFactory;
		}

		public TimeMapDTO GetTimeMap(int timeZoneOffset)
		{
			var timeMap = _timeMapFactory.GetTimeMap();

			var trays = timeMap.GetTimeMapTrays(DateTimeHelper.GetCurrentApplicationTimeValue()).OrderBy(t => t.StartTime)
				.Select(t =>
					new TimeMapTrayDTO
					{
						StartTime = DateTimeHelper.GetAsUserView(t.StartTime, timeZoneOffset),
						EndTime = DateTimeHelper.GetAsUserView(t.EndTime, timeZoneOffset),
						IsAvilable = !t.IsFull()
					})
					.ToArray();

			return new TimeMapDTO { TimeMapTrays = trays };
		}

		public bool IsTimeMapTrayAvilable(TimeSpan startTime, TimeSpan endTime)
		{
			var timeMap = _timeMapFactory.GetTimeMap();

			return !timeMap.IsTimeMapTrayFull(startTime, endTime);
		}

		public void RegisterCreatedOrder(Order order)
		{
			var timeMap = _timeMapFactory.GetTimeMap();

			timeMap.RegisterCreatedOrder(order);
		}

		public void UnegisterCreatedOrder(Order order)
		{
			var timeMap = _timeMapFactory.GetTimeMap();

			timeMap.UnregisterCreatedOrder(order);
		}

		public void UpdateOrderItemStatus(Order order)
		{
			var timeMap = _timeMapFactory.GetTimeMap();

			timeMap.UpdateOrderStatus(order);
		}
	}
}