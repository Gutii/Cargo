using Cargo.Contract.DTOs.Tariffs;
using IDeal.Common.Components.Paginator;

namespace Cargo.Contract.Queries.Tariffs
{
    public class IataChargesQuery : IQuery<PagedResult<IataChargeDto>>
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}