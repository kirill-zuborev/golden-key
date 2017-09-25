using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GK.Booking.Models.Exceptions
{
	[Serializable]
	internal class NotAllItemsExistsInMenuException : Exception
	{
		public Menu Menu { get; private set; }
		public IEnumerable<string> NamesOfMenuItemsToInclude { get; private set; }
		public string MenuItemName { get; private set; }

		public NotAllItemsExistsInMenuException(IEnumerable<string> namesOfMenuItemsToInclude, Menu menu)
		{
			if (namesOfMenuItemsToInclude == null)
			{
				throw new ArgumentNullException("namesOfMenuItemsToInclude");
			}

			NamesOfMenuItemsToInclude = namesOfMenuItemsToInclude;

			if (menu == null)
			{
				throw new ArgumentNullException("menu");
			}

			Menu = menu;
		}

		public NotAllItemsExistsInMenuException(string menuItemName, Menu menu)
		{
			MenuItemName = menuItemName;

			if (menu == null)
			{
				throw new ArgumentNullException("menu");
			}

			Menu = menu;
		}

		protected NotAllItemsExistsInMenuException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}