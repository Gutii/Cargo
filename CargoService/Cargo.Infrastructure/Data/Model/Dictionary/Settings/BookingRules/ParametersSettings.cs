using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directories.Infrastructure.Data.Model.Settings.CarrierBookingProps
{
    public class ParametersSettings
    {
        public int Id { get; set; }
        public string FunctionalSection { get; set; }
        public int NumberGroupParameters { get; set; }
        public int NumberParameterOnGroup { get; set; }
        public string DescriptionRu { get; set; }
        public string DescriptionEn { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; }
        public string UnitMeasurement { get; set; }
        public virtual ICollection<CarrierSettings> CarrierSettings { get; set; }
    }
}
