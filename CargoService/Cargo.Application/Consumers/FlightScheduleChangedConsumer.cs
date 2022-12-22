using Cargo.Application.Services;
using IDeal.Common.Messaging.Shedule;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Cargo.Application.CommandHandlers
{
    public class FlightScheduleChangedConsumer : IConsumer<FlightSheduleChanged>
    {

        private readonly ScheduleService scheduleService;

        public FlightScheduleChangedConsumer(
            ScheduleService scheduleService)
        {
            this.scheduleService = scheduleService;
        }

        public async Task Consume(ConsumeContext<FlightSheduleChanged> context)
        {
            if (context.Message?.Id == null)
                throw new ArgumentNullException("Пропущен параметр для определения рейса");

            await scheduleService.FlightChanged(context.Message);
        }
    }
}
