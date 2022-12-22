using Cargo.Contract.DTOs;
using IDeal.Common.Components.Paginator;

namespace Cargo.Contract.Queries
{
    public class AircraftTypesQuery : IQuery<PagedResult<AircraftTypeDto>>
    {

        public PageInfo Paging { get; set; }
    }
}
