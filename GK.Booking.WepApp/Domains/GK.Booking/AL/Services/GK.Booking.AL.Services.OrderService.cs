namespace GK.Booking.AL.Services
{
	using System;
	using System.Linq;
	using System.Collections.Generic;

	using Models;
	using Models.Exceptions;
	using Infrastructure;
	using Infrastructure.Configuration;
	using Infrastructure.Tools;

	public class OrderService : IOrderService
	{
		private readonly IOrderUnitOfWork _ordersDatabase;
		private readonly IContactService _contactService;
		private readonly ITimeMapService _timeMapService;
		private readonly OrderFactory _orderFactory;
		private readonly IConfig _config;

		public OrderService(IOrderUnitOfWork ordersDatabase, IContactService contactService, ITimeMapService timeMapService, IConfig config, OrderFactory orderFactory)
		{
			if (ordersDatabase == null)
			{
				throw new ArgumentNullException(nameof(ordersDatabase));
			}

			_ordersDatabase = ordersDatabase;

			if (contactService == null)
			{
				throw new ArgumentNullException(nameof(contactService));
			}

			_contactService = contactService;

			if (timeMapService == null)
			{
				throw new ArgumentNullException(nameof(timeMapService));
			}

			_timeMapService = timeMapService;

			if (config == null)
			{
				throw new ArgumentNullException(nameof(config));
			}

			_config = config;

			if (orderFactory == null)
			{
				throw new ArgumentNullException(nameof(orderFactory));
			}

			_orderFactory = orderFactory;
		}

		public OrderDTO PushOrder(PushOrderCommand pushOrderCommand)
		{
			//Check phone number in black list base
			if (_contactService.IsPhoneLocked(pushOrderCommand.PhoneNumber))
			{
				throw new PhoneIsLockedException("Phone number is locked");
			}

			//Create order from factory
			var order = _orderFactory.CreateOrder(pushOrderCommand.PhoneNumber,
													pushOrderCommand.TargetStartDate,
													pushOrderCommand.TargetEndDate,
													pushOrderCommand.MenuItemsNames);

			//Check total order cost
			if (order.GetTotalPrice() > _config.MaxOrderCost.Value)
			{
				throw new MaximumValueExceededException("Exceeded of the maximum price value of the order");
			}

			//Reservation in memory before long operation
			//Throw TimeMapTrayFullException when target tray is full (Check Time map before saving)
			_timeMapService.RegisterCreatedOrder(order);

			try
			{
				//Save the order
				_ordersDatabase.Orders.Create(order);
				foreach (var orderLine in order.OrderLines.ToList<OrderLine>())
				{
					_ordersDatabase.OrderLines.Create(orderLine);
				}
				_ordersDatabase.Save();
			}
			catch (DataBaseOperationException ex)
			{
				_timeMapService.UnegisterCreatedOrder(order);

				throw new ApplicationException("Order not saved due to problems with database.", ex);
			}

			var orderDTO = GetDTO(order);

			return orderDTO;
		}

		public bool ConfirmOrder(int orderId, string customerSecretCode)
		{
			var order = _ordersDatabase.Orders.Get(orderId);

			if (order == null)
			{
				throw new DataNotFoundException("Order not found.");
			}

			if (order.GetOrderExpirationDate(_config.OrderExpireTime.ToTimeSpan()) < DateTimeHelper.GetCurrentApplicationDateTimeValue())
			{
				throw new OrderExpiredException("Order expire.");
			}

			if (order.OrderStatus != Consts.OrderStatus.ORDER_CREATED)
			{
				throw new IncorrectOrderStatusException("Order status should be " + Consts.OrderStatus.ORDER_CREATED + " for this operation.");
			}

			if (order.CustomerSecretCode.ToUpper() == customerSecretCode.ToUpper()) //Not case sensitive
			{
				UpdateOrderStatus(order, Consts.OrderStatus.ORDER_CONFIRMED);

				return true;
			}
			else
			{
				return false;
			}
		}

		public void ConfirmOrderToReady(int orderId)
		{
			var order = _ordersDatabase.Orders.Get(orderId);

			if (order == null)
			{
				throw new DataNotFoundException("Order not found");
			}

			if (order.OrderStatus != Consts.OrderStatus.ORDER_CONFIRMED)
			{
				throw new IncorrectOrderStatusException("Order status should be " + Consts.OrderStatus.ORDER_CONFIRMED + " for this operation.");
			}

			UpdateOrderStatus(order, Consts.OrderStatus.ORDER_READY);
		}

		public void ReadyOrderToConfirm(int orderId)
		{
			var order = _ordersDatabase.Orders.Get(orderId);

			if (order == null)
			{
				throw new DataNotFoundException("Order not found");
			}

			if (order.OrderStatus != Consts.OrderStatus.ORDER_READY)
			{
				throw new IncorrectOrderStatusException("Order status should be " + Consts.OrderStatus.ORDER_READY + " for this operation.");
			}

			UpdateOrderStatus(order, Consts.OrderStatus.ORDER_CONFIRMED);
		}

		public void CompleteOrder(int orderId)
		{
			var order = _ordersDatabase.Orders.Get(orderId);

			if (order == null)
			{
				throw new DataNotFoundException("Order not found");
			}

			if (order.OrderStatus != Consts.OrderStatus.ORDER_READY && order.OrderStatus != Consts.OrderStatus.ORDER_CONFIRMED)
			{
				throw new IncorrectOrderStatusException(String.Format("Order status should be {0} or {1} for this operation.", Consts.OrderStatus.ORDER_CONFIRMED, Consts.OrderStatus.ORDER_READY));
			}

			UpdateOrderStatus(order, Consts.OrderStatus.ORDER_COMPLETED);
		}

		public void DenyOrder(int orderId)
		{
			var order = _ordersDatabase.Orders.Get(orderId);

			if (order == null)
			{
				throw new DataNotFoundException("Order not found");
			}

			UpdateOrderStatus(order, Consts.OrderStatus.ORDER_DENIED);
		}

		public void InvalidateOrder(int orderId)
		{
			var order = _ordersDatabase.Orders.Get(orderId);

			if (order == null)
			{
				throw new DataNotFoundException("Order not found");
			}

			UpdateOrderStatus(order, Consts.OrderStatus.ORDER_INVALID);
		}

		public OrderDTO GetOrder(int orderId)
		{
			var order = _ordersDatabase.Orders.Get(orderId);

			return GetDTO(order);
		}

		public IReadOnlyCollection<OrderDTO> GetTodayActiveOrders()
		{
			DateTime tomorrow = DateTimeHelper.GetCurrentApplicationDateTimeValue().AddDays(1).Date;
			DateTime current = DateTimeHelper.GetCurrentApplicationDateTimeValue().Date;
						
			var orders = _ordersDatabase.Orders
				.Find(order =>
					order.TargetStartDate >= current
						&& order.TargetEndDate < tomorrow
						&& (order.OrderStatus == Consts.OrderStatus.ORDER_CONFIRMED
							|| order.OrderStatus == Consts.OrderStatus.ORDER_READY)
					).ToList();

			var orderIds = orders.Select(order => order.OrderId).ToArray<int>();

			var orderLines = _ordersDatabase.OrderLines
				.Find(line =>
					orderIds.Contains(line.OrderId)
					).ToList();

			var result = MapOrdersWithLines(orders, orderLines)
				.Select(order => GetDTO(order));

			return result.ToList<OrderDTO>();
		}

		public void Dispose()
		{
			_ordersDatabase.Dispose();
			_contactService.Dispose();
		}

		private void UpdateOrderStatus(Order order, string newStatus)
		{
			string oldOrderStatus = order.OrderStatus;
			order.OrderStatus = newStatus;

			_timeMapService.UpdateOrderItemStatus(order);

			try
			{
				_ordersDatabase.Orders.Update(order);
				_ordersDatabase.Save();
			}
			catch (DataBaseOperationException ex)
			{
				order.OrderStatus = oldOrderStatus;
				_timeMapService.UpdateOrderItemStatus(order);   //Rollback status changes when save exception

				throw new ApplicationException("Error while saving changes to database. Status not updated.", ex);
			}
		}

		private OrderDTO GetDTO(Order order)
		{
			return new OrderDTO
			{
				OrderID = order.OrderId,
				CustomerPhoneNumber = order.CustomerPhoneNumber,
				CustomerSecretCode = order.CustomerSecretCode,
				TargetStartTime = DateTimeHelper.GetAsUserView(order.TargetStartDate.TimeOfDay, _config.ClientTimeZoneShift.ShiftMinutes),
				TargetEndTime = DateTimeHelper.GetAsUserView(order.TargetEndDate.TimeOfDay, _config.ClientTimeZoneShift.ShiftMinutes),
				TotalPrice = order.GetTotalPrice(),
				OrderLines = order.OrderLines.Select(l =>
					new OrderLineDTO
					{
						Name = l.Name,
						Price = l.Price
					}),
				OrderStatus = order.OrderStatus,
				CreationTime = DateTimeHelper.GetAsUserView(order.CreationDate, _config.ClientTimeZoneShift.ShiftMinutes),
				OrderLifeTimeMilliseconds = (int)_config.OrderExpireTime.ToTimeSpan().TotalMilliseconds
			};
		}

		private static List<Order> MapOrdersWithLines(IList<Order> orders, IList<OrderLine> lines)
		{

			var mappedOrders = orders.Select(order =>
				new Order
				{
					OrderId = order.OrderId,
					CustomerPhoneNumber = order.CustomerPhoneNumber,
					TargetStartDate = DateTimeHelper.GetWithApplicationKind(order.TargetStartDate),
					TargetEndDate = DateTimeHelper.GetWithApplicationKind(order.TargetEndDate),
					OrderLines = new List<OrderLine>(lines.Where(line =>
						order.OrderId == line.OrderId)),
					CustomerSecretCode = order.CustomerSecretCode,
					OrderIdentifier = order.OrderIdentifier,
					CreationDate = DateTimeHelper.GetWithApplicationKind(order.CreationDate),
					OrderStatus = order.OrderStatus
				});

			return mappedOrders.ToList<Order>();
		}
	}
}