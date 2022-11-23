using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Components {
	public class NavigationMenuViewComponent : ViewComponent {

		private readonly IStoreRepository repository;

		public NavigationMenuViewComponent(IStoreRepository repository) {
			this.repository = repository;
		}

		public IViewComponentResult Invoke() {
			return View(repository.Products
				.Select(p => p.Category)
				.Distinct()
				.OrderBy(x => x));
		}
	}
}
