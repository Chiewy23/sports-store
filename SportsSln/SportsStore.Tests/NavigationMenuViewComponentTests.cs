using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using Moq;
using SportsStore.Components;
using SportsStore.Models;
using System.Collections;

namespace SportsStore.Tests {
	public class NavigationMenuViewComponentTests {
		[Fact]
		public void CanSelectCategories() {
			// Arrange
			var mock = new Mock<IStoreRepository>();
			mock.Setup(m => m.Products).Returns(new Product[] {
				new Product { ProductID = 1, Name = "P1", Category = "Apples" },
				new Product { ProductID = 2, Name = "P2", Category = "Apples" },
				new Product { ProductID = 3, Name = "P3", Category = "Plums" },
				new Product { ProductID = 4, Name = "P4", Category = "Oranges" }
			}.AsQueryable<Product>());

			var target = new NavigationMenuViewComponent(mock.Object);

			// Act
			var results = ((IEnumerable<string>)(target.Invoke() as ViewViewComponentResult).ViewData.Model).ToArray();

			// Assert
			Assert.True(Enumerable.SequenceEqual(new string[] { "Apples", "Oranges", "Plums"}, results ?? new string[4]));
		}

		[Fact]
		public void IndicatesSelectedCategory() {
			// Arrange
			var categoryToSelect = "Apples";
			var mock = new Mock<IStoreRepository>();
			mock.Setup(m => m.Products).Returns(new Product[] {
				new Product { ProductID = 1, Name = "P1", Category = "Apples" },
				new Product { ProductID = 3, Name = "P3", Category = "Plums" },
				new Product { ProductID = 4, Name = "P4", Category = "Oranges" }
			}.AsQueryable<Product>());

			var target = new NavigationMenuViewComponent(mock.Object);
			target.ViewComponentContext = new ViewComponentContext {
				ViewContext = new ViewContext {
					RouteData = new RouteData()
				}
			};
			target.RouteData.Values["category"] = categoryToSelect;

			// Action
			var result = (string)(target.Invoke() as ViewViewComponentResult).ViewData["SelectedCategory"];

			// Assert
			Assert.Equal(categoryToSelect, result);
		}
	}
}
