using AutoMapper;
using Cargo.Application.Services.Settings;
using Cargo.Contract.DTOs.Settings;
using Cargo.Contract.Queries.CommPayloads;
using Cargo.Infrastructure.Data.Model.Settings;
using IDeal.Common.Components.Paginator;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.QueryHandlers.Settings
{
    public class CommPayloadsQueryHandler : IQueryHandler<CommPayloadsQuery, PagedResult<CommPayloadDto>>
    {
        IMapper Mapper;
        CommPayloadsService commPayloadsService;

        public CommPayloadsQueryHandler(IMapper mapper, CommPayloadsService commPayloadsService)
        {
            Mapper = mapper;
            this.commPayloadsService = commPayloadsService;
        }

        public async Task<PagedResult<CommPayloadDto>> Handle(CommPayloadsQuery request, CancellationToken cancellationToken)
        {
            var pageInfo = new PageInfo { PageIndex = request.PageIndex ?? 0, PageSize = request.PageSize ?? 20 };
            var task = commPayloadsService.GetPayloads(pageInfo, request.IataCode);
            return await Mapper.Map<Task<PagedResult<CommPayloadAt>>, Task<PagedResult<CommPayloadDto>>>(task);
        }
    }
}
