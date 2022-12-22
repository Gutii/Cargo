using System;

namespace Cargo.Infrastructure.Data.Model
{
    public class ConsignmentStatus
    {
        public Guid Id { get; set; }
        public int AwbId { get; set; }
        public DateTime DateChange { get; set; }
        public string StatusCode { get; set; }
        public string AirportCode { get; set; }
        public int NumberOfPiece { get; set; }
        public double? Weight { get; set; }
        public double? VolumeAmount { get; set; }
        public string FlightNumber { get; set; }
        public DateTime? FlightDate { get; set; }
        public string Source { get; set; }
        public string TitleRu { get; set; }
        public string TitleEn { get; set; }
        public Awb Awb { get; set; }
    }
}
