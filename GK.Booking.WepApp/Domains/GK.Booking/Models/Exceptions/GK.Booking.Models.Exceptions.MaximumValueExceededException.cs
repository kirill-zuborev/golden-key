using System;

namespace GK.Booking.Models.Exceptions
{
    public class MaximumValueExceededException : ApplicationException
    {
        public MaximumValueExceededException()
        {
        }

        public MaximumValueExceededException(string message) : base(message)
        {
        }
    }
}