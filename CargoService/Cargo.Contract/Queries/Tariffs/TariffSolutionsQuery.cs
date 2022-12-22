using Cargo.Contract.Common;
using Cargo.Contract.DTOs.Tariffs;
using IDeal.Common.Components.Paginator;

namespace Cargo.Contract.Queries.Tariffs
{
    public class TariffSolutionsQuery : IQuery<PagedResult<TariffSolutionDto>>, IContragentSpecific
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int ContragentId { get; set; }
    }
}