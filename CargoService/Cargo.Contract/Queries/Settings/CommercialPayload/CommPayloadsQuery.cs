using Cargo.Contract.DTOs.Settings;
using IDeal.Common.Components.Paginator;

namespace Cargo.Contract.Queries.CommPayloads
{
    public class CommPayloadsQuery : IQuery<PagedResult<CommPayloadDto>>
    {
        public int? PageIndex { get; set; }

        public int? PageSize { get; set; }
        public string IataCode { get; set; }
    }
}
