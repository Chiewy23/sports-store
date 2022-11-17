using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;

namespace SportsStore.Tests;

public class HomeControllerTests
{
    [Fact]
    public void CanUseRepository()
    {
        // Arrange
        var mock = new Mock<IStoreRepository>();
        mock.Setup(m => m.Products).Returns((new Product[] {
                new Product { ProductID = 1, Name = "P1" },
				new Product { ProductID = 2, Name = "P2" }
			}).AsQueryable<Product>()
        );

        var controller = new HomeController(mock.Object);

        // Act
        var result = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

        // Assert
        var prodArray = result?.ToArray();
        Assert.True(prodArray?.Length == 2);
        Assert.Equal("P1", prodArray[0].Name);
		Assert.Equal("P2", prodArray[1].Name);
	}

    [Fact]
    public void CanPaginate() {
		// Arrange
		var mock = new Mock<IStoreRepository>();
		mock.Setup(m => m.Products).Returns((new Product[] {
				new Product { ProductID = 1, Name = "P1" },
				new Product { ProductID = 2, Name = "P2" },
				new Product { ProductID = 3, Name = "P3" },
				new Product { ProductID = 4, Name = "P4" },
				new Product { ProductID = 5, Name = "P5" }
			}).AsQueryable<Product>()
		);

		var controller = new HomeController(mock.Object);
		controller.pageSize = 3;

		// Act
		var result = (controller.Index(2) as ViewResult)?.ViewData.Model as IEnumerable<Product>;

		// Assert
		var prodArray = result?.ToArray();
		Assert.True(prodArray?.Length == 2);
		Assert.Equal("P4", prodArray[0].Name);
		Assert.Equal("P5", prodArray[1].Name);

	}
}