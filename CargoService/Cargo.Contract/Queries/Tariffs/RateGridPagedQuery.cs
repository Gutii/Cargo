using Cargo.Contract.Common;

namespace Cargo.Contract.Queries.Tariffs
{
    public class RateGridPagedQuery : IContragentSpecific
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int ContragentId { get; set; }
    }
}