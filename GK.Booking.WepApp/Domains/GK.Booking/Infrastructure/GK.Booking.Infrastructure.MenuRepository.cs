using GK.Booking.AL;
using GK.Booking.Models;
using GK.Booking.Models.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace GK.Booking.Infrastructure
{
	public class MenuRepository
	{
		private string _menuFilePath;

		public MenuRepository()
		{
			_menuFilePath = HostingEnvironment.MapPath("~/menu.json");
		}

		public Menu Get()
		{
			IEnumerable<MenuItemDTO> menuItems;

			try
			{
				menuItems = JsonConvert.DeserializeObject<MenuItemDTO[]>(GetMenuItemsJson());
			}
			catch (ApplicationException ex)
			{
				throw new CanNotGetMenuException("Can not get menu. Please see inner exception.", ex);
			}

			var menuDTO = new MenuDTO { Items = menuItems };

			var menu = new Menu(menuItems.Select(i => new MenuItem(i.Name, i.Price)));

			return menu;
		}

		private string GetMenuItemsJson()
		{
			var json = File.ReadAllText(_menuFilePath);
			return json;
		}
	}
}