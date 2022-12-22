using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directories.Contract.DTOs.Settings.BookingRules
{
    public class CarrierParametersSettingsDto
    {
        public int? CarrierId { get; set; }
        public int ParametersSettingsId { get; set; }
        public string Abbreviation { get; set; }
        public string DescriptionRu { get; set; }
        public string DescriptionEn { get; set; }
        public string Value { get; set; }
        public string UnitMeasurement { get; set; }
        public string IsDefault { get; set; }
    }
}
