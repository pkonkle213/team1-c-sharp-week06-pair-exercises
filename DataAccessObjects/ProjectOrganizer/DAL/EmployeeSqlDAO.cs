using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProjectOrganizer.DAL
{
    public class EmployeeSqlDAO : IEmployeeDAO
    {
        private readonly string connectionString;

        private const string SqlSelectAll =
            "SELECT * FROM employee;";

        private const string GetEmployees =
            "SELECT * " +
            "FROM employee e " +
            "LEFT OUTER JOIN project_employee pe ON pe.employee_id = e.employee_id " +
            "WHERE project_id IS NULL;";

        private const string SqlSearch =
        "SELECT * FROM employee " +
        "WHERE first_name = @first_name AND last_name = @last_name;";

        // Single Parameter Constructor
        public EmployeeSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns a list of all of the employees.
        /// </summary>
        /// <returns>A list of all employees.</returns>
        public ICollection<Employee> GetAllEmployees()
        {
            List<Employee> results = new List<Employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(SqlSelectAll, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Employee emp = new Employee();
                        emp.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                        emp.DepartmentId = Convert.ToInt32(reader["department_id"]);
                        emp.FirstName = Convert.ToString(reader["first_name"]);
                        emp.LastName = Convert.ToString(reader["last_name"]);
                        emp.JobTitle = Convert.ToString(reader["job_title"]);
                        emp.BirthDate = Convert.ToDateTime(reader["Birth_Date"]);
                        emp.HireDate = Convert.ToDateTime(reader["hire_date"]);
                        results.Add(emp);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Could not get all employees " + ex.Message);
            }
            return results;
        }

        /// <summary>
        /// Find all employees whose names contain the search strings.
        /// Returned employees names must contain *both* first and last names.
        /// </summary>
        /// <remarks>Be sure to use LIKE for proper search matching.</remarks>
        /// <param name="firstname">The string to search for in the first_name field</param>
        /// <param name="lastname">The string to search for in the last_name field</param>
        /// <returns>A list of employees that matches the search.</returns>
        public ICollection<Employee> Search(string firstname, string lastname)
        {
            List<Employee> results = new List<Employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(SqlSearch, conn);
                    command.Parameters.AddWithValue("@first_name", firstname);
                    command.Parameters.AddWithValue("@last_name", lastname);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Employee emp = new Employee();
                        emp.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                        emp.DepartmentId = Convert.ToInt32(reader["department_id"]);
                        emp.FirstName = Convert.ToString(reader["first_name"]);
                        emp.LastName = Convert.ToString(reader["last_name"]);
                        emp.JobTitle = Convert.ToString(reader["job_title"]);
                        emp.BirthDate = Convert.ToDateTime(reader["Birth_Date"]);
                        emp.HireDate = Convert.ToDateTime(reader["hire_date"]);
                        results.Add(emp);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Couldn't find first and last name " + ex.Message);
            }
            return results;
        }



        /// <summary>
        /// Gets a list of employees who are not assigned to any active projects.
        /// </summary>
        /// <returns></returns>
        public ICollection<Employee> GetEmployeesWithoutProjects()
        {
            List<Employee> result = new List<Employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(GetEmployees, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Employee emp = new Employee();
                        emp.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                        emp.DepartmentId = Convert.ToInt32(reader["department_id"]);
                        emp.FirstName = Convert.ToString(reader["first_name"]);
                        emp.LastName = Convert.ToString(reader["last_name"]);
                        emp.JobTitle = Convert.ToString(reader["job_title"]);
                        emp.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                        emp.HireDate = Convert.ToDateTime(reader["hire_date"]);
                        result.Add(emp);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Could not retrieve any employees without projects "+ex.Message);
            }
            return result;
        }
    }
}