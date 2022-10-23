using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;
using SportsStore.WebUI.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;
using Microsoft.AspNetCore.Mvc;

namespace SportsStore.UnitTests
{
    public class ProductsTests
    {
        [Fact]
        public void CanSendPaginationViewModel()
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

            // Assers
            var PagingInfo = result.PagingInfo;
            Assert.Equal(PagingInfo.CurrentPage, 2);
            Assert.Equal(PagingInfo.ItemsPerPage, 3);
            Assert.Equal(PagingInfo.TotalItems, 5);
            Assert.Equal(PagingInfo.TotalPages, 2);
        }

        [Fact]
        public void CanFilterProducts()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {Id = 1, Name = "P1", Category = "Cat1"},
                new Product {Id = 2, Name = "P2", Category = "Cat2"},
                new Product {Id = 3, Name = "P3", Category = "Cat1"},
                new Product {Id = 4, Name = "P4", Category = "Cat2"},
                new Product {Id = 5, Name = "P5", Category = "Cat3"},
            });

            var controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // Act
            var result = ((ProductsListViewModel)controller.List("Cat2", 1).Model)
                .Products.ToArray();

            //Assert
            Assert.Equal(result.Length, 2);
            Assert.True(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.True(result[1].Name == "P4" && result[0].Category == "Cat2");
        }

        [Fact]
        public void CanCreateCategories()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {Id = 1, Name = "P1", Category = "Apples"},
                new Product {Id = 2, Name = "P2", Category = "Apples"},
                new Product {Id = 3, Name = "P3", Category = "Plums"},
                new Product {Id = 4, Name = "P4", Category = "Oranges"},
            });

            var target = new NavigationViewComponent(mock.Object);

            // Act
            var result = ((IEnumerable<string>)target.Invoke()).ToArray();

            //Assert
            Assert.Equal(result.Length, 3);
            Assert.Equal(result[0], "Apples");
            Assert.Equal(result[1], "Oranges");
            Assert.Equal(result[2], "Plums");
        }

        [Fact]
        public void IndicatesSelectedCategory()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {Id = 1, Name = "P1", Category = "Apples"},
                new Product {Id = 4, Name = "P2", Category = "Oranges"},
            });

            var target = new NavigationViewComponent(mock.Object);

            var categoryToSelect = "Apples";

            // Act
            var result = ((ViewComponent)target.Invoke()).ViewBag.SelectedCategory;

            // Assert
            Assert.Equal(categoryToSelect, result);
        }

        [Fact]
        public void GenerateCategorySpecificProductCount()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {Id = 1, Name = "P1", Category = "Cat1"},
                new Product {Id = 2, Name = "P2", Category = "Cat2"},
                new Product {Id = 3, Name = "P3", Category = "Cat1"},
                new Product {Id = 4, Name = "P4", Category = "Cat2"},
                new Product {Id = 5, Name = "P5", Category = "Cat3"},
            });

            var target = new ProductController(mock.Object);
            target.PageSize = 3;

            // Act
            var res1 = ((ProductsListViewModel)target.List("Cat1").Model).PagingInfo.TotalItems;
            var res2 = ((ProductsListViewModel)target.List("Cat2").Model).PagingInfo.TotalItems;
            var res3 = ((ProductsListViewModel)target.List("Cat3").Model).PagingInfo.TotalItems;
            var resAll = ((ProductsListViewModel)target.List(null).Model).PagingInfo.TotalItems;

            // Assert
            Assert.Equal(res1, 2);
            Assert.Equal(res2, 2);
            Assert.Equal(res3, 1);
            Assert.Equal(resAll, 5);
        }
    }
}
