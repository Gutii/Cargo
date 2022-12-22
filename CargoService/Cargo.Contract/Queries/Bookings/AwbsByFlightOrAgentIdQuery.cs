using Cargo.Contract.DTOs.Bookings;
using IDeal.Common.Components;
using IDeal.Common.Components.Paginator;

namespace Cargo.Contract.Queries.Bookings
{
    public class AwbsByFlightOrAgentIdQuery : IQuery<PagedResult<AwbLightDto>>, IAuthenticatedMessage
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public int? FlightId { get; set; }

        public int? AgentId { get; set; }
        public int? GhaId { get; set; }
        public int? CarrierId { get; set; }
        public string Language { get; set; }
        public int CustomerId { get; set; }
        public string SelectedRoleNameEn { get; set; }
    }
}
