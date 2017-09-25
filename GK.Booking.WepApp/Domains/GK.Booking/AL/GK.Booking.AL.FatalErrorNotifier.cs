namespace GK.Booking.AL
{
	using System;

	public static class FatalErrorNotifier
	{
		public delegate void FatalErrorHandler(object sender, FatalErrorEventArgs args);

		public static event FatalErrorHandler OnFatalError;

		public static void NotifyOnError(string errorMessage)
		{
			var args = new FatalErrorEventArgs() { ErrorMessage = errorMessage };

			OnFatalError?.Invoke(null, args);
		}

		public class FatalErrorEventArgs : EventArgs
		{
			public string ErrorMessage { get; set; }
		}
	}
}