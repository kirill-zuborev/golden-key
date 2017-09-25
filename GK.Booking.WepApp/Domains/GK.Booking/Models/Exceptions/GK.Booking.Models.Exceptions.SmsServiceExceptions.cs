using System;

namespace GK.Booking.Models.Exceptions
{
	public class SmsServiceException : ApplicationException
	{
	}

	public class LowBalanceException : SmsServiceException
	{
	}

	public class ClientConfigurationException : SmsServiceException
	{
		public string InvalidConfigurationParameterName { get; }

		public ClientConfigurationException()
		{

		}

		public ClientConfigurationException(string invalidConfigurationPropertyName)
		{
			InvalidConfigurationParameterName = invalidConfigurationPropertyName;
		}
	}

	public class IncorrectDestinationNumberException : SmsServiceException
	{
	}

	public class OtherServiceException : SmsServiceException
	{
		public string AdditionalInformation { get; }

		public OtherServiceException()
		{

		}

		public OtherServiceException(string additionalInformation)
		{
			AdditionalInformation = additionalInformation;
		}
	}
}