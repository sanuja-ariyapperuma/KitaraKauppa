using KitaraKauppa.Core.Orders;
using KitaraKauppa.Core.Users;
using KitaraKauppa.Core.ProductReviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KitaraKauppa.Core.Shared;

namespace KitaraKauppa.Tests.KitaraKauppa.Core.Orders
{
    public class OrderEntityTests
    {
        [Fact]
        public void OrdersEntity_ShouldExists()
        {
            //Act 
            var classType = Type.GetType("KitaraKauppa.Core.Orders.Order, KitaraKauppa.Core");

            //Assert
            Assert.NotNull(classType);
        }

        [Fact]
        public void Orders_ShouldHaveValidProperties() 
        {

            //Arrange
            var type = typeof(Order);

            // Act
            var userId = type.GetProperty("UserId");
            var addressId = type.GetProperty("AddressId");
            var totalAmount = type.GetProperty("TotalAmount");
            var orderStatus = type.GetProperty("OrderStatus");
            var address = type.GetProperty("Address");
            var orderItems = type.GetProperty("OrderItems");
            var users = type.GetProperty("User");


            // Assert
            Assert.NotNull(userId);
            Assert.Equal(typeof(Guid),userId.PropertyType);

            Assert.NotNull(orderStatus);
            Assert.Equal(typeof(OrderStatus), orderStatus.PropertyType);

            Assert.NotNull(addressId);
            Assert.Equal(typeof(Guid?),addressId.PropertyType);

            Assert.NotNull(totalAmount);
            Assert.Equal(typeof(decimal?),totalAmount.PropertyType);

            Assert.NotNull(address);
            Assert.Equal(typeof(UserAddress), address.PropertyType);
            
            Assert.NotNull(orderItems);
            Assert.Equal(typeof(ICollection<OrderItem>),orderItems.PropertyType);
            
            Assert.NotNull(users);
            Assert.Equal(typeof(User), users.PropertyType);
        }
    }
}