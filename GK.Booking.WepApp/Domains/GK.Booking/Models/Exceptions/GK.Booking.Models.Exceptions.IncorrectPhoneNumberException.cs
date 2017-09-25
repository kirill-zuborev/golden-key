using System;

namespace GK.Booking.Models.Exceptions
{
    public class IncorrectPhoneNumberException : ApplicationException
    {
        public IncorrectPhoneNumberException()
        {
        }

        public IncorrectPhoneNumberException(string message) : base(message)
        {
        }
    }
}