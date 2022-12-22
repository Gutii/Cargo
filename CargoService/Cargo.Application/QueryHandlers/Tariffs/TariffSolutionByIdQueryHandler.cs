using AutoMapper;
using Cargo.Contract.DTOs.Tariffs;
using Cargo.Contract.Queries.Tariffs;
using Cargo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.QueryHandlers.Tariffs
{
    public class TariffSolutionByIdQueryHandler : IQueryHandler<TariffSolutionByIdQuery, TariffSolutionDto>
    {
        CargoContext dbContext;
        IMapper mapper;

        public TariffSolutionByIdQueryHandler(CargoContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<TariffSolutionDto> Handle(TariffSolutionByIdQuery request, CancellationToken cancellationToken)
        {
            var tariffSolution = await dbContext.TariffSolutions
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
                .FirstOrDefaultAsync(x => x.Id == request.TariffSolutionId);

            return mapper.Map<TariffSolutionDto>(tariffSolution);
        }
    }
}
