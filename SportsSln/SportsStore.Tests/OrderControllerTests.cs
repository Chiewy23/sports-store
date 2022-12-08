using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;

namespace SportsStore.Tests {
	public class OrderControllerTests {
		[Fact]
		public void CannotCheckoutEmptyCart() {
			// Arrange
			var mock = new Mock<IOrderRepository>();
			var cart = new Cart();
			var order = new Order();
			var target = new OrderController(mock.Object, cart);

			// Act
			var result = target.Checkout(order) as ViewResult;

			// Assert
			mock.Verify(x => x.SaveOrder(It.IsAny<Order>()), Times.Never);
			Assert.True(string.IsNullOrEmpty(result.ViewName));
			Assert.False(result.ViewData.ModelState.IsValid);
		}

		[Fact]
		public void CannotCheckoutInvalidShippingDetails() {
			// Arrange
			var mock = new Mock<IOrderRepository>();
			var cart = new Cart();
			cart.AddItem(new Product(), 1);

			var target = new OrderController(mock.Object, cart);
			target.ModelState.AddModelError("error", "error");

			// Act
			var result = target.Checkout(new Order()) as ViewResult;

			// Assert
			mock.Verify(x => x.SaveOrder(It.IsAny<Order>()), Times.Never);
			Assert.True(string.IsNullOrEmpty(result.ViewName));
			Assert.False(result.ViewData.ModelState.IsValid);
		}
	}
}
