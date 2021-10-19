using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProjectOrganizer.DAL
{
    public class DepartmentSqlDAO : IDepartmentDAO
    {
        private readonly string connectionString;

        private const string SqlInsert =
            "INSERT INTO department (name) " +
            "VALUES (@name); SELECT @@IDENTITY;";

        private const string SqlUpdate =
            "UPDATE department SET name = @name WHERE departnment_id = @departnment_id;";

        // Single Parameter Constructor
        public DepartmentSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns a list of all of the departments.
        /// </summary>
        /// <returns></returns>
        public ICollection<Department> GetDepartments()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new department.
        /// </summary>
        /// <param name="newDepartment">The department object.</param>
        /// <returns>The id of the new department (if successful).</returns>
        public int CreateDepartment(Department newDepartment)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(SqlInsert, conn);
                    command.Parameters.AddWithValue("@name", newDepartment.Name);

                    int id = Convert.ToInt32(command.ExecuteScalar());

                    return id;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Could not create department: " + ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// Updates an existing department.
        /// </summary>
        /// <param name="updatedDepartment">The department object.</param>
        /// <returns>True, if successful.</returns>
        public bool UpdateDepartment(Department updatedDepartment)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(SqlUpdate, conn);
                    command.Parameters.AddWithValue("@name", updatedDepartment.Name);
                    command.Parameters.AddWithValue("@department_id", updatedDepartment.Id);

                    command.ExecuteNonQuery();

                    return true;

                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Could not update departnment " + ex.Message);
                return false;
            }
        }

    }
}
