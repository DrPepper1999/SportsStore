using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{
    public class RepositoryTests
    {
        [Fact]
        public void CanPaginate()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {Id = 1, Name = "P1"},
                new Product {Id = 2, Name = "P2"},
                new Product {Id = 3, Name = "P3"},
                new Product {Id = 4, Name = "P4"},
                new Product {Id = 5, Name = "P5"},

            });
            var controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // Act
            var result = (ProductsListViewModel)controller.List(null, 2).Model;

            // Assert
            var prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal(prodArray[0].Name, "P4");
            Assert.Equal(prodArray[1].Name, "P5");
        }
    }
}