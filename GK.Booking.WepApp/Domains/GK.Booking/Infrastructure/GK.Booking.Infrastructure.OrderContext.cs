using System.Data.Entity;
using GK.Booking.Models;

namespace GK.Booking.Infrastructure
{
	public class OrderContext : DbContext
	{
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderLine> OrderLines { get; set; }

		public OrderContext(string connectionString)
			: base(connectionString)
		{
		}
	}
}