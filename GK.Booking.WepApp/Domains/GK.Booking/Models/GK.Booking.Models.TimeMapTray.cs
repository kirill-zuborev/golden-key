using System;
using System.Linq;
using System.Collections.Generic;
using GK.Booking.Infrastructure.Tools;
using GK.Booking.Models.Exceptions;

namespace GK.Booking.Models
{
	public class TimeMapTray
	{
		private Dictionary<string, TimeMapTrayItem> _trayOrders = new Dictionary<string, TimeMapTrayItem>();

		public TimeSpan StartTime { get; private set; }
		public TimeSpan EndTime { get; private set; }
		public int Capacity { get; private set; }

		public bool IsFull()
		{
			if (_trayOrders.Values.Where(t => t.IsActive()).Count() >= Capacity)
			{
				var a = _trayOrders.Values.Where(t => t.IsActive()).ToList();
				return true;
			}
			else
			{
				return false;
			}
		}

		public TimeMapTray(TimeSpan startTime, TimeSpan endTime, int capacity, TimeSpan orderLifeTime, IReadOnlyCollection<Order> trayOrders)
		{
			if (startTime > endTime)
			{
				throw new ArgumentOutOfRangeException("Start time can not be more than End time.");
			}

			StartTime = startTime;
			EndTime = endTime;
			Capacity = capacity;

			var verifiedOrders = trayOrders.Where(t => t.TargetStartDate.TimeOfDay >= startTime
												&& t.TargetEndDate.TimeOfDay <= endTime).ToList<Order>();

			for (int i = 0; i < trayOrders.Count; i++)
			{
				_trayOrders.Add(verifiedOrders[i].OrderIdentifier,
					new TimeMapTrayItem(verifiedOrders[i].GetOrderExpirationDate(orderLifeTime), verifiedOrders[i].OrderStatus));
			}
		}

		public void AddOrderItem(string orderIdentifier, DateTime orderExpirationDate, string orderStatus)
		{
			if (IsFull())
			{
				throw new TimeMapTrayFullException("Time map tray is full");
			}

			_trayOrders.Add(orderIdentifier, new TimeMapTrayItem(orderExpirationDate, orderStatus));
		}

		public void InvalidateOrderItem(string orderIdentifier)
		{
			_trayOrders[orderIdentifier].UpdateStatus(Consts.OrderStatus.ORDER_INVALID);
		}

		public void UpdateOrderItemStatus(string orderIdentifier, string newStatus)
		{
			_trayOrders[orderIdentifier].UpdateStatus(newStatus);
		}

		private class TimeMapTrayItem
		{
			private DateTime _itemExpirationDate;
			private string _itemStatus;

			public TimeMapTrayItem(DateTime itemExpirationDate, string itemStatus)
			{
				_itemExpirationDate = itemExpirationDate;
				_itemStatus = itemStatus;
			}

			public void UpdateStatus(string newStatus)
			{
				_itemStatus = newStatus;
			}

			public bool IsActive()
			{
				bool isActive = _itemStatus == Consts.OrderStatus.ORDER_CONFIRMED
									|| _itemStatus == Consts.OrderStatus.ORDER_READY
									|| (_itemExpirationDate > DateTimeHelper.GetCurrentApplicationDateTimeValue()
															&& _itemStatus == Consts.OrderStatus.ORDER_CREATED);

				return isActive;
			}
		}
	}
}