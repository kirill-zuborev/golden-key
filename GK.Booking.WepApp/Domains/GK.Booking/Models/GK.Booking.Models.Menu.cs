using System;
using System.Collections.Generic;
using System.Linq;
using GK.Booking.Models.Exceptions;

namespace GK.Booking.Models
{
	public class Menu
	{
		private IEnumerable<MenuItem> _items;

		public Menu(IEnumerable<MenuItem> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException(nameof(items));
			}

			_items = items;
		}

		public MenuItem GetItemByName(string itemName)
		{
			var item = _items.SingleOrDefault(item1 => item1.Name == itemName);

			if (item == null)
			{
				throw new MenuItemNotFoundException(itemName, this);
			}

			return item;
		}
	}
}