using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class ReservationDAO
    {
        private readonly string connectionString;

        public const string InsertReservation =
            "INSERT INTO reservation (space_id,number_of_attendees,start_date,end_date,reserved_for) " +
            "VALUES(@space_id, @number_of_attendees, @start_date, @end_date, @reserved_for); SELECT @@IDENTITY;";

        public ReservationDAO(string databaseConnectionString)
        {
            this.connectionString = databaseConnectionString;
        }

        public int SubmitReservation(int space_Id,DateTime startDate, DateTime endDate, string reservedFor, int attendees)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(InsertReservation, conn);
                    command.Parameters.AddWithValue("@space_id", space_Id);
                    command.Parameters.AddWithValue("@number_of_attendees",attendees);
                    command.Parameters.AddWithValue("@start_date",startDate);
                    command.Parameters.AddWithValue("@end_date",endDate);
                    command.Parameters.AddWithValue("@reserved_for",reservedFor);
                    int id = Convert.ToInt32(command.ExecuteScalar());

                    return id;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Could not add the reservation to the database: " + ex.Message);
                return -1;
            }
        }
    }
}
