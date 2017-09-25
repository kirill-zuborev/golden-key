using System;
using System.Runtime.Serialization;

namespace GK.Booking.Models
{
	[Serializable]
	internal class MenuItemNotFoundException : ApplicationException
	{
		public string MenuItemName { get; private set; }
		public Menu Menu { get; set; }

		public MenuItemNotFoundException(string menuItemName, Menu menu) : base("Menu item not found in menu.")
		{
			if (string.IsNullOrWhiteSpace(menuItemName))
			{
				throw new ArgumentNullException("menuItemName");
			}

			MenuItemName = menuItemName;

			if (menu == null)
			{
				throw new ArgumentNullException("menu");
			}

			Menu = menu;
		}
		protected MenuItemNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}