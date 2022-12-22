using System;


namespace Cargo.Contract.DTOs.Bookings
{
    public class AwbLightDto
    {
        public int? AwbId { get; set; }
        public int? BookingId { get; set; }
        public string AwbIdentification { get; set; }

        public string OriginAwb { get; set; }
        public string DestinationAwb { get; set; }
        public int NumberOfPieces { get; set; }
        public decimal Weight { get; set; }
        public decimal VolumeAmount { get; set; }
        public string ManifestDescriptionOfGoods { get; set; }
        public string Product { get; set; }
        public string Status { get; set; }
        /// <summary>
        /// Индикатор таможенной перевозки
        /// </summary>
        public string DiIndicator { get; set; }
        public string SpecialHandlingRequirements { get; set; }
        public int? ForwardingAgentId { get; set; }
        public string ForwardingAgent { get; set; }
        public string SpecialServiceRequest { get; set; }


        public int FlightId { get; set; }

        /// <summary> 
        /// KK, LL, NN 
        /// </summary> 
        public string SpaceAllocationCode { get; set; }


        public string Carrier { get; set; }

        public string FlightNumber { get; set; }
        public string FullFlights { get; set; }

        public string FlightOrigin { get; set; }
        public string FlightDestination { get; set; }

        public DateTime? FlightDate { get; set; }

        public DateTime? StDeparture { get; set; }
        public DateTime? StArrival { get; set; }
    }
}
