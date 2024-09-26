using System;
using KitaraKauppa.Core.Products;
using Xunit;

namespace KitaraKauppa.Tests.KitaraKauppa.Core.Products
{
    public class ImageTests
    {
        [Fact]
        public void ImageClass_ShouldExists()
        {
            //Act
            var imageClassType = Type.GetType("KitaraKauppa.Core.Products.Image, KitaraKauppa.Core");

            //
            Assert.NotNull(imageClassType);
        }

        [Fact]
        public void Image_ShouldHaveValidProperties()
        {
            //Arrange
            var type = typeof(Image);

            //Act
            var imageId = type.GetProperty("ImageAlt");
            var extention = type.GetProperty("Extention");
            var product = type.GetProperty("Product");

            //Assert
            Assert.NotNull(imageId);
            Assert.Equal(typeof(string), imageId.PropertyType);

            Assert.NotNull(extention);
            Assert.Equal(typeof(string), extention.PropertyType);

            Assert.NotNull(product);
            Assert.Equal(typeof(ICollection<Product>), product.PropertyType);
        }
    }
}