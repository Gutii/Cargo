using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Application.CsvMapper
{
    [Delimiter(",")]
    public class QuotasOperativeCsvMapper
    {
        [Ignore]
        public ulong Id { get; set; }
        [Index(0)]
        public int CarrierId { get; set; }
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
