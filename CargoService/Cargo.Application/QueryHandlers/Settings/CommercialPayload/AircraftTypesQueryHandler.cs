using AutoMapper;
using Cargo.Contract.DTOs;
using Cargo.Contract.Queries;
using Cargo.Infrastructure.Data;
using IDeal.Common.Components.Paginator;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.QueryHandlers
{
    public class AircraftTypesQueryHandler : IQueryHandler<AircraftTypesQuery, PagedResult<AircraftTypeDto>>
    {
        CargoContext dbContext;
        IMapper mapper;

        public AircraftTypesQueryHandler(CargoContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<PagedResult<AircraftTypeDto>> Handle(AircraftTypesQuery request, CancellationToken cancellationToken)
        {
            var task = Task.Run(() => dbContext.AircraftTypes
                .Page(request.Paging));
            return await mapper.Map<Task<PagedResult<AircraftTypeDto>>>(task);
        }
    }
}
