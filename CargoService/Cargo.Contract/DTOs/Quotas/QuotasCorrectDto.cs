using IDeal.Common.Components.Messages.ObjectStructures.Fwbs.Ver17;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Contract.DTOs.Quotas
{
    public class QuotasCorrectDto
    {
        public ulong QuotasOperativeId { get; set; }
        public QuotasOperativeDto QuotasOperative { get; set; }
        public int CarrierId { get; set; }
        public ulong FlightId { get; set; }
        public FlightSheduleDto FlightShedule { get; set; }
        public int AgentId { get; set; }
        public Agent Agent { get; set; }
        public int WeightLimit { get; set; }
        public int VolumeLimit { get; set; }
    }
}
