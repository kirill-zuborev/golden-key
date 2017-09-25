using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Web;
using System.IO;
using GK.Booking.Models.Exceptions;


namespace GK.Booking.AL.Services
{
	public class MenuService : IMenuService
	{
		private readonly string _menuFilePath;

		public MenuService()
		{
			_menuFilePath = HttpContext.Current.Server.MapPath("~/menu.json");
		}

		public MenuDTO GetMenu()
		{
			IEnumerable<MenuItemDTO> menuItems;

			try
			{
				menuItems = JsonConvert.DeserializeObject<MenuItemDTO[]>(GetMenuItemsJson());
			}
			catch (ApplicationException ex)
			{
				throw new CanNotGetMenuException("Can not get menu. Please see inner exception", ex);
			}

			var menu = new MenuDTO { Items = menuItems };
			return menu;
		}

		private string GetMenuItemsJson()
		{
			var json = File.ReadAllText(_menuFilePath);
			return json;
		}
	}
}