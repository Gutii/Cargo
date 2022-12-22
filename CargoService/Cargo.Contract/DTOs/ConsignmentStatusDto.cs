using System;

namespace Cargo.Contract.DTOs
{
    public class ConsignmentStatusDto
    {
        public Guid Id { get; set; }
        public string AwbIdentifier { get; set; }
        public DateTime DateChange { get; set; }
        public string StatusCode { get; set; }
        public string AirportCode { get; set; }
        public QuantityDetailDto QuantityDetail { get; set; }
        public MovementDetailDto MovementDetail { get; set; }
        public string Source { get; set; }
        public string TitleRu { get; set; }
        public string TitleEn { get; set; }
    }

    public class QuantityDetailDto
    {
        public int NumberOfPiece { get; set; }
        public double? Weight { get; set; }
        public double? VolumeAmount { get; set; }
    }

    public class MovementDetailDto
    {
        public string FlightNumber { get; set; }
        public DateTime FlightDate { get; set; }
    }
}
