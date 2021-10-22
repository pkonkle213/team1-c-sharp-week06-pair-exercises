using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public class SpacesDAO
    {

        private readonly string connectionString;

        private const string SqlSelectBySpaces =
            "SELECT s.id, s.name,open_from,open_to,daily_rate,is_accessible,max_occupancy " +
            "FROM space s " +
            "INNER JOIN venue v ON v.id=s.venue_id " +
            "WHERE v.id= @id";

        private const string SqlSelectSpaces =
            "SELECT s.id, s.name, s.daily_rate,s.max_occupancy,s.is_accessible,s.open_from,s.open_to " +
            "FROM space s " +
            "INNER JOIN venue v ON v.id = s.venue_id " +
            "WHERE v.id = @venueid " +
            "    AND @uifromdate >= s.open_from " +
            "    AND @uienddate <= s.open_to " +
            "    AND @uioccupancy <= s.max_occupancy " +
            "UNION " +
            "SELECT s.id, s.name, s.daily_rate, s.max_occupancy, s.is_accessible, s.open_from, s.open_to " +
            "FROM space s INNER JOIN venue v ON v.id = s.venue_id " +
            "WHERE v.id = @venueid" +
            "    AND s.open_from IS NULL" +
            "    AND @uioccupancy <= s.max_occupancy";

        public SpacesDAO(string databaseConnectionString)
        {
            this.connectionString = databaseConnectionString;
        }

        public List<Spaces> GetSpaces(Venue venue)
        {
            List<Spaces> results = new List<Spaces>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(SqlSelectBySpaces, conn);
                    command.Parameters.AddWithValue("@id", venue.ID);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Spaces spaces = BuildSpacesFromReader(reader);
                        results.Add(spaces);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Could not query the database: " + ex.Message);
            }

            return results;
        }

        public List<Spaces> GetAvailableSpaces(Venue venue, DateTime fromDate, DateTime endDate, int occupancy)
        {
            List<Spaces> results = new List<Spaces>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(SqlSelectSpaces, conn);
                    command.Parameters.AddWithValue("@venueid", venue.ID);
                    command.Parameters.AddWithValue("@uifromdate", fromDate);
                    command.Parameters.AddWithValue("@uienddate", endDate);
                    command.Parameters.AddWithValue("@uioccupancy", occupancy);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Spaces spaces = BuildSpacesFromReader(reader);
                        results.Add(spaces);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Could not query the database: " + ex.Message);
            }

            return results;
        }

        private Spaces BuildSpacesFromReader(SqlDataReader reader)
        {
            Spaces spaces = new Spaces();
            spaces.Id = Convert.ToInt32(reader["id"]);
            spaces.Name = Convert.ToString(reader["name"]);
            spaces.Daily_Rate = Convert.ToDecimal(reader["daily_rate"]);
            spaces.Max_Occupancy = Convert.ToInt32(reader["max_occupancy"]);
            spaces.Is_Accessible = Convert.ToBoolean(reader["is_accessible"]);

            if (reader["open_from"] != DBNull.Value)
            {
                spaces.Open_From = Convert.ToInt32(reader["open_from"]);
            }
            else
            {
                spaces.Open_From = 0;
            }

            if (reader["open_to"] != DBNull.Value)
            {
                spaces.Open_To = Convert.ToInt32(reader["open_to"]);
            }
            else
            {
                spaces.Open_To = 0;
            }

            return spaces;
        }
    }
}
