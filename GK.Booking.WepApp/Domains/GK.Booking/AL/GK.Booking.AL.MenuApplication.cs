using System;
using GK.Booking.AL.Services;

namespace GK.Booking.AL
{
	public class MenuApplication
	{
		private readonly IMenuService _menuService;

		public MenuApplication(IMenuService menuService)
		{
			if (menuService == null)
			{
				throw new ArgumentNullException(nameof(menuService));
			}

			_menuService = menuService;
		}

		public MenuDTO GetMenu()
		{
			return _menuService.GetMenu();
		}
	}
}