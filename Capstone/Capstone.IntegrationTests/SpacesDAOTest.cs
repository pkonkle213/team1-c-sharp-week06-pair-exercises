using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Capstone.DAL;
using System.Collections.Generic;
using Capstone.Models;
using System;

namespace Capstone.IntegrationTests
{
    [TestClass]
    public class SpacesDAOTest : IntegrationTestBase
    {
        [TestMethod]
        public void GetSpacesTest()
        {
            // Arrange
            SpacesDAO dao = new SpacesDAO(this.ConnectionString);
            Venue venue = new Venue
            {
                ID = 1
            };

            // Act
            List<Spaces> results = dao.GetSpaces(venue);
            
            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count());

        }

        [TestMethod]
        public void GetAvailableSpacesTest()
        {
            //Arrange
            SpacesDAO dao = new SpacesDAO(this.ConnectionString);
            Venue venue = new Venue
            {
                ID = 1
                
            };

            DateTime fromdate = new DateTime (2021, 10, 10);
            DateTime enddate = new DateTime(2021, 10, 17);
            int occupancy = 200;

            //Act
            List<Spaces> results = dao.GetAvailableSpaces(venue, fromdate, enddate, occupancy);

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count());
        }
    }
}
