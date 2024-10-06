using System;
using System.Collections.Generic;
using Xunit;

using KitaraKauppa.Core.Products;
using KitaraKauppa.Core.Shared;
using System.ComponentModel.DataAnnotations;

namespace KitaraKauppa.Tests.KitaraKauppa.Core.Products
{
    public class ProductTests
    {
        [Fact]
        public void ProductEntity_ShouldExist()
        {
            // Act
            var classType = Type.GetType("KitaraKauppa.Core.Products.Product, KitaraKauppa.Core");

            // Assert
            Assert.NotNull(classType);
        }

        [Fact]
        public void Product_ShouldHaveValidProperties()
        {
            // Arrange
            var type = typeof(Product);

            // Act and Assert each property
            var title = type.GetProperty("Title");
            var description = type.GetProperty("Description");
            var unitPrice = type.GetProperty("UnitPrice");
            var varientType = type.GetProperty("VarientType");
            var orientation = type.GetProperty("Orientation");
            var brandId = type.GetProperty("BrandId");
            var brand = type.GetProperty("Brand");
            var images = type.GetProperty("Images");
            var colors = type.GetProperty("Colors");

            Assert.NotNull(title);
            Assert.Equal(typeof(string), title.PropertyType);

            Assert.NotNull(description);
            Assert.Equal(typeof(string), description.PropertyType);

            Assert.NotNull(unitPrice);
            Assert.Equal(typeof(decimal), unitPrice.PropertyType);

            Assert.NotNull(varientType);
            Assert.Equal(typeof(VarientType), varientType.PropertyType);

            Assert.NotNull(orientation);
            Assert.Equal(typeof(Orientation), orientation.PropertyType);

            Assert.NotNull(brandId);
            Assert.Equal(typeof(Guid), brandId.PropertyType);

            Assert.NotNull(brand);
            Assert.Equal(typeof(Brand), brand.PropertyType);

            Assert.NotNull(images);
            Assert.Equal(typeof(ICollection<Image>), images.PropertyType);

            Assert.NotNull(colors);
            Assert.Equal(typeof(ICollection<Color>), colors.PropertyType);
        }

        [Fact]
        public void Product_Title_ShouldHaveMinLengthValidation()
        {
            // Arrange
            var property = typeof(Product).GetProperty("Title");

            // Act
            var minLengthAttribute = property.GetCustomAttributes(typeof(MinLengthAttribute), true)[0] as MinLengthAttribute;

            // Assert
            Assert.NotNull(minLengthAttribute);
            Assert.Equal(3, minLengthAttribute.Length);
        }

        [Fact]
        public void Product_Title_ShouldHaveMaxLengthValidation()
        {
            // Arrange
            var property = typeof(Product).GetProperty("Title");

            // Act
            var maxLengthAttribute = property.GetCustomAttributes(typeof(MaxLengthAttribute), true)[0] as MaxLengthAttribute;

            // Assert
            Assert.NotNull(maxLengthAttribute);
            Assert.Equal(50, maxLengthAttribute.Length);
        }

        [Fact]
        public void Product_Description_ShouldBeRequired()
        {
            // Arrange
            var property = typeof(Product).GetProperty("Description");

            // Act
            var requiredAttribute = property.GetCustomAttributes(typeof(RequiredAttribute), true)[0] as RequiredAttribute;

            // Assert
            Assert.NotNull(requiredAttribute);
        }

        [Fact]
        public void Product_UnitPrice_ShouldBeRequired()
        {
            // Arrange
            var property = typeof(Product).GetProperty("UnitPrice");

            // Act
            var requiredAttribute = property.GetCustomAttributes(typeof(RequiredAttribute), true)[0] as RequiredAttribute;

            // Assert
            Assert.NotNull(requiredAttribute);
        }

        [Fact]
        public void Product_VarientType_ShouldBeRequired()
        {
            // Arrange
            var property = typeof(Product).GetProperty("VarientType");

            // Act
            var requiredAttribute = property.GetCustomAttributes(typeof(RequiredAttribute), true)[0] as RequiredAttribute;

            // Assert
            Assert.NotNull(requiredAttribute);
        }

        [Fact]
        public void Product_Orientation_ShouldBeRequired()
        {
            // Arrange
            var property = typeof(Product).GetProperty("Orientation");

            // Act
            var requiredAttribute = property.GetCustomAttributes(typeof(RequiredAttribute), true)[0] as RequiredAttribute;

            // Assert
            Assert.NotNull(requiredAttribute);
        }
    }
}