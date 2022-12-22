using AutoMapper;
using Cargo.Contract.DTOs.Bookings;
using Cargo.Contract.Queries.Bookings;
using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Data.Model;
using IDeal.Common.Components.Paginator;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.QueryHandlers.Bookings
{
    public class AwbsByFlightOrAgentIdQueryHandler : IQueryHandler<AwbsByFlightOrAgentIdQuery, PagedResult<AwbLightDto>>
    {
        CargoContext CargoContext;

        readonly MapperConfiguration mapperConfiguration;

        IMapper mapper;

        public AwbsByFlightOrAgentIdQueryHandler(CargoContext CargoContext, IMapper mapper)
        {
            this.CargoContext = CargoContext;
            this.mapper = mapper;
        }

        public async Task<PagedResult<AwbLightDto>> Handle(AwbsByFlightOrAgentIdQuery request, CancellationToken cancellationToken)
        {
            Task<PagedResult<Awb>> task = Task.Run(() =>
                {
                    var awbs = this.CargoContext.Awbs
                    .Include(r => r.Bookings)
                    .AsNoTracking()
                    .Where(r => request.FlightId != null ? r.Bookings.Any(b => b.FlightScheduleId == (ulong)request.FlightId) : true)
                    .Where(r => request.AgentId != null ? r.AgentId == request.AgentId : true)
                    .ToList();

                    ulong[] fss = awbs.SelectMany(a => a.Bookings).Select(b => b.FlightScheduleId).Distinct().ToArray();

                    IQueryable<FlightShedule> flights = this.CargoContext.FlightShedules.AsNoTracking().Where(f => fss.Contains(f.Id));
                    awbs.SelectMany(a => a.Bookings).Join(flights, b => b.FlightScheduleId, f => f.Id, (b, f) => { b.FlightSchedule = f; return b; }).ToList();

                    return awbs.Page(new PageInfo { PageIndex = 0, PageSize = 20 });
                }, cancellationToken);
            return await this.mapper.Map<Task<PagedResult<AwbLightDto>>>(task);
        }
    }
}
