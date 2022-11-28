using SportsStore.Models;

namespace SportsStore.Tests {
	public class CartTests {

		[Fact]
		public void CanAddNewLines() {
			// Arrange
			var p1 = new Product { ProductID = 1, Name = "P1" };
			var p2 = new Product { ProductID = 2, Name = "P2" };

			var target = new Cart();

			// Act
			target.AddItem(p1, 1);
			target.AddItem(p2, 1);
			var results = target.Lines.ToArray();

			// Assert
			Assert.Equal(2, results.Length);
			Assert.Equal(p1, results[0].Product);
			Assert.Equal(p2, results[1].Product);
		}

		[Fact]
		public void CanAddQuantityForExistingLines() {
			// Arrange
			var p1 = new Product { ProductID = 1, Name = "P1" };
			var p2 = new Product { ProductID = 2, Name = "P2" };

			var target = new Cart();

			// Act
			target.AddItem(p1, 1);
			target.AddItem(p2, 1);
			target.AddItem(p1, 10);
			var results = target.Lines.OrderBy(x => x.Product.ProductID).ToArray();

			// Assert
			Assert.Equal(2, results.Length);
			Assert.Equal(11, results[0].Quantity);
			Assert.Equal(1, results[1].Quantity);
		}
	}
}
