namespace SportsStore.Models {
	public class EFStoreRepository : IStoreRepository {

		private readonly StoreDbContext context;

		public EFStoreRepository(StoreDbContext context) {
			this.context = context;
		}

		public IQueryable<Product> Products => this.context.Products;
	}
}
