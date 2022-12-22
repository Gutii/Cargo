using AutoMapper;
using Cargo.Contract.DTOs.Tariffs;
using Cargo.Contract.Queries.Tariffs;
using Cargo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.QueryHandlers.Tariffs
{
    public class CarrierChargeByIdQueryHandler : IQueryHandler<CarrierChargeByIdQuery, CarrierChargeDto>
    {
        CargoContext dbContext;
        IMapper mapper;

        public CarrierChargeByIdQueryHandler(CargoContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<CarrierChargeDto> Handle(CarrierChargeByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await dbContext.CarrierCharges
                .AsNoTracking()
                .Include(x => x.IncludedShrCodes)
                .Include(x => x.ExcludedShrCodes)
                .Include(x => x.IncludedProducts)
                .Include(x => x.ExcludedProducts)
                .Include(x => x.CarrierChargeBindings)
                .ThenInclude(x => x.Currency)
                .Include(x => x.CarrierChargeBindings)
                .ThenInclude(x => x.Country)
                .Include(x => x.CarrierChargeBindings)
                .ThenInclude(x => x.Airports)
                .FirstOrDefaultAsync(x => x.Id == request.CarrierChargeId);

            return mapper.Map<CarrierChargeDto>(result);
        }
    }
}