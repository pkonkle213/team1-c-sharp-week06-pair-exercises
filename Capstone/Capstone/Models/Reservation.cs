using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Reservation
    {
        public int Reservation_ID { get; set; }
        public int Space_ID { get; set; }
        public int Number_Of_Attendees { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public string Reserved_For { get; set; }
        public int Length_Of_Stay
        {
            get
            {
                return Convert.ToInt32(End_Date.Subtract(Start_Date));
                //Does this work? Please? Let it?
            }
        }


    }

}
