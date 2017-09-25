using System.Data.Entity;
using GK.Booking.Models;

namespace GK.Booking.Infrastructure
{
	public class ContactContext : DbContext
	{
		public DbSet<LockedPhone> LockedPhones { get; set; }

		public ContactContext(string connectionString)
			: base(connectionString)
		{
		}
	}
}