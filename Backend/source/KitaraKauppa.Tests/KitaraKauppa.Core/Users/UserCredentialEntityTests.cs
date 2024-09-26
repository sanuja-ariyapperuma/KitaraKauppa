using KitaraKauppa.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Tests.KitaraKauppa.Core.Users
{
    public class UserCredentialEntityTests
    {
        [Fact]
        public void UserCredentialEntity_ShouldExists() 
        {
            //Act 
            var classType = Type.GetType("KitaraKauppa.Core.Users.UserCredential, KitaraKauppa.Core");

            //Assert
            Assert.NotNull(classType);
        }
        [Fact]
        public void UserCredential_ShouldHaveValidProperties()
        {

            //Arrange
            var type = typeof(UserCredential);

            // Act
            var userName = type.GetProperty("UserName");
            var password = type.GetProperty("Password");
            var user = type.GetProperty("User");

            Assert.NotNull(userName);
            Assert.Equal(typeof(string), userName.PropertyType);

            Assert.NotNull(password);
            Assert.Equal(typeof(string), password.PropertyType);

            Assert.NotNull(user);
            Assert.Equal(typeof(User), user.PropertyType);
        }
    }
}
