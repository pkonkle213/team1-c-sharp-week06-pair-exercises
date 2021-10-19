using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectOrganizer.DAL;
using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTests
{
    [TestClass]
    public class EmployeeSqlDAOTests : ProjectTestsBase
    {
        [TestMethod]
        public void GetAllEmployeesGetsAllEmployees()
        {
            //Arrange
            EmployeeSqlDAO dao = new EmployeeSqlDAO(this.ConnectionString);

            //Act
            ICollection<Employee> emp = dao.GetAllEmployees();

            //Assert
            Assert.IsNotNull(emp);
            Assert.AreEqual(2, GetRowCount("employee"));
        }

        [TestMethod]
        public void GetAllEmployeesWithANameReturnsAllEmployeesWithTheName()
        {
            //Arrange
            EmployeeSqlDAO dao = new EmployeeSqlDAO(this.ConnectionString);
            string first = "Phillip";
            string last = "Konkle";

            //Act
            ICollection<Employee> results = dao.Search(first, last);

            //Assert
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void GetEmployeesWithoutProjectsShouldReturnUnassignedPeople()
        {
            //Arrange
            EmployeeSqlDAO dao = new EmployeeSqlDAO(this.ConnectionString);

            //Act
            ICollection<Employee> emp = dao.GetEmployeesWithoutProjects();

            //Assert
            Assert.IsNotNull(emp);
            Assert.IsTrue(emp.Count > 0);
        }
    }
}
