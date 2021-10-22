using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Spaces
    {


        public int Id { get; set; }
        public int Venue_Id { get; set; }
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
                switch (Open_From)
                {
                    case 1:
                        return "Jan.";
                        break;

                    case 2:
                        return "Feb.";
                        break;

                    case 3:
                        return "Mar.";
                        break;

                    case 4:
                        return "Apr.";
                        break;

                    case 5:
                        return "May.";
                        break;

                    case 6:
                        return "Jun.";
                        break;

                    case 7:
                        return "Jul.";
                        break;

                    case 8:
                        return "Aug.";
                        break;

                    case 9:
                        return "Sep.";
                        break;

                    case 10:
                        return "Oct.";
                        break;

                    case 11:
                        return "Nov.";
                        break;

                    case 12:
                        return "Dec.";
                        break;

                    default:
                        return " ";
                        break;

                }
            }
        }

        public string To_Month
        {
            get
            {
                switch (Open_To)
                {
                    case 1:
                        return "Jan.";
                        break;

                    case 2:
                        return "Feb.";
                        break;

                    case 3:
                        return "Mar.";
                        break;

                    case 4:
                        return "Apr.";
                        break;

                    case 5:
                        return "May.";
                        break;

                    case 6:
                        return "Jun.";
                        break;

                    case 7:
                        return "Jul.";
                        break;

                    case 8:
                        return "Aug.";
                        break;

                    case 9:
                        return "Sep.";
                        break;

                    case 10:
                        return "Oct.";
                        break;

                    case 11:
                        return "Nov.";
                        break;

                    case 12:
                        return "Dec.";
                        break;

                    default:
                        return " ";
                        break;

                }
            }
        }
    }
}