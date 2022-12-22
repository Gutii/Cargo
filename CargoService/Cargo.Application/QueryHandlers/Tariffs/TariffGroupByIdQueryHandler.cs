using AutoMapper;
using Cargo.Application.QueryHandlers;
using Cargo.Contract.DTOs.Tariffs;
using Cargo.Contract.Queries.Tariffs;
using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Data.Model.Tariffs;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Directories.Application.QueryHandlers.Settings.Rates
{
    public class TariffGroupByIdQueryHandler : IQueryHandler<TariffGroupByIdQuery, TariffGroupDto>
    {
        CargoContext dbContext;
        IMapper mapper;

        public TariffGroupByIdQueryHandler(CargoContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<TariffGroupDto> Handle(TariffGroupByIdQuery request, CancellationToken cancellationToken)
        {
            Task<TariffGroup> task = Task.Run(() => dbContext.TariffGroups
                .AsNoTracking()
                .Include(tg => tg.Airports)
                .FirstOrDefault(w => w.Id == request.TariffGroupId));

            return await mapper.Map<Task<TariffGroupDto>>(task);
        }
    }
}