using System;

namespace Cargo.Contract.DTOs.Quotas
{
    public class QuotasOperativeDto
    {
        public ulong Id { get; set; }
        public int? CarrierId { get; set; }
        public string AwbPrefix { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string Flight { get; set; }
        public string FlightLegFrom { get; set; }
        public string FlightLegTo { get; set; }
        public string WeekDay { get; set; }
        public string AwbOrigin { get; set; }
        public string AwbDest { get; set; }
        public string SalesProduct { get; set; }
        public string Shc { get; set; }
        public bool IsHardAllotment { get; set; }
        public int WeightLimit { get; set; }
        public int VolumeLimit { get; set; }
        public string Currency { get; set; }
    }
}
