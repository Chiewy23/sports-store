using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers {
	public class OrderController : Controller {

		private readonly IOrderRepository repository;
		private readonly Cart cart;

		public OrderController(IOrderRepository repository, Cart cart) {
			this.repository = repository;
			this.cart = cart;
		}

		public IActionResult Checkout() {
			return View(new Order());
		}

		[HttpPost]
		public IActionResult Checkout(Order order) {
			if (cart.Lines.Count() == 0) {
				ModelState.AddModelError("", "Sorry, your cart is empty!");
			}

			if (ModelState.IsValid) {
				order.Lines = cart.Lines;
				repository.SaveOrder(order);
				cart.Clear();

				return RedirectToPage("/Completed", new { orderId = order.OrderID });
			} else {
				return View();
			}
		}
	}
}
