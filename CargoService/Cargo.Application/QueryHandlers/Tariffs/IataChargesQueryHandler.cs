using AutoMapper;
using Cargo.Contract.DTOs.Tariffs;
using Cargo.Contract.Queries.Tariffs;
using Cargo.Infrastructure.Data;
using IDeal.Common.Components.Paginator;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.QueryHandlers.Tariffs
{
    public class IataChargesQueryHandler : IQueryHandler<IataChargesQuery, PagedResult<IataChargeDto>>
    {
        CargoContext dbContext;
        IMapper mapper;

        public IataChargesQueryHandler(CargoContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<PagedResult<IataChargeDto>> Handle(IataChargesQuery request, CancellationToken cancellationToken)
        {
            var task = Task.Run(() =>
                dbContext.IataCharges
                .AsNoTracking()
                .Page(new PageInfo { PageIndex = request.PageIndex, PageSize = request.PageSize })
            );
            return await mapper.Map<Task<PagedResult<IataChargeDto>>>(task);
        }
    }
}