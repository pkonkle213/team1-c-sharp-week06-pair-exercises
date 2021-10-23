using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Spaces
    {


        public int Id { get; set; }
        //public int Venue_Id { get; set; }
        public string Name { get; set; }
        public int Open_From { get; set; }
        public int Open_To { get; set; }
        public string Is_Accessible { get; set; }
        public decimal Daily_Rate { get; set; }
        public int Max_Occupancy { get; set; }
        public string From_Month
        {
            get
            {
                string month;
                switch (Open_From)
                {

                    case 1:
                        month = "Jan.";
                        break;

                    case 2:
                        month = "Feb.";
                        break;

                    case 3:
                        month = "Mar.";
                        break;

                    case 4:
                        month = "Apr.";
                        break;

                    case 5:
                        month = "May.";
                        break;

                    case 6:
                        month = "Jun.";
                        break;

                    case 7:
                        month = "Jul.";
                        break;

                    case 8:
                        month = "Aug.";
                        break;

                    case 9:
                        month = "Sep.";
                        break;

                    case 10:
                        month = "Oct.";
                        break;

                    case 11:
                        month = "Nov.";
                        break;

                    case 12:
                        month = "Dec.";
                        break;

                    default:
                        month = " ";
                        break;

                }
                return month;
            }
        }

        public string To_Month
        {
            get
            {
                string month;
                switch (Open_To)
                {
                    case 1:
                        month = "Jan.";
                        break;

                    case 2:
                        month = "Feb.";
                        break;

                    case 3:
                        month = "Mar.";
                        break;

                    case 4:
                        month = "Apr.";
                        break;

                    case 5:
                        month = "May.";
                        break;

                    case 6:
                        month = "Jun.";
                        break;

                    case 7:
                        month = "Jul.";
                        break;

                    case 8:
                        month = "Aug.";
                        break;

                    case 9:
                        month = "Sep.";
                        break;

                    case 10:
                        month = "Oct.";
                        break;

                    case 11:
                        month = "Nov.";
                        break;

                    case 12:
                        month = "Dec.";
                        break;

                    default:
                        month = " ";
                        break;
                }

                return month;
            }
        }
    }
}