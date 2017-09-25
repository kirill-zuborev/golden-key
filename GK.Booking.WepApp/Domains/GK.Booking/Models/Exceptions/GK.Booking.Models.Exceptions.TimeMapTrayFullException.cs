using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GK.Booking.Models.Exceptions
{
    [Serializable]
    public class TimeMapTrayFullException : ApplicationException
    {
        public TimeMapTrayFullException()
        {
        }

        public TimeMapTrayFullException(string message) : base(message)
        {
        }
    }
}