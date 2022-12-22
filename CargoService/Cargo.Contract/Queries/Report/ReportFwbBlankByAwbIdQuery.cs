
namespace Cargo.Contract.Queries.Report
{
    public class ReportFwbBlankByAwbIdQuery : IQuery<byte[]>
    {
        public int awbId { get; set; }
    }
}
