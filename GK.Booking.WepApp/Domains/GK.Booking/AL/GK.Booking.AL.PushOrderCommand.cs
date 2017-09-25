namespace GK.Booking.AL
{
	using System;
	using System.Collections.Generic;

	using Infrastructure.Tools;

	public class PushOrderCommand
	{
		private string _phoneNumber;
		private DateTime _targetStartDate;
		private DateTime _targetEndDate;
		private IEnumerable<string> _menuItemsNames;

		public string PhoneNumber
			=> _phoneNumber;
		public DateTime TargetStartDate
			=> _targetStartDate;
		public DateTime TargetEndDate
			=> _targetEndDate;
		public IEnumerable<string> MenuItemsNames
			=> _menuItemsNames;

		public PushOrderCommand(string phoneNumber, TimeSpan targetStartTime, TimeSpan targetEndTime, IEnumerable<string> menuItemsNames)
		{
			if(string.IsNullOrEmpty(phoneNumber))
			{
				throw new ArgumentNullException(nameof(phoneNumber));
			}

			_phoneNumber = phoneNumber;

			_targetStartDate = DateTimeHelper.GetWithTodayDate(targetStartTime);
			_targetEndDate = DateTimeHelper.GetWithTodayDate(targetEndTime);

			_menuItemsNames = menuItemsNames;
		}
	}
}