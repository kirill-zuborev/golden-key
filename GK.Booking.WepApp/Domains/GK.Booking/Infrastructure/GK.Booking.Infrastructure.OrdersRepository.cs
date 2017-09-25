using GK.Booking.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Hosting;

namespace GK.Booking.Infrastructure
{
    public class OrdersRepository : IOrdersRepository
    {
        class OrderDataModel
        {
            public int ID { get; set; }

            public IEnumerable<OrderLineDataModel> Lines { get; set; }

            public decimal TotalPrice { get; set; }

            public DateTime CreationDate { get; set; }

            public DateTime TargetStartDate { get; set; }
            public DateTime TargetEndDate { get; set; }

            public string PhoneNumber { get; set; }
        }

        class OrderLineDataModel
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
        }

        private string _ordersFilePath;

        public OrdersRepository()
        {
            _ordersFilePath = HostingEnvironment.MapPath("~/orders.json");
        }

        public void Save(Order order)
        {
            var orders = ReadOrders().ToList();
            orders.Add(new OrderDataModel
            {
                ID = order.OrderId,
                Lines = order.OrderLines.Select(l => new OrderLineDataModel { Name = l.Name, Price = l.Price }),
                CreationDate = order.CreationDate,
                TargetStartDate = order.TargetStartDate,
                TargetEndDate = order.TargetEndDate,
                TotalPrice = order.GetTotalPrice(),
                PhoneNumber = order.CustomerPhoneNumber,
            });
            WriteOrders(orders);
        }

        public IEnumerable<Order> GetAll()
        {
            var json = File.ReadAllText(_ordersFilePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                return new Order[0];
            }

            var ordersData = JsonConvert.DeserializeObject<OrderDataModel[]>(json);

            var orders = GetPopulatedOrders(ordersData);

            return orders;
        }

        //public IEnumerable<Order> GetFromTimeSpan(DateTime startTime, DateTime endtime)
        //{
        //    var json = File.ReadAllText(_ordersFilePath);

        //    if (string.IsNullOrWhiteSpace(json))
        //    {
        //        return new Order[0];
        //    }

        //    var fileData = JsonConvert.DeserializeObject<OrderDataModel[]>(json);

            
        //    OrderDataModel[] ordersData = fileData.Where(d => d.TargetTime >= startTime && d.TargetTime <= endtime)
        //                        .Select(d =>
        //                            new OrderDataModel
        //                            {
        //                                ID = d.ID,
        //                                Lines = d.Lines.Select(l => new OrderLineDataModel { Name = l.Name, Price = l.Price }),
        //                                CustomerCode = d.CustomerCode,
        //                                TargetTime = d.TargetTime,
        //                                CreationTime = d.CreationTime,
        //                                TotalPrice = d.TotalPrice
        //                            }).ToArray();

        //    var orders = GetPopulatedOrders(ordersData);

        //    return orders;
        //}

        //private IEnumerable<Order> GetPopulatedOrders(OrderDataModel[] ordersSource)
        //{
        //    var orders = new List<Order>();

        //    foreach (var orderData in ordersSource)
        //    {
        //        var constructor = typeof(Order).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[0], null);
        //        var order = (Order)constructor.Invoke(null);

        //        order.GetType()
        //            .GetProperty("Lines")
        //            .SetValue(
        //                order,
        //                orderData.Lines.Select(l => new OrderLine(l.Name, l.Price)),
        //                null
        //            );

        //        order.GetType()
        //            .GetProperty("ID")
        //            .SetValue(
        //                order,
        //                orderData.ID,
        //                null
        //            );

        //        order.GetType()
        //            .GetProperty("CreationTime")
        //            .SetValue(
        //                order,
        //                orderData.CreationTime,
        //                null
        //            );

        //        order.GetType()
        //            .GetProperty("TargetTime")
        //            .SetValue(
        //                order,
        //                orderData.TargetTime,
        //                null
        //            );

        //        order.GetType()
        //            .GetProperty("TotalPrice")
        //            .SetValue(
        //                order,
        //                new Price(orderData.TotalPrice),
        //                null
        //            );

        //        order.GetType()
        //            .GetProperty("CustomerCode")
        //            .SetValue(
        //                order,
        //                new CustomerCode(orderData.CustomerCode),
        //                null
        //            );

        //        orders.Add(order);
        //    }

        //    return orders;
        //}

        private IEnumerable<OrderDataModel> ReadOrders()
        {
            var text = File.ReadAllText(_ordersFilePath);

            if (string.IsNullOrWhiteSpace(text))
            {
                return new OrderDataModel[0];
            }

            var orders = JsonConvert.DeserializeObject<OrderDataModel[]>(text);
            return orders;
        }

        private void WriteOrders(IEnumerable<OrderDataModel> orders)
        {
            var json = JsonConvert.SerializeObject(orders.ToArray());
            File.WriteAllText(_ordersFilePath, json);
        }
    }
}