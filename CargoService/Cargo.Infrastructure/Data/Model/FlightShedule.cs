using IDeal.Common.Components;
using System;
using System.Collections.Generic;

namespace Cargo.Infrastructure.Data.Model
{
    public class FlightShedule
    {
        public ulong Id { get; set; }
        public ulong ExternalId { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Number { get; set; }
        public DateTime FlightDate { get; set; }
        public DateTime StOrigin { get; set; }
        public DateTime StDestination { get; set; }
        public DateTime StOriginLocal { get; set; }
        public DateTime StDestinationLocal { get; set; }
        public int State { get; set; }
        public string AircraftRegistration { get; set; }
        public string AircraftType { get; set; }
        public string SHR { get; set; }
        public double? PayloadWeight { get; set; }
        public double? PayloadVolume { get; set; }
        public FlightSaleState SaleState { get; set; }

        public ICollection<Booking> Bookings { get; set; }
        public ICollection<BookingRcs> BookingRcs { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
