using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Components {
	public class NavigationMenuViewComponent : ViewComponent {

		private readonly IStoreRepository repository;

		public NavigationMenuViewComponent(IStoreRepository repository) {
			this.repository = repository;
		}

		/**
		 * The ViewComponent base class provides access to context objects through
		 * a set of properties, one of which is RouteData. This provides info about
		 * how the request URL was handled by the routing system.
		 * 
		 * We can use this to get the value of the currently selected category
		 * and pass it to the view.
		 * 
		 * In most cases we would do so by creating another view model class, but 
		 * we can also use the view bag feature, which allows unstructured data to 
		 * be passed to a view alongside the view model object.
		 */
		public IViewComponentResult Invoke() {
			ViewBag.SelectedCategory = RouteData?.Values["category"];
			return View(repository.Products
				.Select(p => p.Category)
				.Distinct()
				.OrderBy(p => p));
		}
	}
}
