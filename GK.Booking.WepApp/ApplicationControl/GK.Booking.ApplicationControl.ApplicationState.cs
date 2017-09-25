using System;
using System.IO;
using System.Web;
using System.Runtime.Serialization.Formatters.Binary;
using GK.Booking.AL;

namespace GK.Booking.ApplicationControl
{
	public class ApplicationState
	{
		private readonly string _stateFilePath = HttpContext.Current.Server.MapPath("~/application_state.dat");
		private InstanceState _state;

		public bool IsBookingClientEnabled
		{
			get { return _state.IsBookingClientEnabled; }
		}

		public string DisabledReasonText
		{
			get { return _state.DisabledReasonText; }
		}

		public ApplicationState()
		{
			Init();

			FatalErrorNotifier.OnFatalError += new FatalErrorNotifier.FatalErrorHandler(OnApplicationFatalError);
		}

		public void SetBookingClientEnabled()
		{
			_state.IsBookingClientEnabled = true;
			_state.DisabledReasonText = null;
			SaveInstanceState();
		}

		public void SetBookingClientDisabled(string disabledReasonText)
		{
			_state.IsBookingClientEnabled = false;
			_state.DisabledReasonText = disabledReasonText;
			SaveInstanceState();
		}

		private void OnApplicationFatalError(object sender, FatalErrorNotifier.FatalErrorEventArgs args)
		{
			SetBookingClientDisabled(args.ErrorMessage);
		}

		private void Init()
		{
			_state = GetInstanceState();
		}

		private InstanceState GetInstanceState()
		{
			BinaryFormatter formatter = new BinaryFormatter();
			InstanceState returnedInstanceState = new InstanceState(true, null);
			bool stateFileNotExists = true;

			using (FileStream fStream = new FileStream(_stateFilePath, FileMode.OpenOrCreate))
			{
				if (fStream.Length != 0 && formatter.Deserialize(fStream) is InstanceState)
				{
					fStream.Position = 0;
					returnedInstanceState = formatter.Deserialize(fStream) as InstanceState;

					stateFileNotExists = false;
				}
			}

			if (stateFileNotExists)
			{
				SaveInstanceState();
			}

			return returnedInstanceState;
		}

		private void SaveInstanceState()
		{
			InstanceState state = new InstanceState(IsBookingClientEnabled, DisabledReasonText);
			BinaryFormatter formatter = new BinaryFormatter();

			using (FileStream fStream = new FileStream(_stateFilePath, FileMode.OpenOrCreate))
			{
				formatter.Serialize(fStream, state);
			}
		}

		[Serializable]
		private class InstanceState
		{
			public InstanceState(bool isBookingClientEnabled, string disabledReasonText)
			{
				IsBookingClientEnabled = isBookingClientEnabled;
				DisabledReasonText = disabledReasonText;
			}

			public bool IsBookingClientEnabled { get; set; }

			public string DisabledReasonText { get; set; }
		}
	}
}