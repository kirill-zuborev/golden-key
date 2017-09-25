using System;
using System.Linq;
using System.Collections.Generic;
using GK.Booking.Infrastructure.Tools;
using GK.Booking.Models.Exceptions;

namespace GK.Booking.Models
{
	public class OrderFactory
	{
		private readonly Menu _menu;

		public OrderFactory(Menu menu)
		{
			if (menu == null)
			{
				throw new ArgumentNullException(nameof(menu));
			}

			_menu = menu;
		}

		public Order CreateOrder(string phoneNumber, DateTime targetStartDate, DateTime targetEndDate, IEnumerable<string> namesOfMenuItemsToInclude)
		{
			var order = new Order
			{
				CustomerPhoneNumber = phoneNumber,
				CustomerSecretCode = SecretCodeGenerator.Generate(),
				TargetStartDate = targetStartDate,
				TargetEndDate = targetEndDate,
				OrderStatus = Consts.OrderStatus.ORDER_CREATED,
				OrderIdentifier = Guid.NewGuid().ToString(),
				CreationDate = DateTimeHelper.GetCurrentApplicationDateTimeValue()
			};

			var orderLines = new List<OrderLine>();

			foreach (var menuItemName in namesOfMenuItemsToInclude.ToList<string>())
			{
				MenuItem menuItem;

				try
				{
					menuItem = _menu.GetItemByName(menuItemName);
				}
				catch (MenuItemNotFoundException)
				{
					throw new NotAllItemsExistsInMenuException(namesOfMenuItemsToInclude, _menu);
				}

				var orderLine = new OrderLine(menuItem.Name, menuItem.Price);
				orderLine.Order = order;

				orderLines.Add(orderLine);
			}

			order.OrderLines = orderLines;

			return order;
		}
	}
}