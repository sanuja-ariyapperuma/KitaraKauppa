using KitaraKauppa.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Tests.KitaraKauppa.Core.Base
{
    
    public class BaseEntityTests
    {
        [Fact]
        public void BaseEntity_ShouldHaveValidProperties()
        {

            //Arrange
            var type = typeof(UserRole);

            // Act
            var id = type.GetProperty("Id");
            var createdAt = type.GetProperty("CreatedAt");
            var updatedAt = type.GetProperty("UpdatedAt");
            var isDeleted = type.GetProperty("IsDeleted");

            Assert.NotNull(id);
            Assert.Equal(typeof(Guid), id.PropertyType);

            Assert.NotNull(createdAt);
            Assert.Equal(typeof(DateTime), createdAt.PropertyType);

            Assert.NotNull(updatedAt);
            Assert.Equal(typeof(DateTime), updatedAt.PropertyType);

            Assert.NotNull(isDeleted);
            Assert.Equal(typeof(bool), isDeleted.PropertyType);
        }
        [Fact]
        public void Id_Should_Be_Assignable()
        {
            // Arrange
            var testEntity = new BaseTestEntity();
            var newGuid = Guid.NewGuid();

            // Act
            testEntity.Id = newGuid;

            // Assert
            Assert.Equal(newGuid, testEntity.Id);
        }

        [Fact]
        public void CreatedAt_Should_Be_Assignable()
        {
            // Arrange
            var testEntity = new BaseTestEntity();
            var createdAt = DateTime.UtcNow;

            // Act
            testEntity.CreatedAt = createdAt;

            // Assert
            Assert.Equal(createdAt, testEntity.CreatedAt);
        }

        [Fact]
        public void UpdatedAt_Should_Be_Assignable()
        {
            // Arrange
            var testEntity = new BaseTestEntity();
            var updatedAt = DateTime.UtcNow.AddHours(1);

            // Act
            testEntity.UpdatedAt = updatedAt;

            // Assert
            Assert.Equal(updatedAt, testEntity.UpdatedAt);
        }

        [Fact]
        public void IsDeleted_Should_Be_True_When_Set()
        {
            // Arrange
            var testEntity = new BaseTestEntity();

            // Act
            testEntity.IsDeleted = true;

            // Assert
            Assert.True(testEntity.IsDeleted);
        }

        [Fact]
        public void IsDeleted_Should_Be_False_ByDefault()
        {
            // Arrange
            var testEntity = new BaseTestEntity();

            // Assert
            Assert.False(testEntity.IsDeleted); // By default, IsDeleted should be false
        }
    }
}
