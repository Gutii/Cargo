using AutoMapper;
using Cargo.Infrastructure.Data;
using IDeal.Common.Components.Messages.ObjectStructures.Fbls.Ver4;
using IDeal.Common.Messaging.Messages;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Application.Services;

public class FblService
{
    private readonly CargoContext cargoContext;
    private readonly IMapper mapper;
    private readonly IPublishEndpoint endpoint;
    private readonly TelegrammService _telegrammService;
    public FblService(CargoContext cargoContext, IMapper mapper, IPublishEndpoint endpoint, TelegrammService telegrammService)
    {
        this.cargoContext = cargoContext;
        this.mapper = mapper;
        this.endpoint = endpoint;
        _telegrammService = telegrammService;
    }

    public async Task SendFbl(ulong flightId)
    {
        try
        {
            var flight = await cargoContext.FlightShedules.AsNoTracking().FirstOrDefaultAsync(x => x.ExternalId == flightId);
            if (flight == null)
            {
                var message = new StringBuilder("Отправка FBL пропущена по причине:")
                    .AppendLine($"Рейс с ExternalId={flightId} не найден")
                    .ToString();
                await _telegrammService.Send(message);
                return;
            }

            // Получаем список всех броней для рейса
            flight.Bookings = await cargoContext.Bookings.AsNoTracking()
                .Include(a => a.Awb)
                .Where(i => i.FlightScheduleId == flightId)
                .Where(b => b.SpaceAllocationCode == "KK")
                .ToListAsync();

            if (flight.Bookings is { Count: 0 })
            {
                //Если до вылета более 12 часов - FBL не отправляем
                var delta = flight.FlightDate - DateTime.Now;
                if (delta > new TimeSpan(6, 0, 0))
                {
                    // var message = new StringBuilder("Отправка FBL пропущена по причине:")
                    //     .AppendLine($"До вылета рейса {flight.Number} более 6 часов и отсутствуют брони")
                    //     .AppendLine($"Дата вылета: {flight.StOrigin:dd.MM.yy HH:mm}")
                    //     .AppendLine($"До вылета {delta:g}")
                    //     .ToString();
                    // await _telegrammService.Send(message);
                    return;
                }
            }

            var fbl = mapper.Map<Fbl>(flight);
            if (!flight.Bookings.Any())
                fbl.PortUnloading = new PortUnloading
                {
                    AirportCode = flight.Destination,
                    NilCargoIndication = new NilCargoIndication { NilCargoCode = "NIL" }
                };

            // Добавление email аэропорта из TelexSettings
            //var email = string.Empty;
            //var il = cargoContext.IataLocations.AsNoTracking().FirstOrDefault(s => s.Code == flight.Origin);
            //if (il != null)
            //{
            //    var ts = cargoContext.TelexSettings.AsNoTracking().Where(s => s.IataLocationId == il.Id && s.Type == "FBL").ToList();
            //    ts.ForEach(s => { email = string.Concat(email, ";", s.Emails); });
            //}

            var il = cargoContext.IataLocations.AsNoTracking().FirstOrDefault(s => s.Code == flight.Origin);
            await endpoint.Publish<BuildMessage>(new
            {
                __CorrelationId = Guid.NewGuid(),
                Carrier = fbl.FlightInformation.CarrierCode,
                Fbl = fbl,
                LinkedObjectId = flight.Id,
                CustomerId = 55,
                IataLocationId = il?.Id
            });

            //var d = flight.StOrigin - DateTime.UtcNow;
            //await _telegrammService.Send($"FBL для рейса {flight.Number}\nДата вылета {flight.StOrigin:dd.MM.yy HH:mm}\nДо вылета {d:g}\nОтправлен");
        }
        catch (Exception e)
        {
            await _telegrammService.SendError(e);
        }
    }
}