using System;
using KitaraKauppa.Core.Products;
using Xunit;

namespace KitaraKauppa.Tests.KitaraKauppa.Core.Products
{
    public class ProductCategoryTests
    {
        [Fact]
        public void ProductCategoryClass_ShouldExists()
        {
            // Act
            var categoryClassType = Type.GetType("KitaraKauppa.Core.Products.ProductCategory, KitaraKauppa.Core");

            // Assert
            Assert.NotNull(categoryClassType);
        }

        [Fact]
        public void Category_ShouldHaveValidProductCategoryId()
        {
            //Arrange
            var productCategory = typeof(ProductCategory);

            // Act
            var productCategoryId = productCategory.GetProperty("ProductCategoryId");

            // Assert
            Assert.NotNull(productCategoryId);
            Assert.Equal(typeof(Guid), productCategoryId.PropertyType);
        }

        [Fact]
        public void Category_ShouldHaveValidCategoryId()
        {
            //Arrange
            var productCategory = typeof(ProductCategory);

            // Act
            var categoryId = productCategory.GetProperty("CategoryId");

            // Assert
            Assert.NotNull(categoryId);
            Assert.Equal(typeof(Guid), categoryId.PropertyType);
        }


        [Fact]
        public void Product_ShouldHaveValidProductId()
        {
            //Arrange
            var productCategory = typeof(ProductCategory);

            // Act
            var productId = productCategory.GetProperty("ProductId");

            // Assert
            Assert.NotNull(productId);
            Assert.Equal(typeof(Guid), productId.PropertyType);
        }
    }
}