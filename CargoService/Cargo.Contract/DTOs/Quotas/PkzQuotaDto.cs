using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Contract.DTOs.Quotas
{
    public class PkzQuotaDto
    {
        public ulong FlightSheduleId { get; set; }
        public bool IsHardAllotment { get; set; }
        public int AgentId { get; set; }
        public List<int> AwbsId { get; set; }
        public decimal QuotaVolume { get; set; }
        public decimal QuotaWeight { get; set; }
        public decimal BusyVolume { get; set; }
        public decimal BusyWeight { get; set; }
    }
}
