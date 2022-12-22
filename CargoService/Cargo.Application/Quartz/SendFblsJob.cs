using AutoMapper;
using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Data.Model;
using IDeal.Common.Components.Messages.ObjectStructures.Fbls.Ver4;
using IDeal.Common.Messaging.Messages;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cargo.Application.Quartz
{
    [DisallowConcurrentExecution]
    public class SendFblsJob : IJob
    {
        CargoContext CargoContext;
        IMapper mapper;
        IPublishEndpoint endpoint;

        public SendFblsJob(CargoContext CargoContext, IMapper mapper, IPublishEndpoint endpoint)
        {
            this.CargoContext = CargoContext;
            this.mapper = mapper;
            this.endpoint = endpoint;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            DateTime now = DateTime.Now;
            DateTime begDateTime = now.Subtract(new TimeSpan(12, 0, 0)); // Сейчас минус 12 часов
            DateTime endDateTime = now.Subtract(new TimeSpan(-6, 0, 0)); // Сейчас минус 6 часов !!! для теста -6

            // Получаем список всех броней, на которые есть рейсы, попавшие в интервал
            List<Booking> bookings = CargoContext.Bookings.AsNoTracking()
                .Include(a => a.Awb)
                .Include(i => i.FlightSchedule)
                .Where(b => b.FlightSchedule.StOrigin >= begDateTime)
                .Where(b => b.FlightSchedule.StOrigin <= endDateTime)
                .Where(b => b.SpaceAllocationCode == "KK")
                .ToList();

            bookings
                .GroupBy(gb => gb.FlightScheduleId)
                .ForEach(async gb =>
                {
                    FlightShedule flight = gb.First().FlightSchedule;
                    flight.Bookings = gb.ToList();

                    // Добавление email аэропорта из TelexSettings
                    var email = string.Empty;
                    var il = CargoContext.IataLocations.AsNoTracking().FirstOrDefault(s => s.Code == flight.Origin);
                    var ts = CargoContext.TelexSettings.AsNoTracking().Where(s => s.IataLocationId == il.Id && s.Type == "FBL").ToList();
                    ts.ForEach(s => { email = string.Concat(email, ";", s.Emails); });

                    Fbl fbl = mapper.Map<Fbl>(flight);
                    await endpoint.Publish<BuildMessage>(new { __CorrelationId = Guid.NewGuid(), Carrier = fbl.FlightInformation.CarrierCode, Fbl = fbl, LinkedObjectId = flight.Id, CustomerId = 55, EmailRequestSource = email });
                });
        }
    }
}
