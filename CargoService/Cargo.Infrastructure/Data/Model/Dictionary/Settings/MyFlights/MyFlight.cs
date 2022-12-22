using System;
using System.Collections.Generic;

namespace Cargo.Infrastructure.Data.Model.Settings.MyFlights
{
    public class MyFlight
    {
        public int Id { get; set; }
        public int CarrierId { get; set; }
        public Customer Carrier { get; set; }
        public string Agreement { get; set; }
        public DateTime DateAt { get; set; }
        public DateTime? DateTo { get; set; }

        public ICollection<MyFlightNumbers> MyFlightsNumbers { get; set; }
        public ICollection<MyFlightRoute> MyFlightsRoutes { get; set; }
    }
}
