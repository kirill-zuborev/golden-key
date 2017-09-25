using System;

namespace GK.Booking.Models.Exceptions
{
    public class PhoneIsLockedException : ApplicationException
    {
        public PhoneIsLockedException()
        {
        }

        public PhoneIsLockedException(string message) : base(message)
        {
        }
    }
}