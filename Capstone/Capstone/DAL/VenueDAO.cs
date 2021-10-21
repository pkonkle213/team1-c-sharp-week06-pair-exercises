using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    /// <summary>
    /// This class handles working with Venues in the database.
    /// </summary>
    public class VenueDAO 
    {
        private readonly string connectionString;

        private const string SqlSelectByVenue =
            "SELECT id, name, city_id, description " +
            "FROM venue";

        public VenueDAO(string databaseConnectionString)
        {
            this.connectionString = databaseConnectionString;
        }

        public IEnumerable<Venue> GetVenues()
        {
            List<Venue> results = new List<Venue>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(SqlSelectByVenue, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Venue venue = BuildVenueFromReader(reader);
                        results.Add(venue);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Could not query the database: " + ex.Message);
            }

            return results;
        }

        private Venue BuildVenueFromReader(SqlDataReader reader)
        {
            return new Venue
            {
                ID = Convert.ToInt32(reader["id"]),
                Name = Convert.ToString(reader["name"]),
                City_ID = Convert.ToInt32(reader["city_id"]),
                Description = Convert.ToString(reader["description"])
            };
        }
    }
}
