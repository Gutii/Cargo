using AutoMapper;
using Cargo.Contract.DTOs.Tariffs;
using Cargo.Contract.Queries.Tariffs;
using Cargo.Infrastructure.Data;
using IDeal.Common.Components.Paginator;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.QueryHandlers.Tariffs
{
    public class TariffGroupsQueryHandler : IQueryHandler<TariffGroupsQuery, PagedResult<TariffGroupDto>>
    {
        CargoContext dbContext;
        IMapper mapper;

        public TariffGroupsQueryHandler(CargoContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<PagedResult<TariffGroupDto>> Handle(TariffGroupsQuery request, CancellationToken cancellationToken)
        {
            var task = Task.Run(() => dbContext.TariffGroups
            .AsNoTracking()
            .Include(tg => tg.Airports)
            .Where(x => x.ContragentId == request.ContragentId)
            .Page(new PageInfo { PageIndex = request.PageIndex, PageSize = request.PageSize }));

            return await mapper.Map<Task<PagedResult<TariffGroupDto>>>(task);
        }
    }
}