using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SportsStore.Tests
{
    public class SessionsTests
    {
        [Fact]
        public void TestSession()
        {
            // Arrange
            var cart = new Cart();
            cart.AddItem(new Product
            {
                Id = 1,
                Name = "P1",
                Description = "Dic 1 test",
                Category = "Cat1",
                Price = 100M
            }, 1);


            var serCart = JsonSerializer.Serialize(cart);
            var desCart = JsonSerializer.Deserialize<Cart>(serCart);

            var actual = desCart.LineCollection.ToArray();
            var expected = cart.LineCollection.ToArray();

            Assert.Equal(actual[0].Quantity, expected[0].Quantity);
            Assert.Equal(actual[0].Product.Id, expected[0].Product.Id);
            Assert.Equal(actual[0].Product.Name, expected[0].Product.Name);
            Assert.Equal(actual[0].Product.Description, expected[0].Product.Description);
            Assert.Equal(actual[0].Product.Category, expected[0].Product.Category);
            Assert.Equal(actual[0].Product.Price, expected[0].Product.Price);
        }
    }
}
