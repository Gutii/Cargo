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
    public class CarrierChargesQueryHandler : IQueryHandler<CarrierChargesQuery, PagedResult<CarrierChargeDto>>
    {
        CargoContext dbContext;
        IMapper mapper;

        public CarrierChargesQueryHandler(CargoContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<PagedResult<CarrierChargeDto>> Handle(CarrierChargesQuery request, CancellationToken cancellationToken)
        {
            var task = Task.Run(() =>
                dbContext.CarrierCharges
                .AsNoTracking()
                .Include(x => x.IncludedShrCodes)
                .Include(x => x.ExcludedShrCodes)
                .Include(x => x.IncludedProducts)
                .Include(x => x.ExcludedProducts)
                .Where(x => x.ContragentId == request.ContragentId)
                .Page(new PageInfo { PageIndex = request.PageIndex, PageSize = request.PageSize })
            );
            return await mapper.Map<Task<PagedResult<CarrierChargeDto>>>(task);
        }
    }
}