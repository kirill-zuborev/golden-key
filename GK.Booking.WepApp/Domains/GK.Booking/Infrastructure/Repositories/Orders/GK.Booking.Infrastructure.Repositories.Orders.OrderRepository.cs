using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using GK.Booking.Models;

namespace GK.Booking.Infrastructure.Repositories.Orders
{
	public class OrderRepository : IRepository<Order>
	{
		private readonly OrderContext _db;

		public OrderRepository(OrderContext context)
		{
			this._db = context;
		}

		public IReadOnlyCollection<Order> GetAll()
		{
			return _db.Orders.ToArray<Order>();
		}

		public Order Get(int id)
		{
			return _db.Orders.Find(id);
		}

		public void Create(Order order)
		{
			_db.Orders.Add(order);
		}

		public void Update(Order order)
		{
			_db.Entry(order).State = EntityState.Modified;
		}

		public IEnumerable<Order> Find(Expression<Func<Order, Boolean>> predicate)
		{
			var result = _db.Orders.Where(predicate).ToList();
			return result;
		}

		public void Delete(int id)
		{
			Order order = _db.Orders.Find(id);
			if (order != null)
				_db.Orders.Remove(order);
		}
	}
}