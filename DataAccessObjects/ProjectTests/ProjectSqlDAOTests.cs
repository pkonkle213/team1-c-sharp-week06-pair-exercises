using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectOrganizer.DAL;
using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTests
{
    [TestClass]
    public class ProjectSqlDAOTests : ProjectTestsBase
    {
        [TestMethod]
        public void GetAllProjects_Should_GetAllProjects()
        {
            //Arrange
            ProjectSqlDAO dao = new ProjectSqlDAO(this.ConnectionString);

            //Act
            ICollection<Project> proj = dao.GetAllProjects();

            //Assert
            Assert.IsNotNull(proj);
            Assert.AreEqual(1, GetRowCount("project"));
        }

        [TestMethod]
        public void AssignEmployeeShouldAssignAnEmployee()
        {
            //Arrange
            ProjectSqlDAO dao = new ProjectSqlDAO(this.ConnectionString);

            //Act
            bool result = dao.AssignEmployeeToProject(1, 2);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RemoveEmployee_SHOULD_RemoveTheEmployee()
        {
            //Arrange
            ProjectSqlDAO dao = new ProjectSqlDAO(this.ConnectionString);

            //Act
            bool result = dao.RemoveEmployeeFromProject(1, 1);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateProjectShouldAddANewProject()
        {
            //Arrange
            ProjectSqlDAO dao = new ProjectSqlDAO(this.ConnectionString);
            Project p = new Project
            {
                Name = "Superfriends",
                StartDate = new DateTime(2021,7,4),
                EndDate = new DateTime(2021,10,31)
            };

            //Act
            int result = dao.CreateProject(p);

            //Assert
            Assert.IsTrue(result > 0);
        }

    }
}
