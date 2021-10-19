using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectOrganizer.DAL;
using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTests
{
    [TestClass]
    public class DepartmentSqlDAOTests : ProjectTestsBase
    {
        [TestMethod]
        public void CreateDepartmentTest()
        {
            //Arrange
            DepartmentSqlDAO dao = new DepartmentSqlDAO(ConnectionString);
            Department D = new Department
            {
                Name = "Team Bond",
                
            };

            //Act
            int result = dao.CreateDepartment(D);

            //Assert
            Assert.IsTrue(result > 1);
        }

        [TestMethod]
        public void UpdateDepartmentTest()
        {
            //Arrange
            DepartmentSqlDAO dao = new DepartmentSqlDAO(ConnectionString);
            Department updateDepartment = new Department
            {
                Name = "Outkast",
                Id = 1

            };


            //Act
            bool result = dao.UpdateDepartment(updateDepartment);

            //Assert
            Assert.IsTrue(result);

        }

    }
    
}
