using Cargo.Infrastructure.Data.Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directories.Infrastructure.Data.Model.Settings.CarrierBookingProps
{
    public class CarrierSettings
    {
        public int Id { get; set; }
        public int CarrierId { get; set; }
        public int ParametersSettingsId { get; set; }
        public ParametersSettings ParametersSettings { get; set; }
        public string Value { get; set; }
    }
}
