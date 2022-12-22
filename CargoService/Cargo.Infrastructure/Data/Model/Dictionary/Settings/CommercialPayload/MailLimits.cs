using Cargo.Infrastructure.Data.Model.Dictionary;
using System;

namespace Cargo.Infrastructure.Data.Model.Settings
{
    public class MailLimits
    {
        public int Id { get; set; }
        public string IataCode { get; set; }
        public int AircraftTypeId { get; set; }
        public AircraftType AircraftType { get; set; }
        public int? AirlineId { get; set; }
        public Airline Airline { get; set; }
        public int? FromIataLocationId { get; set; }
        public IataLocation FromIataLocation { get; set; }
        public int? InIataLocationId { get; set; }
        public IataLocation InIataLocation { get; set; }
        public string FlightNumber { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public decimal MaxPayloadVolume { get; set; }
        public decimal MaxPayloadWeight { get; set; }
    }
}
