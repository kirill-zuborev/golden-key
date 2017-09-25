using System;

namespace GK.Booking.Models
{
    public class ConfigurationException : ApplicationException
    {
        public ConfigurationException()
        {
        }

        public ConfigurationException(string message) : base(message)
        {
        }
    }
}