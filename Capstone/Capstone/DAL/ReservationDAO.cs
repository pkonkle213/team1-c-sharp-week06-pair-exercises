using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public class ReservationDAO
    {
        private readonly string connectionString;

        public ReservationDAO(string databaseConnectionString)
        {
            this.connectionString = databaseConnectionString;
        }


    }
}
