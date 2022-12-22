using Cargo.Infrastructure.Data.Model.Tariffs;
using System.Collections.Generic;

namespace Cargo.Infrastructure.Data.Model.Dictionary
{
    public class Shr
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string InternationalDescr { get; set; }
        public string RussianDescr { get; set; }
        public string ShrGroup { get; set; }
        public string ForbShrGroupIata { get; set; }
        public string RecomendedShr { get; set; }

        public ICollection<TransportProduct> TransportProducts { get; set; }
        public ICollection<TariffAddon> TariffAddons { get; set; }
        public ICollection<CarrierCharge> IncludingCarrierCharges { get; set; }
        public ICollection<CarrierCharge> ExcludingCarrierCharges { get; set; }
    }
}