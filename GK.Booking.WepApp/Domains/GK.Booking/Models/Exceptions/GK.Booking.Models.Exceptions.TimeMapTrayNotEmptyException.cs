using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GK.Booking.Models.Exceptions
{
    [Serializable]
    public class TimeMapTrayNotEmptyException : ApplicationException
    {
        public TimeMapTrayNotEmptyException()
        {
        }

        public TimeMapTrayNotEmptyException(string message) : base(message)
        {
        }
    }
}