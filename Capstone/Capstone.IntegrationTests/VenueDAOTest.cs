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
    }
}
