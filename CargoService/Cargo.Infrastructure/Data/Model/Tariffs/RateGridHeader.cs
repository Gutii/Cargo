using System.Collections.Generic;

namespace Cargo.Infrastructure.Data.Model.Tariffs
{
    public class RateGridHeader
    {
        public ulong Id { get; set; }

        public int ContragentId { get; set; }

        public string Code { get; set; }

        public virtual List<RateGridRank> Ranks { get; set; }

        public virtual List<TariffSolution> TariffSolutions { get; set; }
    }
}