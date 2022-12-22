using Cargo.Contract.DTOs.Settings;
using IDeal.Common.Components.Paginator;

namespace Cargo.Contract.Queries.Settings
{
    public class AircraftsQuery : IQuery<PagedResult<AircraftsDto>>
    {
        public int? PageIndex { get; set; }

        public int? PageSize { get; set; }
        public string IataCode { get; set; }
    }
}
