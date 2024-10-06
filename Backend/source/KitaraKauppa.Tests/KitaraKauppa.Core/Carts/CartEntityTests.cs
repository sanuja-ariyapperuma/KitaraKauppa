using KitaraKauppa.Core.Orders;
using KitaraKauppa.Core.Users;
using KitaraKauppa.Core.ProductReviews;
using KitaraKauppa.Core.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Tests.KitaraKauppa.Core.Carts
{
    public class CartEntityTests
    {
        [Fact]
        public void CartEntity_ShouldExists()
        {
            //Act 
            var classType = Type.GetType("KitaraKauppa.Core.Carts.Cart, KitaraKauppa.Core");

            //Assert
            Assert.NotNull(classType);
        }

        [Fact]
        public void Carts_ShouldHaveValidProperties() 
        {

            //Arrange
            var type = typeof(Cart);

            // Act
            var cartId = type.GetProperty("CartId");
            var userId = type.GetProperty("UserId");
            var createdAt = type.GetProperty("CreatedAt");
            var cartItems = type.GetProperty("CartItems");

            // Assert
            Assert.NotNull(cartId);
            Assert.Equal(typeof(Guid),cartId.PropertyType);


            Assert.NotNull(userId);
            Assert.Equal(typeof(Guid),userId.PropertyType);

            Assert.NotNull(createdAt);
            Assert.Equal(typeof(DateTime?),createdAt.PropertyType);

            Assert.NotNull(cartItems);
            Assert.Equal(typeof(ICollection<CartItem>),cartItems.PropertyType);
            
        }
    }
}