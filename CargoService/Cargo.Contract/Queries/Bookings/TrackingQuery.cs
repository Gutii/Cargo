using Cargo.Contract.DTOs;
using IDeal.Common.Components.Paginator;

namespace Cargo.Contract.Queries.Bookings
{
    public class TrackingQuery : IQuery<PagedResult<ConsignmentStatusDto>>
    {
        public int Id { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
    }
}
