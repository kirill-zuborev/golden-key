namespace GK.Booking.AL
{
	public static class ErrorCodesConst
	{
		public const int UNDEFINED_ERROR = 0;
		#region Contact exceptions
		public const int INVALID_PHONE_NUMBER = 10;
		#endregion
		#region Message sent exceptions
		public const int MESSAGE_NOT_SENT = 11;
		#endregion
		#region Booking process exceptions
		public const int PHONE_NUMBER_LOCKED = 12;
		public const int ORDER_MAXIMUM_PRICE_EXCEEDED = 13;
		public const int TARGET_TIME_MAP_TRAY_IS_FULL = 14;
		public const int CURRENT_STATUS_NOT_CORRECT_FOR_OPERATION = 15;
		public const int ORDER_EXPIRED = 16;
		#endregion

		#region Login errors
		public const int WRONG_USER_CREDENTIALS = 17;
		#endregion
	}
}