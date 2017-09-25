using System;

namespace GK.Booking.Models.Exceptions
{
    public class OrderExpiredException : ApplicationException
    {
        public OrderExpiredException()
        {
        }

        public OrderExpiredException(string message) : base(message)
        {
        }
    }
}