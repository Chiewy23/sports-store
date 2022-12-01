using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Moq;
using SportsStore.Models;
using SportsStore.Pages;
using System.Text;
using System.Text.Json;

namespace SportsStore.Tests {
	public class CartPageTests {
		[Fact]
		public void CanLoadCart() {
			// Arrange
			var p1 = new Product { ProductID = 1, Name = "P1" };
			var p2 = new Product { ProductID = 2, Name = "P2" };
			
			var mockRepo = new Mock<IStoreRepository>();
			mockRepo.Setup(m => m.Products).Returns((new Product[] {
				p1, p2
			}).AsQueryable<Product>());

			var testCart = new Cart();
			testCart.AddItem(p1, 1);
			testCart.AddItem(p2, 1);

			var mockSession = new Mock<ISession>();
			byte[] data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(testCart));
			mockSession.Setup(c => c.TryGetValue(It.IsAny<string>(), out data));

			var mockContext = new Mock<HttpContext>();
			mockContext.SetupGet(c => c.Session).Returns(mockSession.Object);

			// Action
			var cartModel = new CartModel(mockRepo.Object) {
				PageContext = new PageContext(new ActionContext {
					HttpContext = mockContext.Object,
					RouteData = new RouteData(),
					ActionDescriptor = new PageActionDescriptor()
				})
			};
			cartModel.OnGet("myUrl");

			// Assert
			Assert.Equal(2, cartModel.Cart.Lines.Count);
			Assert.Equal("myUrl", cartModel.ReturnUrl);
		}
	}
}
