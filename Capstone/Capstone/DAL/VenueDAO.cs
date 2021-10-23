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
            "FROM venue " +
            "ORDER BY name ASC";

        private const string SqlSelectVenueDetails =
            "SELECT v.name AS VenueName,v.description,c.name AS CityName,c.state_abbreviation AS St " +
            "FROM venue v " +
            "INNER JOIN city c ON v.city_id = c.id " +
            "WHERE v.id = @id";

        private const string SqlSelectCategories =
            "SELECT c.name AS CategoryName " +
            "FROM venue v " +
            "INNER JOIN category_venue cv ON cv.venue_id = v.id " +
            "INNER JOIN category c ON c.id = cv.category_id " +
            "WHERE v.id = @id";

        public VenueDAO(string databaseConnectionString)
        {
            this.connectionString = databaseConnectionString;
        }

        public List<Venue> GetVenues()
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

        public Venue GetSpecificVenue(Venue venue)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(SqlSelectVenueDetails, conn);
                    command.Parameters.AddWithValue("@id", venue.ID);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        venue = AddCitySt(venue, reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Unable to read the database:  " + ex.Message);
            }

            return venue;
        }

        public List<string> GetCategories(Venue venue)
        {
            List<string> categories = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(SqlSelectCategories, conn);
                    command.Parameters.AddWithValue("@id", venue.ID);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        categories.Add(Convert.ToString(reader["CategoryName"]));
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Unable to read the database: " + ex.Message);
            }

            return categories;
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

        private Venue AddCitySt(Venue venue, SqlDataReader reader)
        {
            venue.City_Name = Convert.ToString(reader["CityName"]);
            venue.State_Abbreviation = Convert.ToString(reader["St"]);

            return venue;
        }
    }
}
