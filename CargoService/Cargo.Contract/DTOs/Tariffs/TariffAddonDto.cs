using System.Collections.Generic;

namespace Cargo.Contract.DTOs.Tariffs
{
    public class TariffAddonDto
    {
        public ulong Id { get; set; }

        public int MinimumAddon { get; set; }

        public int WeightAddon { get; set; }

        public List<ShrDto> ShrCodes { get; set; }
    }
}