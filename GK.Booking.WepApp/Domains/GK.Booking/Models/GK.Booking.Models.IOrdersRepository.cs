using System;
using System.Collections.Generic;
using GK.Booking.Models;

namespace GK.Booking.Models
{
    public interface IOrdersRepository
    {
        IEnumerable<Order> GetAll();
        IEnumerable<Order> GetFromTimeSpan(DateTime startTime, DateTime endTime);
        void Save(Order order);
    }
}