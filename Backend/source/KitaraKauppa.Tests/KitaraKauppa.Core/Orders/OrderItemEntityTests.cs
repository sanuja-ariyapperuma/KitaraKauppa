using KitaraKauppa.Core.Orders;
using KitaraKauppa.Core.Products;
using KitaraKauppa.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace KitaraKauppa.Tests.KitaraKauppa.Core.Orders
{
    public class OrderItemEntityTests
    {
        [Fact]
        public void OrderItemsEntity_ShouldExists()
        {
            //Act 
            var classType = Type.GetType("KitaraKauppa.Core.Orders.OrderItem, KitaraKauppa.Core");

            //Assert
            Assert.NotNull(classType);
        }

        [Fact]
        public void OrderItems_ShouldHaveValidproperties()
        {
            //Arrange
            var type = typeof(OrderItem);

            // Act
            var orderId = type.GetProperty("OrderId");
            var productId = type.GetProperty("ProductId");
            var colorId = type.GetProperty("ColorId");
            var oriantation = type.GetProperty("Orientation");
            var units = type.GetProperty("Units");
            var price = type.GetProperty("Price");
            var color = type.GetProperty("Color");
            var order = type.GetProperty("Order");
            var product = type.GetProperty("Product");

            // Assert
            Assert.NotNull(colorId);
            Assert.Equal(typeof(Guid), colorId.PropertyType);

            Assert.NotNull(orderId);
            Assert.Equal(typeof(Guid),orderId.PropertyType);

            Assert.NotNull(productId);
            Assert.Equal(typeof(Guid),productId.PropertyType);

            Assert.NotNull(oriantation);
            Assert.Equal(typeof(Orientation), oriantation.PropertyType);

            Assert.NotNull(price);
            Assert.Equal(typeof(decimal),price.PropertyType);

            Assert.NotNull(units);
            Assert.Equal(typeof(int), units.PropertyType);

            Assert.NotNull(order);
            Assert.Equal(typeof(Order),order.PropertyType);

            Assert.NotNull(product);
            Assert.Equal(typeof(Product), product.PropertyType);

            Assert.NotNull(color);
            Assert.Equal(typeof(Color), color.PropertyType);

        }
    }
}