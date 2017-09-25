using GK.Booking.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GK.Booking.Models
{
	[Serializable]
	internal class NotAllItemsExistsInMenuException : Exception
	{
		public Menu Menu { get; private set; }
		public IEnumerable<string> NamesOfMenuItemsToInclude { get; private set; }

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

		protected NotAllItemsExistsInMenuException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}