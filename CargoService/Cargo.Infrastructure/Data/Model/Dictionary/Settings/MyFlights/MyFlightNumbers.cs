namespace Cargo.Infrastructure.Data.Model.Settings.MyFlights
{
    public class MyFlightNumbers
    {
        public int Id { get; set; }
        public int BeginNum { get; set; }
        public int? EndNum { get; set; }

        public int MyFlightsId { get; set; }
        public MyFlight MyFlights { get; set; }

    }
}
