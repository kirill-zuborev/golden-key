using System;
using GK.Booking.Models;

namespace GK.Booking.Infrastructure.Repositories.Orders
{
	public class EFOrderUnitOfWork : IOrderUnitOfWork
	{
		private readonly OrderContext _db;
		private OrderRepository _orderRepo;
		private OrderLineRepository _orderLineRepo;

		public EFOrderUnitOfWork(string connectionString)
		{
			_db = new OrderContext(connectionString);
		}

		public IRepository<Order> Orders
		{
			get
			{
				if (_orderRepo == null)
					_orderRepo = new OrderRepository(_db);
				return _orderRepo;
			}
		}

		public IRepository<OrderLine> OrderLines
		{
			get
			{
				if (_orderLineRepo == null)
					_orderLineRepo = new OrderLineRepository(_db);
				return _orderLineRepo;
			}
		}
		public void Save()
		{
			try
			{
				_db.SaveChanges();
			}
			catch (ApplicationException ex)
			{
				throw new DataBaseOperationException("Order is not saved.", ex);
			}
		}

		#region IDisposable implementation
		private bool _disposed = false;

		public virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (disposing)
				{
					_db.Dispose();
				}
				this._disposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}