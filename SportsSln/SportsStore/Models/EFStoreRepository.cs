namespace SportsStore.Models {
	public class EFStoreRepository : IStoreRepository {

		private readonly StoreDbContext context;

		public EFStoreRepository(StoreDbContext context) {
			this.context = context;
		}

		/*
		 * Maps the products property defined by the IStoreRepository 
		 * interface onto the Products property defined by the StoreDbContext
		 * class.
		 * 
		 * The Products property in the context class returns a DbSet<Product>
		 * object, which implements the IQueryable<T> interface.
		 */
		public IQueryable<Product> Products => this.context.Products;

		public void CreateProduct(Product product) {
			context.Add(product);
			context.SaveChanges();
		}

		public void DeleteProduct(Product product) {
			context.Remove(product);
			context.SaveChanges();
		}

		public void SaveProduct(Product product) {
			context.SaveChanges();
		}
	}
}
