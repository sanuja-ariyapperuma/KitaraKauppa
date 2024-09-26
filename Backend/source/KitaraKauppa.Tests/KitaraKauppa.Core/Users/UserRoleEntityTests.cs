using KitaraKauppa.Core.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Tests.KitaraKauppa.Core.Users
{
    public  class UserRoleEntityTests
    {
        [Fact]
        public void UserRoleEntity_ShouldExists() 
        {
            //Act 
            var classType = Type.GetType("KitaraKauppa.Core.Users.UserRole, KitaraKauppa.Core");

            //Assert
            Assert.NotNull(classType);
        }
        [Fact]
        public void UserRole_ShouldHaveValidProperties()
        {

            //Arrange
            var type = typeof(UserRole);

            // Act
            var userRoleId = type.GetProperty("Id");
            var userRoleName = type.GetProperty("UserRoleName");
            var users = type.GetProperty("Users");

            Assert.NotNull(userRoleId);
            Assert.Equal(typeof(Guid), userRoleId.PropertyType);

            Assert.NotNull(userRoleName);
            Assert.Equal(typeof(string), userRoleName.PropertyType);

            Assert.NotNull(users);
            Assert.Equal(typeof(ICollection<User>), users.PropertyType);
        }

        [Fact]
        public void UserRoleName_Should_ThrowException_IfEmpty()
        {
            var userRole = new UserRole();
            var context = new ValidationContext(userRole);
            var result = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(userRole, context, result, true);

            Assert.False(isValid);
            Assert.Contains(result, r => r.MemberNames.Contains("UserRoleName"));
        }

        [Fact]
        public void UserRoleName_Should_Be_Valid_IfLengthWithinLimits()
        {
            var userRole = new UserRole { UserRoleName = "Admin" };
            var context = new ValidationContext(userRole);
            var result = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(userRole, context, result, true);

            Assert.True(isValid);
            Assert.DoesNotContain(result, r => r.MemberNames.Contains("UserRoleName"));
        }

        [Fact]
        public void Users_Collection_Should_NotBeNull_WhenInitialized()
        {
            var userRole = new UserRole();
            Assert.NotNull(userRole.Users);
        }
    }
}
