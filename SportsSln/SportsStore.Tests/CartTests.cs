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

		[Fact]
		public void CanRemoveLine() {
			// Arrange
			var p1 = new Product { ProductID = 1, Name = "P1" };
			var p2 = new Product { ProductID = 2, Name = "P2" };
			var p3 = new Product { ProductID = 3, Name = "P3" };

			var target = new Cart();
			target.AddItem(p1, 1);
			target.AddItem(p2, 3);
			target.AddItem(p3, 5);
			target.AddItem(p2, 1);

			// Act
			target.RemoveLine(p2);

			// Assert
			Assert.Empty(target.Lines.Where(x => x.Product == p2));
			Assert.Equal(2, target.Lines.Count());
		}
	}
}
