using System;

namespace GK.Booking.Models
{
	public interface IOrderUnitOfWork : IDisposable
	{
		IRepository<Order> Orders { get; }
		IRepository<OrderLine> OrderLines { get; }

		void Save();
	}
}
