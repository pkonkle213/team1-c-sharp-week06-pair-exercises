using Capstone.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

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

        private readonly ReservationDAO reservationDAO;

        public UserInterface(string connectionString)
        {
            this.connectionString = connectionString;
            venueDAO = new VenueDAO(connectionString);
            spacesDAO = new SpacesDAO(connectionString);
            reservationDAO = new ReservationDAO(connectionString);
        }

        //Main menu that we have
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
                Console.WriteLine();

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
                    Console.WriteLine("Please select one of the two possible options.");
                    Console.WriteLine();
                }
            }
        }

        // Below resolves MVP #1 with a User System.
        public void ViewVenues()
        {
            Console.Clear(); // This is clearing the page
            bool quit = false;
            while (!quit)
            {
                Console.WriteLine("Which venue would you like to view?");
                //Call a method to form a list of available venues
                List<Venue> venues = venueDAO.GetVenues();
                //Display each venue by a foreach(List<Venue> venue in venues){} loop
                //Displayed a for loop instead of a foreach so we could reference the index
                for (int i = 0; i < venues.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) {venues[i].Name}");
                }
                Console.WriteLine("R) Return to Previous Screen");
                Console.WriteLine();
                string answer = Console.ReadLine();
                Console.Clear();
                int answerI;
                //Validating the users input information
                if (answer.ToLower() == "r")
                {
                    quit = true;
                }
                else if (!int.TryParse(answer, out answerI))
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

        //This takes care of MVP 2.1 comment
        public void ViewVenueDetails(Venue venue)
        {
            Console.WriteLine(venue.Name);
            venue = venueDAO.GetSpecificVenue(venue);
            Console.WriteLine($"Location: {venue.City_Name}, {venue.State_Abbreviation}");
            List<string> categories = venueDAO.GetCategories(venue);
            Console.Write($"Categories: ");
            //Listing the categories in the Venue
            foreach (string item in categories)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            Console.WriteLine(venue.Description);
            Console.WriteLine();
            //Doing another menu and seeing what the User would like
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
                    Console.WriteLine("This function has yet to be completed. Version 2.0 coming out soon!");
                }
                else
                {
                    Console.Write("Please select a valid option.");
                }

            }

        }
        
        public void ListVenueSpace(Venue venue)
        {
            Console.Clear();
            Console.WriteLine(venue.Name + " Spaces");
            List<Spaces> spaces = spacesDAO.GetSpaces(venue);
            const int padName = 35;
            const int padOpen = 10;
            const int padClose = 10;
            const int padRate = 20;
            const int padAccess = 15;
            //This takes care of MVP #2.2 where a space is blocked out for various months
            Console.WriteLine("    " + "Name".PadRight(padName) + "Open".PadRight(padOpen) + "Close".PadRight(padClose) + "Daily Rate".PadRight(padRate) + "Is Accessible".PadRight(padAccess) + "Max. Occupancy");
            for (int i = 0; i < spaces.Count; i++)
            {
                Console.WriteLine($"#{i + 1}) {spaces[i].Name.PadRight(padName)}{spaces[i].From_Month.PadRight(padOpen)}{spaces[i].To_Month.PadRight(padClose)}{spaces[i].Daily_Rate.ToString("C").PadRight(padRate)}{spaces[i].Is_Accessible.PadRight(padAccess)}{spaces[i].Max_Occupancy}");
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
                else if (answer == "1")
                {
                    ReserveSpace(venue);
                }
                else
                {
                    Console.WriteLine("Please select a valid option.");
                }
            }
        }
        //This takes care of MVP 3
        public void ReserveSpace(Venue venue)
        {
            try
            {

                Console.Clear();
                Console.Write("When do you need the space? (MM/DD/YYYY) ");
                string answerDate = Console.ReadLine();
                DateTime fromDate = Convert.ToDateTime(answerDate);
                Console.Write("How many days will you need the space? ");
                string answerDays = Console.ReadLine();
                int days = Convert.ToInt32(answerDays);
                DateTime endDate = fromDate.AddDays(days);
                Console.Write("How many people will be in attendance? ");
                string answerPeople = Console.ReadLine();
                int occupancy = Convert.ToInt32(answerPeople);
                Console.WriteLine();
                List<Spaces> available = spacesDAO.GetAvailableSpaces(venue, fromDate, endDate, occupancy);
                if (available.Count == 0)
                {
                    //This takes care of MVP 3.3
                    Console.WriteLine("No spaces available please try different venue");
                }
                else
                {
                    Console.WriteLine("The following spaces are available based on your needs: ");

                    const int padNumber = 10;
                    const int padName = 30;
                    const int padRate = 15;
                    const int padMaxOcc = 15;
                    const int padAccess = 12;
                    Console.WriteLine("Space #".PadRight(padNumber) + "Name".PadRight(padName) + "Daily Rate".PadRight(padRate) + "Max Occup.".PadRight(padMaxOcc) + "Accessible".PadRight(padAccess) + "Total Cost");
                    List<int> acceptableIDs = new List<int>();
                    foreach (Spaces space in available)
                    {
                        Console.WriteLine($"{space.Id.ToString().PadRight(padNumber)}{space.Name.PadRight(padName)}{space.Daily_Rate.ToString("C").PadRight(padRate)}{space.Max_Occupancy.ToString().PadRight(padMaxOcc)}{space.Is_Accessible.ToString().PadRight(padAccess)}{(space.Daily_Rate * days).ToString("C")}");
                        acceptableIDs.Add(space.Id);
                    }

                    // MVP 4.0
                    Console.WriteLine();
                    Console.Write("Which space would you like to reserve (enter 0 to cancel)? ");
                    string answerReserve = Console.ReadLine();

                    if (answerReserve != "0")
                    {
                        //test if the answer was legit space: if List<space> contains answer
                        try
                        {
                            int answerID = Convert.ToInt32(answerReserve);

                            if (acceptableIDs.Contains(answerID))
                            {
                                Console.Write("Who is this reservation for? ");
                                string answerName = Console.ReadLine();
                                //Add the reservation to the table
                                int confirmation = reservationDAO.SubmitReservation(Convert.ToInt32(answerReserve), fromDate, endDate, answerName, Convert.ToInt32(answerPeople));


                                Console.WriteLine();
                                Console.WriteLine("Thanks for submitting your reservation! The details for your event are listed below:");
                                Console.WriteLine();

                                Spaces space = spacesDAO.GetSpecificSpace(answerID);

                                //Output the confirmation information 
                                const int pad = 18;
                                Console.WriteLine("Confirmation #: ".PadLeft(pad) + confirmation);
                                Console.WriteLine("Venue: ".PadLeft(pad) + venue.Name);
                                Console.WriteLine("Space: ".PadLeft(pad) + space.Name);
                                Console.WriteLine("Reserved For: ".PadLeft(pad) + answerName);
                                Console.WriteLine("Attendees: ".PadLeft(pad) + answerPeople);
                                Console.WriteLine("Arrival Date: ".PadLeft(pad) + fromDate.ToString("d"));
                                Console.WriteLine("Depart Date: ".PadLeft(pad) + endDate.ToString("d"));
                                Console.WriteLine("Total Cost: ".PadLeft(pad) + (space.Daily_Rate * days).ToString("C"));
                            }
                        }

                        catch (FormatException ex)
                        {
                            Console.WriteLine("Please type a valid a option: " + ex.Message);
                        }


                    }
                }
            }

            catch (FormatException ex)
            {
                Console.WriteLine("Please type a valid a option: " + ex.Message);
            }

        }

        
    }
}