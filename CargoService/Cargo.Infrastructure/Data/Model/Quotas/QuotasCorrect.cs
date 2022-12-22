using Cargo.Infrastructure.Data.Model.Dictionary.Settings;

namespace Cargo.Infrastructure.Data.Model.Quotas
{
    public class QuotasCorrect
    {
        public ulong QuotasOperativeId { get; set; }
        public QuotasOperative QuotasOperative { get; set; }
        public int CarrierId { get; set; }
        public ulong FlightId { get; set; }
        public FlightShedule FlightShedule { get; set; }
        public int AgentId { get; set; }
        public Agent Agent { get; set; }
        public int WeightLimit { get; set; }
        public int VolumeLimit { get; set; }
    }
}
