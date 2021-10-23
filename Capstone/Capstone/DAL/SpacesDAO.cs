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
            "SELECT TOP 5 s.id, s.name,open_from,open_to,daily_rate,is_accessible,max_occupancy " +
            "FROM space s " +
            "INNER JOIN venue v ON v.id=s.venue_id " +
            "WHERE v.id = @id";

        private const string SqlSelectSpecificSpace =
            "SELECT * " +
            "FROM space " +
            "WHERE id = @newId";

        private const string SqlSelectSpaces =
            "SELECT s.id, s.name, s.daily_rate, s.max_occupancy, s.is_accessible, s.open_from, s.open_to " +
            "FROM space s " +
            "WHERE s.venue_id = @vId " +
            "AND s.open_from <= @sOpenFromM " +
            "AND s.open_to >= @sOpenToM " +
            "AND s.max_occupancy >= @uioccupancy " +
            "AND NOT EXISTS ( " +
            "SELECT * " +
            "FROM reservation r " +
            "WHERE r.space_id = s.id AND r.start_date <= @endDate AND r.end_date >= @startDate) " +
            "UNION " +
            "SELECT s.id, s.name, s.daily_rate, s.max_occupancy, s.is_accessible, s.open_from, s.open_to " +
            "FROM space s " +
            "WHERE s.venue_id = @vId " +
            "AND s.open_from IS NULL " +
            "AND s.max_occupancy >= @uioccupancy " +
            "AND NOT EXISTS ( " +
            "SELECT * " +
            "FROM reservation r " +
            "WHERE r.space_id = s.id AND r.start_date <= @endDate AND r.end_date >= @startDate)";

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
                    command.Parameters.AddWithValue("@vId", venue.ID);
                    command.Parameters.AddWithValue("@sOpenFromM", fromDate.Month);
                    command.Parameters.AddWithValue("@sOpenToM", endDate.Month);
                    command.Parameters.AddWithValue("@endDate", endDate);
                    command.Parameters.AddWithValue("@startDate", fromDate);
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

        public Spaces GetSpecificSpace(int id)
        {
            Spaces space = new Spaces();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(SqlSelectSpecificSpace, conn);
                    command.Parameters.AddWithValue("@newId", id);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        space = BuildSpecificSpace(reader, id);
                    }
                }
            }
            catch(SqlException ex)
            {
                Console.WriteLine("Unable to read the database: " + ex.Message);

            }

            return space;
        }

        private Spaces BuildSpecificSpace(SqlDataReader reader,int id)
        {
            Spaces spaces = new Spaces();
            spaces.Name = Convert.ToString(reader["name"]);
            spaces.Daily_Rate = Convert.ToDecimal(reader["daily_rate"]);

            return spaces;
        }

        private Spaces BuildSpacesFromReader(SqlDataReader reader)
        {
            Spaces spaces = new Spaces();
            spaces.Id = Convert.ToInt32(reader["id"]);
            spaces.Name = Convert.ToString(reader["name"]);
            spaces.Daily_Rate = Convert.ToDecimal(reader["daily_rate"]);
            spaces.Max_Occupancy = Convert.ToInt32(reader["max_occupancy"]);

            if (Convert.ToBoolean(reader["is_accessible"]) == true)
            {
                spaces.Is_Accessible = "Yes";
            }

            if (Convert.ToBoolean(reader["is_accessible"]) == false)
            {
                spaces.Is_Accessible = "No";
            }

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