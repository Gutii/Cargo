namespace Cargo.Infrastructure.Data.Model.Settings.MyFlights
{
    public class MyFlightRoute
    {
        public int Id { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }

        public int MyFlightsId { get; set; }
        public MyFlight MyFlights { get; set; }

    }
}
