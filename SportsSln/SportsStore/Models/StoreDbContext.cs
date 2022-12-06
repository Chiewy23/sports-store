using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models {
	public class StoreDbContext : DbContext {
		public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }

		/*
		 * Provides access to the Products objects in
		 * the database.
		 */
		public DbSet<Product> Products { get; set;}
		public DbSet<Order> Orders { get; set;}
	}
}
