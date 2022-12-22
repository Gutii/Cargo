using AutoMapper;
using Cargo.Application.Services;
using Cargo.Contract.DTOs;
using Cargo.Contract.Queries.Schedule;

using IdealResults;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.QueryHandlers.Schedule
{
    public class FlightScheduleSaleStateQueryHandler : IQueryHandler<FlightScheduleSaleStateQuery, Result<FlightSheduleDto>>
    {
        ScheduleService ScheduleService;
        IMapper mapper;

        public FlightScheduleSaleStateQueryHandler(ScheduleService scheduleService, IMapper mapper)
        {
            this.ScheduleService = scheduleService;
            this.mapper = mapper;
        }

        public async Task<Result<FlightSheduleDto>> Handle(FlightScheduleSaleStateQuery request, CancellationToken cancellationToken)
        {
            var flightShedule = ScheduleService.FlightStateOpenClose(request.Id);
            var dto = mapper.Map<Task<Result<FlightSheduleDto>>>(flightShedule);
            return await dto;
        }
    }
}
