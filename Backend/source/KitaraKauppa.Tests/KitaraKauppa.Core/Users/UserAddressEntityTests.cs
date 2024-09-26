using KitaraKauppa.Core.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Tests.KitaraKauppa.Core.Users
{
    public class UserAddressEntityTests
    {
        [Fact]
        public void UserAddressEntity_ShouldExists() 
        {
            //Act 
            var classType = Type.GetType("KitaraKauppa.Core.Users.UserAddress, KitaraKauppa.Core");

            //Assert
            Assert.NotNull(classType);
        }
        [Fact]
        public void UserAddress_ShouldHaveValidProperties()
        {

            //Arrange
            var type = typeof(UserAddress);

            // Act
            var addressLine1 = type.GetProperty("AddressLine1");
            var addressLine2 = type.GetProperty("AddressLine2");
            var cityId = type.GetProperty("CityId");
            var city = type.GetProperty("City");
            var userId = type.GetProperty("UserId");
            var user = type.GetProperty("User");

            Assert.NotNull(userId);
            Assert.Equal(typeof(Guid), userId.PropertyType);

            Assert.NotNull(addressLine1);
            Assert.Equal(typeof(string), addressLine1.PropertyType);

            Assert.NotNull(addressLine2);
            Assert.Equal(typeof(string), addressLine2.PropertyType);

            Assert.NotNull(cityId);
            Assert.Equal(typeof(Guid), cityId.PropertyType);

            Assert.NotNull(city);
            Assert.Equal(typeof(City), city.PropertyType);

            Assert.NotNull(userId);
            Assert.Equal(typeof(Guid), userId.PropertyType);

            Assert.NotNull(user);
            Assert.Equal(typeof(User), user.PropertyType);
        }

        [Fact]
        public void AddressLine1_Should_Throw_ValidationError_IfEmpty()
        {
            // Arrange
            var userAddress = new UserAddress { AddressLine1 = string.Empty };
            var validationContext = new ValidationContext(userAddress);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(userAddress, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, r => r.MemberNames.Contains(nameof(UserAddress.AddressLine1)));
        }

        [Fact]
        public void AddressLine1_Should_Throw_ValidationError_IfTooLong()
        {
            // Arrange
            var userAddress = new UserAddress { AddressLine1 = new string('A', 51) }; // 51 characters, exceeding max length
            var validationContext = new ValidationContext(userAddress);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(userAddress, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, r => r.MemberNames.Contains(nameof(UserAddress.AddressLine1)));
        }

        [Fact]
        public void AddressLine1_Should_Be_Valid_IfWithinMaxLength()
        {
            // Arrange
            var userAddress = new UserAddress { AddressLine1 = "Valid Address Line" };
            var validationContext = new ValidationContext(userAddress);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(userAddress, validationContext, validationResults, true);

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void AddressLine2_Should_Throw_ValidationError_IfTooLong()
        {
            // Arrange
            var userAddress = new UserAddress { AddressLine2 = new string('B', 51) }; // Exceeding max length
            var validationContext = new ValidationContext(userAddress);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(userAddress, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, r => r.MemberNames.Contains(nameof(UserAddress.AddressLine2)));
        }

        [Fact]
        public void AddressLine2_Should_Be_Valid_IfWithinMaxLength()
        {
            // Arrange
            var userAddress = new UserAddress
            {
                AddressLine2 = "Valid Address Line 2",
                AddressLine1 = "Address Line 1",   // Required
                CityId = Guid.NewGuid(),           // Required
                UserId = Guid.NewGuid(),           // Required
                City = new City(),                 // Required virtual navigation property
                User = new User()                  // Required virtual navigation property
            };
            var validationContext = new ValidationContext(userAddress);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(userAddress, validationContext, validationResults, true);

            // Assert
            Assert.True(isValid);
        }

    }
}
