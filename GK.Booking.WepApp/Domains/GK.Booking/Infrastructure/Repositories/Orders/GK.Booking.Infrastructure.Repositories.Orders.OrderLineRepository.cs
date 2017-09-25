using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using GK.Booking.Models;

namespace GK.Booking.Infrastructure.Repositories.Orders
{
	public class OrderLineRepository : IRepository<OrderLine>
	{
		private OrderContext _db;

		public OrderLineRepository(OrderContext context)
		{
			this._db = context;
		}

		public IReadOnlyCollection<OrderLine> GetAll()
		{
			return _db.OrderLines.Include(o => o.Order).ToArray<OrderLine>();
		}

		public OrderLine Get(int id)
		{
			return _db.OrderLines.Find(id);
		}

		public void Create(OrderLine orderLine)
		{
			_db.OrderLines.Add(orderLine);
		}

		public void Update(OrderLine orderLine)
		{
			_db.Entry(orderLine).State = EntityState.Modified;
		}

		public IEnumerable<OrderLine> Find(Expression<Func<OrderLine, Boolean>> predicate)
		{
			var result = _db.OrderLines.Include(o => o.Order).Where(predicate).ToList();
			return result;
		}

		public void Delete(int id)
		{
			OrderLine orderLine = _db.OrderLines.Find(id);
			if (orderLine != null)
				_db.OrderLines.Remove(orderLine);
		}
	}
}