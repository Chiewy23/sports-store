using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers {
	public class HomeController : Controller {
		private readonly IStoreRepository repository;

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

		public IActionResult Index() => View(repository.Products);
	}
}
