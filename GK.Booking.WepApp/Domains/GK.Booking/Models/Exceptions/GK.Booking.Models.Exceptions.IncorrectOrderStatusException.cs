using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GK.Booking.Models.Exceptions
{
    public class IncorrectOrderStatusException : ApplicationException
    {
        public IncorrectOrderStatusException()
        {
        }

        public IncorrectOrderStatusException(string message) : base(message)
        {
        }
    }
}