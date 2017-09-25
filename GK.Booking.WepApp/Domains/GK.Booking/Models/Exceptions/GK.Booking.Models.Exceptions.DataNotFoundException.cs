using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GK.Booking.Models.Exceptions
{
    public class DataNotFoundException : ApplicationException
    {
        public DataNotFoundException()
        {
        }

        public DataNotFoundException(string message) : base(message)
        {
        }
    }
}