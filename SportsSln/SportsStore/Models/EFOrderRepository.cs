using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models {
	public class EFOrderRepository : IOrderRepository {

		private StoreDbContext context;

		public EFOrderRepository(StoreDbContext ctx) {
			this.context = ctx;
		}

		/*
		 * Using Include and ThenInclude methods to specify that, when
		 * an Order object is read from the database, the collection
		 * associated with the Lines property should also be loaded along
		 * with each Product object associated with each collection object.
		 */
		public IQueryable<Order> Orders => context.Orders.Include(x => x.Lines).ThenInclude(p => p.Product);

		public void SaveOrder(Order order) {
			context.AttachRange(order.Lines.Select(x => x.Product));
			if (order.OrderID == 0) {
				context.Orders.Add(order);
			}

			context.SaveChanges();
		}
	}
}
