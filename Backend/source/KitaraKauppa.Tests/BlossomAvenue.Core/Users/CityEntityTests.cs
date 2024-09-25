﻿using KitaraKauppa.Core.Users;
using System;
using System.Collections.Generic;
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

            // Act
            var cityId = type.GetProperty("CityId");
            var cityName = type.GetProperty("CityName");
            var addressDetails = type.GetProperty("AddressDetails");

            Assert.NotNull(cityId);
            Assert.Equal(typeof(Guid), cityId.PropertyType);

            Assert.NotNull(cityName);
            Assert.Equal(typeof(string), cityName.PropertyType);

            Assert.NotNull(addressDetails);
            Assert.Equal(typeof(ICollection<AddressDetail>), addressDetails.PropertyType);
        }
    }
}
