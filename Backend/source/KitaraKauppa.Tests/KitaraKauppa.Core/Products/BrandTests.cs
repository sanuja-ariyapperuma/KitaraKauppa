using KitaraKauppa.Core.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Tests.KitaraKauppa.Core.Products
{
    public class BrandTests
    {
        [Fact]
        public void Brand_ShouldExists()
        {
            //Act 
            var classType = Type.GetType("KitaraKauppa.Core.Products.Brand, KitaraKauppa.Core");

            //Assert
            Assert.NotNull(classType);
        }
        [Fact]
        public void Brand_ShouldHaveValidProperties()
        {

            //Arrange
            var type = typeof(Brand);

            // Act
            var name = type.GetProperty("Name");

            Assert.NotNull(name);
            Assert.Equal(typeof(string), name.PropertyType);
        }
    }
}
