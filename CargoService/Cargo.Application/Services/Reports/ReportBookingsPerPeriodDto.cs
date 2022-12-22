using System;

namespace Cargo.Application.Services.Reports
{
    public class ReportBookingsPerPeriodDto
    {
        public string AwbNumder { get; set; }
        public string AwbOrigin { get; set; }
        public string AwbDestination { get; set; }
        public int AwbNumberOfPieces { get; set; }
        public decimal AwbWeight { get; set; }
        public decimal AwbVolume { get; set; }
        public int? AwbAgentId { get; set; }
        public string AwbAgent { get; set; }
        public decimal BookingNumberOfPieces { get; set; }
        public decimal BookingWeight { get; set; }
        public decimal BookingVolume { get; set; }
        public string BookingSpaceAllocationCode { get; set; }
        public ulong BookingFlightSceduleId { get; set; }
        public string FlightNumber { get; set; }
        public DateTime FlightDate { get; set; }
        public string FlightOrigin { get; set; }
        public string FlightDestination { get; set; }

    }
}
