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
    public class TariffSolutionsQueryHandler : IQueryHandler<TariffSolutionsQuery, PagedResult<TariffSolutionDto>>
    {
        CargoContext dbContext;
        IMapper mapper;

        public TariffSolutionsQueryHandler(CargoContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<PagedResult<TariffSolutionDto>> Handle(TariffSolutionsQuery request, CancellationToken cancellationToken)
        {
            var task = Task.Run(() =>
                dbContext.TariffSolutions
                .AsNoTracking()
                .Include(x => x.AwbOriginAirport)
                .Include(x => x.AwbDestinationAirport)
                .Include(x => x.AwbOriginTariffGroup)
                .Include(x => x.AwbDestinationTariffGroup)
                .Include(x => x.TransitAirport)
                .Include(x => x.RateGrid)
                .ThenInclude(x => x.Ranks)
                .Include(x => x.RateGridRankValues)
                .Include(x => x.Addons)
                .ThenInclude(x => x.ShrCodes)
                .Where(x => x.ContragentId == request.ContragentId)
                .Page(new PageInfo { PageIndex = request.PageIndex, PageSize = request.PageSize })
            );
            return await mapper.Map<Task<PagedResult<TariffSolutionDto>>>(task);
        }
    }
}