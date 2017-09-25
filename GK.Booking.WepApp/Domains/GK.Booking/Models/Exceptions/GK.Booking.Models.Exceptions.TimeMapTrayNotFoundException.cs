using System;

namespace GK.Booking.Models.Exceptions
{
    [Serializable]
    public class TimeMapTrayNotFoundException : ApplicationException
    {
        public TimeMapTrayNotFoundException()
        {
        }

        public TimeMapTrayNotFoundException(string message) : base(message)
        {
        }
    }
}