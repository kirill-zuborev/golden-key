using System;

namespace GK.Booking.Models
{
	public interface IContactUnitOfWork : IDisposable
	{
		IRepository<LockedPhone> LockedPhones { get; }

		void Save();
	}
}