using KitaraKauppa.Core.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Tests.KitaraKauppa.Core.Products
{
    public class ColorTests
    {
        // Check properties of Color class
        [Fact]
        public void Color_ShouldHaveValidProperties()
        {
            // Arrange
            var type = typeof(Color);

            // Act
            var name = type.GetProperty("Name");
            var products = type.GetProperty("Products");

            // Assert
            Assert.NotNull(name);
            Assert.Equal(typeof(string), name.PropertyType);

            Assert.NotNull(products);
            Assert.Equal(typeof(ICollection<Product>), products.PropertyType);
        }

        [Fact]
        public void Color_Name_SetAndGet_Success()
        {
            // Arrange
            var color = new Color();
            var expectedName = "Red";

            // Act
            color.Name = expectedName;
            var actualName = color.Name;

            // Assert
            Assert.Equal(expectedName, actualName);
        }

        [Fact]
        public void Color_Products_SetAndGet_Success()
        {
            // Arrange
            var color = new Color();
            var expectedProducts = new List<Product>();

            // Act
            color.Products = expectedProducts;
            var actualProducts = color.Products;

            // Assert
            Assert.Equal(expectedProducts, actualProducts);
        }
    }
}
