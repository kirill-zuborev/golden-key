using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using GK.Booking.Models;

namespace GK.Booking.Infrastructure.Repositories.Contacts
{
	public class LockedPhoneRepository : IRepository<LockedPhone>
	{
		private readonly ContactContext _db;

		public LockedPhoneRepository(ContactContext context)
		{
			this._db = context;
		}

		public IReadOnlyCollection<LockedPhone> GetAll()
		{
			return _db.LockedPhones.ToArray<LockedPhone>();
		}

		public LockedPhone Get(int id)
		{
			return _db.LockedPhones.Find(id);
		}

		public IEnumerable<LockedPhone> Find(Expression<Func<LockedPhone, Boolean>> predicate)
		{
			var result = _db.LockedPhones.Where(predicate).ToList();
			return result;
		}

		public void Create(LockedPhone item)
		{
			_db.LockedPhones.Add(item);
		}

		public void Update(LockedPhone item)
		{
			_db.Entry(item).State = EntityState.Modified;
		}

		public void Delete(int id)
		{
			LockedPhone lockedPhone = _db.LockedPhones.Find(id);
			if (lockedPhone != null)
				_db.LockedPhones.Remove(lockedPhone);
		}
		private static Expression<Func<T, bool>> FuncToExpression<T>(Func<T, Boolean> func)
		{
			return x => func(x);
		}
	}
}