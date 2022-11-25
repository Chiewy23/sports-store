using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers {
	public class HomeController : Controller {
		private readonly IStoreRepository repository;
		public int PageSize = 4;

		/**
		 * When ASP.NET Core creates a new instance of the HomeController
		 * class to handle a HTTP request, it inspects the contructor and
		 * sees it requires an object which implements the IStoreRepository
		 * interface.
		 * 
		 * To determine which implementation class should be used, ASP.NET
		 * Core consults the configuration in the Program class which (in this case)
		 * tells it to create a new instance of EFStoreRepository for every request.
		 * 
		 * This process is known as dependency injection.
		 */
		public HomeController(IStoreRepository repository) {
			this.repository = repository;
		}

		public IActionResult Index(string category, int productPage = 1) 
			=> View(new ProductsListViewModel {
				Products = repository.Products
					.Where(p => category == null || p.Category == category)
					.OrderBy(p => p.ProductID)
					.Skip((productPage - 1) * PageSize)
					.Take(PageSize),
				PagingInfo = new PagingInfo {
					CurrentPage= productPage,
					ItemsPerPage= PageSize,
					TotalItems= category == null ? repository.Products.Count() : repository.Products.Where(e => e.Category== category).Count()
				},
				CurrentCategory = category
			});
	}
}
