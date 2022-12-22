using System;

namespace Cargo.Contract.Queries.Report
{
    public class ReportBookingsPerPeriodQuery : IQuery<byte[]>
    {
        public int agentId { get; set; }
        public DateTime beginDate { get; set; }
        public DateTime endDate { get; set; }
    }
}
