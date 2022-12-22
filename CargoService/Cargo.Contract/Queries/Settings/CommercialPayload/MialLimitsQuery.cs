using Cargo.Contract.DTOs.Settings.CommercialPayload;
using IDeal.Common.Components.Paginator;

namespace Cargo.Contract.Queries.Settings.CommercialPayload
{
    public class MailLimitsQuery : IQuery<PagedResult<MailLimitsDto>>
    {
        public PageInfo PageInfo { get; set; }
        public string IataCode { get; set; }
    }
}
