using Cargo.Infrastructure.Data.Migrations;
using Directories.Infrastructure.Data.Model.Settings.CarrierBookingProps;
using System.Collections.Generic;

namespace Cargo.Infrastructure.Data.Model.Settings
{
    public class Customer
    {
        public int Id { get; set; }
        public string IataCode { get; set; }
        public string AcPrefix { get; set; }
        public string AcMailPrefix { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public virtual ICollection<CarrierSettings> CarrierSettings { get; set; }
    }
}
