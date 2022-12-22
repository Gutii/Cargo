using Cargo.Infrastructure.Data.Model.Dictionary;
using System;

namespace Cargo.Infrastructure.Data.Model.Settings
{
    //Управление ПКЗ для собственного парка по типам ВС
    public class CommPayloadAt
    {
        /// <summary>
        /// Identifier
        /// </summary>
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
        public int MaxPayloadVolume { get; set; }
        public int MaxPayloadWeight { get; set; }
        public string AccseptedShr { get; set; }
    }
}
