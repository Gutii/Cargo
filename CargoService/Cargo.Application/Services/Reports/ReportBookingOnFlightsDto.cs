using System;

namespace Cargo.Application.Services.Reports
{
    public class ReportBookingOnFlightsDto
    {
        public ulong FlightId { get; set; }
        public string FlightOrigin { get; set; }
        public string FlightDestination { get; set; }
        public string FlightNumber { get; set; }
        public string TypeVs { get; set; }
        public string FlightAircraftType { get; set; }
        public DateTime FlightStDestination { get; set; }
        public DateTime FlightDate { get; set; }
        public double WeightFact { get; set; }
        public double VolumeFact { get; set; }
        public double WeightPlan { get; set; }
        public double VolumePlan { get; set; }
    }
}
