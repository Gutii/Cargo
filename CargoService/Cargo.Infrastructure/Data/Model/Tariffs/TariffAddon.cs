using Cargo.Infrastructure.Data.Model.Dictionary;
using System.Collections.Generic;

namespace Cargo.Infrastructure.Data.Model.Tariffs
{
    public class TariffAddon
    {
        public ulong Id { get; set; }
        public int WeightAddon { get; set; }
        public int MinimumAddon { get; set; }
        public ICollection<Shr> ShrCodes { get; set; }
        public TariffSolution TariffSolution { get; set; }
    }
}