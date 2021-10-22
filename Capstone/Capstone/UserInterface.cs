using Capstone.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;
using Capstone.DAL;

namespace Capstone
{
    /// <summary>
    /// This class is responsible for representing the main user interface to the user.
    /// </summary>
    /// <remarks>
    /// ALL Console.ReadLine and WriteLine in this class
    /// NONE in any other class. 
    ///  
    /// The only exceptions to this are:
    /// 1. Error handling in catch blocks
    /// 2. Input helper methods in the CLIHelper.cs file
    /// 3. Things your instructor explicitly says are fine
    /// 
    /// No database calls should exist in classes outside of DAO objects
    /// </remarks>
    public class UserInterface
    {
        private readonly string connectionString;

        private readonly VenueDAO venueDAO;

        private readonly SpacesDAO spacesDAO;

        public UserInterface(string connectionString)
        {
            this.connectionString = connectionString;
            venueDAO = new VenueDAO(connectionString);
            spacesDAO = new SpacesDAO(connectionString);
        }

        public void Run()
        {
            Console.WriteLine("Welcome to Excelsior Venues!");
            Console.WriteLine();

            bool quit = false;
            while (!quit)
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1) List Venues");
                Console.WriteLine("Q) Quit");

                string answer = Console.ReadLine();

                if (answer == "1")
                {
                    ViewVenues();
                }
                else if (answer.ToLower() == "q")
                {
                    quit = true;
                }
                else
                {
                    Console.WriteLine("Please try reading the instructions and not being a jerk.");
                    Console.WriteLine();
                }
            }
        }

        public void ViewVenues()
        {
            Console.Clear();
            bool quit = false;
            while (!quit)
            {
                Console.WriteLine("Which venue would you like to view?");
                //Call a method to form a list of available venues
                List<Venue> venues = venueDAO.GetVenues();
                //Display each venue by a foreach(List<Venue> venue in venues){} loop
                for (int i = 0; i < venues.Count; i++)
                {
                    Console.WriteLine($"{i+1}) {venues[i].Name}");
                }
                Console.WriteLine("R) Return to Previous Screen");
                Console.WriteLine();
                string answer = Console.ReadLine();
                Console.Clear();
                int answerI;

                if (answer.ToLower() == "r")
                {
                    quit = true;
                }
                else if (!int.TryParse(answer,out answerI))
                {
                    Console.WriteLine("Please try again");

                }
                else
                {
                    //Takes the ID entered by the user, converts it to a number, and displays the information accordingly
                    ViewVenueDetails(venues[answerI - 1]);
                }
            }
        }

        public void ViewVenueDetails(Venue venue)
        {

            Console.WriteLine(venue.Name);
            venue = venueDAO.GetSpecificVenue(venue);
            Console.WriteLine($"Location: {venue.City_Name}, {venue.State_Abbreviation}");
            List<string> categories = venueDAO.GetCategories(venue);
            Console.Write($"Categories: ");
            foreach (string item in categories)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            Console.WriteLine(venue.Description);
            Console.WriteLine();

            bool quit = false;
            while (!quit)
            {
                Console.WriteLine("What would you like to do next?");
                Console.WriteLine("1) View Spaces");
                Console.WriteLine("2) Search for Reservation");
                Console.WriteLine("R) Return to Previous Screen");
                Console.WriteLine();
                string answer = Console.ReadLine();

                if (answer.ToLower() == "r")
                {
                    quit = true;
                }
                else if (answer == "1")
                {
                    ListVenueSpace(venue);
                }
                else if (answer == "2")
                {
                    
                }
                else
                {
                    Console.Write("No you are wrong sir, please try again! ");
                }
                        
            }

        }

        public void ListVenueSpace(Venue venue)
        {
            Console.Clear();
            Console.WriteLine(venue.Name + " Spaces");
            List<Spaces> spaces = spacesDAO.GetSpaces(venue);
            const int padName = 20;
            const int padOpen = 7;
            const int padClose = 7;
            const int padRate = 15;
            Console.WriteLine("     "+"Name".PadRight(padName) + "Open".PadRight(padOpen) + "Close".PadRight(padClose) + "  Daily Rate".PadRight(padRate) + "   Max. Occupancy");
            for (int i = 0; i < spaces.Count; i++)
            {
                Console.WriteLine($"#{i + 1}) {spaces[i].Name.PadRight(padName)} {spaces[i].From_Month.PadRight(padOpen)} {spaces[i].To_Month.PadRight(padClose)} {spaces[i].Daily_Rate.ToString("C").PadRight(padRate)} {spaces[i].Max_Occupancy}");
            }
            
            bool quit = false;
            while (!quit)
            {
                Console.WriteLine();
                Console.WriteLine("What would you like to do next?");
                Console.WriteLine("1) Reserve a Space");
                Console.WriteLine("R) Return to Previous Screen");
                Console.WriteLine();
                string answer = Console.ReadLine();

                if (answer.ToLower() == "r")
                {
                    quit = true;
                }

            }
        }


    }
}
