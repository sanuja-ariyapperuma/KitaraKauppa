using NUnit.Framework;
using KitaraKauppa.Core.Products;

namespace KitaraKauppa.Tests.KitaraKauppa.Core.Products
{
    public class BrandTests
    {

        //Validate property
        [Fact]
        public void Brand_ShouldHaveValidProperties()
        {
            //Arrange
            var type = typeof(Brand);

            // Act
            var name = type.GetProperty("Name");

            Assert.NotNull(name);
            Assert.AreEqual(typeof(string), name.PropertyType);
        }
    }
}
