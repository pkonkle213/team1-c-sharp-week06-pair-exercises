using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Capstone.DAL;
using System.Collections.Generic;
using Capstone.Models;
using System;
using System.Data.SqlClient;
using System.Transactions;


namespace Capstone.IntegrationTests
{
    [TestClass]
    public class ReservationDAOTest : IntegrationTestBase
    {
        [TestMethod]
        public void GetReservationTest()
        {
            //Arrange
            ReservationDAO dao = new ReservationDAO(ConnectionString);
            int spaceId = 1;
            DateTime startDate = new DateTime(2021, 10, 10);
            DateTime endDate = new DateTime(2021, 10, 17);
            string reservedFor = "Matt Eland";
            int attendees = 33;

            //Act
            int results = dao.SubmitReservation(spaceId, startDate, endDate, reservedFor, attendees);


            //Assert
            Assert.IsTrue(results > 0);
            //Assert.AreEqual(results, GetRowCount("Reservation"));

            
        }
    }
}


