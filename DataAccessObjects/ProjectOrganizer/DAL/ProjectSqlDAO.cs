using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProjectOrganizer.DAL
{
    public class ProjectSqlDAO : IProjectDAO
    {
        private readonly string connectionString;

        private const string SqlSelectAll =
            "SELECT * " +
            "FROM project;";

        private const string SqlRemove =
            "DELETE FROM project_employee " +
            "WHERE project_id = @project_id AND employeeId = @employeeId;";

        private const string SqlAssign =
            "INSERT INTO project_employee (project_id, employee_id) " +
            "VALUES (@project_id,@employee_id);";

        private const string SqlInsertProject =
            "INSERT INTO project (name,from_date,to_date) " +
            "VALUES (@name,@from_date,@to_date);";

        // Single Parameter Constructor
        public ProjectSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns all projects.
        /// </summary>
        /// <returns></returns>
        public ICollection<Project> GetAllProjects()
        {
            List<Project> results = new List<Project>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(SqlSelectAll, conn);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Project pimp = new Project();
                        pimp.ProjectId = Convert.ToInt32(reader["project_id"]);
                        pimp.Name = Convert.ToString(reader["name"]);
                        pimp.StartDate = Convert.ToDateTime(reader["from_date"]);
                        pimp.EndDate = Convert.ToDateTime(reader["to_date"]);

                        results.Add(pimp);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Could not get all the projects: " + ex.Message);
            }
            return results;
        }

        /// <summary>
        /// Assigns an employee to a project using their IDs.
        /// </summary>
        /// <param name="projectId">The project's id.</param>
        /// <param name="employeeId">The employee's id.</param>
        /// <returns>If it was successful.</returns>
        public bool AssignEmployeeToProject(int projectId, int employeeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(SqlAssign, conn);
                    command.Parameters.AddWithValue("@project_id", projectId);
                    command.Parameters.AddWithValue("@employee_id", employeeId);

                    command.ExecuteNonQuery();
                }

                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Unable to assign the employee to the project: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Removes an employee from a project.
        /// </summary>
        /// <param name="projectId">The project's id.</param>
        /// <param name="employeeId">The employee's id.</param>
        /// <returns>If it was successful.</returns>
        public bool RemoveEmployeeFromProject(int projectId, int employeeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(SqlRemove, conn);
                    command.Parameters.AddWithValue("@project_id", projectId);
                    command.Parameters.AddWithValue("@employee_id", employeeId);

                    command.ExecuteNonQuery();
                }

                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Unable to remove employee from project " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <param name="newProject">The new project object.</param>
        /// <returns>The new id of the project.</returns>
        public int CreateProject(Project newProject)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(SqlInsertProject, conn);
                    command.Parameters.AddWithValue("@name", newProject.Name);
                    command.Parameters.AddWithValue("@from_date", newProject.StartDate);
                    command.Parameters.AddWithValue("@to_date", newProject.EndDate);

                    int id = Convert.ToInt32(command.ExecuteScalar());
                    return id;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("something here " + ex.Message);
                return -1;
            }
        }

    }
}
