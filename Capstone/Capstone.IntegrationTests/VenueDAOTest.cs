using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Capstone.DAL;
using Capstone.Models;




namespace Capstone.IntegrationTests
{
    [TestClass]
    public class VenueTest : IntegrationTestBase
    {
        [TestMethod]
        public void GetVenueTest()
        {
            // Arrange
            VenueDAO dao = new VenueDAO(this.ConnectionString);

            // Act
            IEnumerable<Venue> results = dao.GetVenues();

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count());

        }

        [TestMethod]
        public void GetCategoiresTest()
        {
            //Arrange
            VenueDAO dao = new VenueDAO(this.ConnectionString);
            Venue venue = new Venue
            {
                ID = 1
            };

            //Act
            List<string> results = dao.GetCategories(venue);

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count());

        }
    }

      

   
}
