using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.UnitTests
{
    public class CartTests
    {
        [Fact]
        public void CanAddNewLineCollection()
        {
            // Arrange
            var p1 = new Product { Id = 1, Name = "P1" };
            var p2 = new Product { Id = 2, Name = "P2" };

            var target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            var result = target.LineCollection.ToArray();

            // Assert
            Assert.Equal(result.Length, 2);
            Assert.Equal(result[0].Product, p1);
            Assert.Equal(result[1].Product, p2);
        }

        [Fact]
        public void CanAddQuantityForExistingLineCollection()
        {
            // Arrange
            var p1 = new Product { Id = 1, Name = "P1" };
            var p2 = new Product { Id = 2, Name = "P2" };

            var target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            var result = target.LineCollection.OrderBy(c => c.Product.Id).ToArray();

            // Assert
            Assert.Equal(result.Length, 2);
            Assert.Equal(result[0].Quantity, 11);
            Assert.Equal(result[1].Quantity, 1);
        }

        [Fact]
        public void CanRemoveLine()
        {
            // Arrange
            var p1 = new Product { Id = 1, Name = "P1" };
            var p2 = new Product { Id = 2, Name = "P2" };
            var p3 = new Product { Id = 3, Name = "P3" };

            var target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            // Act
            target.RemoveLine(p2);

            // Assert
            Assert.Equal(target.LineCollection.Where(c => c.Product == p2).Count(), 0);
            Assert.Equal(target.LineCollection.Count(), 2);
        }

        [Fact]
        public void CalculateCartTotal()
        {
            // Arrange
            var p1 = new Product { Id = 1, Name = "P1", Price = 100M };
            var p2 = new Product { Id = 2, Name = "P2", Price = 50M };

            var target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            var result = target.ComputeTotalValue();

            // Assert
            Assert.Equal(result, 450M);
        }

        [Fact]
        public void CanClearContents()
        {
            // Arrange
            var p1 = new Product { Id = 1, Name = "P1", Price = 100M };
            var p2 = new Product { Id = 2, Name = "P2", Price = 50M };

            var target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            //Act
            target.Clear();

            // Assert
            Assert.Equal(target.LineCollection.Count(), 0);
        }

        [Fact]
        public void CanAddToCart()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
               new Product {Id =1, Name = "P1", Category = "Apples"}
            }.AsQueryable());

            var cart = new Cart();

            var target = new CartController(mock.Object);

            // Act
            target.AddToCart(cart, 1, null);

            // Assert
            Assert.Equal(cart.LineCollection.Count(), 1);
            Assert.Equal(cart.LineCollection.ToArray()[0].Product.Id, 1);
        }

        [Fact]
        public void AddingProductToCartGoesToCartScreen()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
               new Product {Id =1, Name = "P1", Category = "Apples"}
            }.AsQueryable());

            var cart = new Cart();

            var target = new CartController(mock.Object);

            // Act
            var result = target.AddToCart(cart, 2, "myUrl");

            // Assert
            Assert.Equal(result.ActionName, "Index");
            Assert.Equal(result.RouteValues["returnUrl"], "myUrl");
        }

        [Fact]
        public void CanViewCartContents()
        {
            // Arrange
            var cart = new Cart();

            var target = new CartController(null);

            // Act
            var result = (CartIndexViewModel)target.Index(cart, "myUrl").ViewData.Model;

            // Assert
            Assert.Same(result.Cart, cart);
            Assert.Equal(result.ReturnUrl, "myUrl");
        }
    }
}
