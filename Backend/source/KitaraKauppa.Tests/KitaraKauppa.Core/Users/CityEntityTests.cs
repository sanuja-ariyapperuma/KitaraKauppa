using KitaraKauppa.Core.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Tests.KitaraKauppa.Core.Users
{
    public class CityEntityTests
    {
        [Fact]
        public void CityEntity_ShouldExists() 
        {
            //Act 
            var classType = Type.GetType("KitaraKauppa.Core.Users.City, KitaraKauppa.Core");

            //Assert
            Assert.NotNull(classType);
        }
        [Fact]
        public void City_ShouldHaveValidProperties()
        {

            //Arrange
            var type = typeof(City);

            var cityName = type.GetProperty("CityName");


            Assert.NotNull(cityName);
            Assert.Equal(typeof(string), cityName.PropertyType);
        }

        [Fact]
        public void CityName_Should_Throw_ValidationError_IfEmpty()
        {
            // Arrange
            var city = new City { CityName = string.Empty };
            var validationContext = new ValidationContext(city);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(city, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, r => r.MemberNames.Contains(nameof(City.CityName)));
        }

        [Fact]
        public void CityName_Should_Throw_ValidationError_IfTooLong()
        {
            // Arrange
            var city = new City { CityName = new string('A', 51) }; // 51 characters, exceeding max length
            var validationContext = new ValidationContext(city);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(city, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, r => r.MemberNames.Contains(nameof(City.CityName)));
        }

        [Fact]
        public void CityName_Should_Be_Valid_IfWithinMaxLength()
        {
            // Arrange
            var city = new City { CityName = "ValidCityName" };
            var validationContext = new ValidationContext(city);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(city, validationContext, validationResults, true);

            // Assert
            Assert.True(isValid);
        }
    }
}
