using System;

namespace GK.Booking.Models.Exceptions
{
    public class MessageNotSentException : ApplicationException
    {
        public MessageNotSentException()
        {
        }

        public MessageNotSentException(string message) : base(message)
        {
        }

        public MessageNotSentException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}