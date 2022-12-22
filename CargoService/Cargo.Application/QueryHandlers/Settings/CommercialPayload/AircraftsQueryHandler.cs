using AutoMapper;
using Cargo.Application.Services.CommPayload;
using Cargo.Contract.DTOs.Settings;
using Cargo.Contract.Queries.Settings;
using IDeal.Common.Components.Paginator;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.QueryHandlers.Settings
{
    public class AircraftsQueryHandler : IQueryHandler<AircraftsQuery, PagedResult<AircraftsDto>>
    {
        AircraftsService AircraftsService;
        IMapper mapper;

        public AircraftsQueryHandler(IMapper mapper, AircraftsService aircraftsService)
        {
            this.AircraftsService = aircraftsService;
            this.mapper = mapper;
        }

        public async Task<PagedResult<AircraftsDto>> Handle(AircraftsQuery request, CancellationToken cancellationToken)
        {
            var pageInfo = new PageInfo { PageIndex = request.PageIndex ?? 0, PageSize = request.PageSize ?? 20 };
            var a = AircraftsService.GetAircrafts(pageInfo, request.IataCode);
            return await mapper.Map<Task<PagedResult<AircraftsDto>>>(a);
        }
    }
}
