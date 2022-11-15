namespace SportsStore.Models {
	public interface IStoreRepository {
		/*
		 * Represents a collection of objects which can
		 * be queried, such as those managed by a db.
		 * 
		 * The IQueryable<T> interface allows a collection
		 * of objects to be queried efficiently. Once use case
		 * is the ability to retrieve a subset of objects using
		 * standard LINQ statements.
		 * 
		 * Without this interface, we would have to retrieve all
		 * the objects first and then discard the ones we
		 * don't want.
		 * 
		 * However, note that each time a collection of objects is
		 * enumerated, the query will be evaluated again, meaning a
		 * new query is sent to the database. This can undermine the
		 * efficiency of using IQueryable<T>.
		 * 
		 * In such situations, convert IQueryable<T> to a more
		 * predictable form using ToList or ToArray extension
		 * method.
		 */
		IQueryable<Product> Products { get; }
	}
}
