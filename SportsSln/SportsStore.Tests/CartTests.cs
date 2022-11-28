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
	}
}
