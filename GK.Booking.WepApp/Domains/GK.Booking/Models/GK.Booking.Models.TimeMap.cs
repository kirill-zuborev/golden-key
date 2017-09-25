using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using GK.Booking.Infrastructure.Tools;
using GK.Booking.Infrastructure.Configuration;
using GK.Booking.Models.Exceptions;

namespace GK.Booking.Models
{
	public class TimeMap
	{
		private ReadOnlyCollection<TimeMapTray> _timeMapTrays;
		private readonly TimeMapConfig _timeMapConfig;

		private const bool TRAY_IS_FULL = true;
		private const bool TRAY_IS_NOT_FULL = false;

		public DateTime CreationDate { get; private set; }

		public TimeMap(IReadOnlyCollection<Order> todayOrdersMap, TimeMapConfig timeMapConfig)
		{
			_timeMapConfig = timeMapConfig;

			Init(todayOrdersMap);
		}

		public TimeMapTray GetTimeMapTray(TimeSpan startDate, TimeSpan endDate)
		{
			return _timeMapTrays.SingleOrDefault(t => t.StartTime == startDate && t.EndTime == endDate);
		}

		public IReadOnlyCollection<TimeMapTray> GetTimeMapTrays(TimeSpan strartingFrom)
		{
			return _timeMapTrays.Where(t =>
							t.StartTime >= (strartingFrom + _timeMapConfig.CookingDelay)).ToList<TimeMapTray>();
		}

		public bool IsTimeMapTrayFull(TimeSpan targetStartTime, TimeSpan targetEndTime)
		{
			var timeMapTray = GetTimeMapTray(targetStartTime, targetEndTime);

			if (timeMapTray == null)
			{
				throw new TimeMapTrayNotFoundException("Target time map tray was not found");
			}

			if (timeMapTray.IsFull())
			{
				return TRAY_IS_FULL;
			}

			return TRAY_IS_NOT_FULL;
		}

		public void RegisterCreatedOrder(Order order)
		{
			TimeMapTray targetTray = GetTimeMapTray(order.TargetStartDate.TimeOfDay, order.TargetEndDate.TimeOfDay);

			targetTray.AddOrderItem(order.OrderIdentifier, order.GetOrderExpirationDate(_timeMapConfig.OrderLifeTime), order.OrderStatus);
		}

		public void UnregisterCreatedOrder(Order order)
		{
			TimeMapTray targetTray = GetTimeMapTray(order.TargetStartDate.TimeOfDay, order.TargetEndDate.TimeOfDay);

			targetTray.InvalidateOrderItem(order.OrderIdentifier);
		}

		public void UpdateOrderStatus(Order order)
		{
			TimeMapTray targetTray = GetTimeMapTray(order.TargetStartDate.TimeOfDay, order.TargetEndDate.TimeOfDay);

			targetTray.UpdateOrderItemStatus(order.OrderIdentifier, order.OrderStatus);
		}

		private void Init(IReadOnlyCollection<Order> todayOrdersMap)
		{
			TimeSpan mapStartTime = DateTimeHelper.RoundTo(_timeMapConfig.WorkingDayStart,
				Convert.ToInt32(_timeMapConfig.TraySize.TotalMinutes));

			var trayList = new List<TimeMapTray>();

			for (TimeSpan i = _timeMapConfig.WorkingDayStart; i < _timeMapConfig.WorkingDayEnd; i += _timeMapConfig.TraySize)
			{
				var trayOrers = todayOrdersMap.Where(t => t.TargetStartDate.TimeOfDay >= i &&
														t.TargetEndDate.TimeOfDay <= i.Add(_timeMapConfig.TraySize)).ToList<Order>();

				TimeMapTray trayItem = new TimeMapTray(i,
														i.Add(_timeMapConfig.TraySize),
														GetTrayCapcityByRules(i, (i + _timeMapConfig.TraySize)),
														_timeMapConfig.OrderLifeTime,
														trayOrers);

				trayList.Add(trayItem);
			}

			_timeMapTrays = new ReadOnlyCollection<TimeMapTray>(trayList);

			CreationDate = DateTimeHelper.GetCurrentApplicationDateTimeValue();
		}

		private int GetTrayCapcityByRules(TimeSpan startTime, TimeSpan endTime)
		{
			TimeMapRule suitableRuleByStartTime = GetDefaultRule();
			TimeMapRule suitableRuleByEndTime = GetDefaultRule();

			for (int i = 0; i < _timeMapConfig.TimeRules.Count; i++)
			{
				if (_timeMapConfig.TimeRules[i].StartTime <= startTime && _timeMapConfig.TimeRules[i].EndTime >= startTime)
				{
					if (_timeMapConfig.TimeRules[i].MaxOrders > suitableRuleByStartTime.MaxOrders)
					{
						suitableRuleByStartTime = _timeMapConfig.TimeRules[i];
					}
				}

				if (_timeMapConfig.TimeRules[i].StartTime < endTime && _timeMapConfig.TimeRules[i].EndTime > endTime)
				{
					if (_timeMapConfig.TimeRules[i].MaxOrders > suitableRuleByStartTime.MaxOrders)
					{
						suitableRuleByStartTime = _timeMapConfig.TimeRules[i];
					}
				}
			}

			//Return max Capacity
			return suitableRuleByStartTime.MaxOrders > suitableRuleByEndTime.MaxOrders ?
				suitableRuleByStartTime.MaxOrders : suitableRuleByEndTime.MaxOrders;
		}

		private TimeMapRule GetDefaultRule()
		{
			return new TimeMapRule
			{
				Id = 0,
				StartTime = DateTimeHelper.GetStartDateOfToday().TimeOfDay,
				EndTime = DateTimeHelper.GetEndDateOfToday().TimeOfDay,
				MaxOrders = 1
			};
		}
	}
}