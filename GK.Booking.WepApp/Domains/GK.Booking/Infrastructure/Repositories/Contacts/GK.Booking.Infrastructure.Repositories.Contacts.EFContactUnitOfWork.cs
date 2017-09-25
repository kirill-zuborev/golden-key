using System;
using GK.Booking.Models;

namespace GK.Booking.Infrastructure.Repositories.Contacts
{
	public class EFContactUnitOfWork : IContactUnitOfWork
	{
		private readonly ContactContext _db;
		private LockedPhoneRepository _lockedPhoneRepo;

		public EFContactUnitOfWork(string connectionString)
		{
			_db = new ContactContext(connectionString);
		}

		public IRepository<LockedPhone> LockedPhones
		{
			get
			{
				if (_lockedPhoneRepo == null)
					_lockedPhoneRepo = new LockedPhoneRepository(_db);
				return _lockedPhoneRepo;
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
				throw new DataBaseOperationException("LockedPhone is not saved.", ex);
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