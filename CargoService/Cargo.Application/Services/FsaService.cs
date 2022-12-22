using AutoMapper;
using Cargo.Infrastructure.Data;
using IDeal.Common.Components.Messages.ObjectStructures.Fsas;
using IDeal.Common.Messaging.Messages;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cargo.Application.Services
{
    public class FsaService
    {
        private readonly Regex awbPattern;
        private readonly CargoContext cargoContext;
        private readonly IMapper mapper;
        private readonly IPublishEndpoint endpoint;
        private readonly AwbService awbBookingsService;
        public FsaService(CargoContext cargoContext, IMapper mapper, IPublishEndpoint endpoint, AwbService awbBookingsService)
        {
            this.cargoContext = cargoContext;
            this.mapper = mapper;
            this.endpoint = endpoint;
            this.awbBookingsService = awbBookingsService;
            awbPattern = new Regex(@"\b(?<acPrefix>\d{3})-(?<serialNumber>\d{8})\b");
        }
        public async Task SendFsaBkd(int? awbId = null, string awbIdentifier = null, string email = null)
        {
            var verify = AwbIdentifierParse(awbIdentifier);
            var awb = awbId == null && verify.Success
                ? awbBookingsService.Awb(null, awbIdentifier).Result.Value
                : awbBookingsService.Awb(awbId).Result.Value;


            if (awb.Bookings.All(b => b.SpaceAllocationCode == "KK"))
            {
                awb = cargoContext.Awbs.AsNoTracking()
                    .Include(a => a.Bookings).ThenInclude(b => b.FlightSchedule)
                    .Single(a => a.Id == awb.Id);

                // Добавление email аэропорта из TelexSettings
                //var ts = cargoContext.TelexSettings.AsNoTracking().Where(s => s.IataLocationId == il.Id && s.Type == "FSA (BKD)").ToList();
                //ts.ForEach(s => { email = string.Concat(email, ";", s.Emails); });
                var il = cargoContext.IataLocations.AsNoTracking().FirstOrDefault(s => s.Code == awb.Origin);
                var fsa = mapper.Map<Fsa>(awb);
                await endpoint.Publish<BuildMessage>(new { __CorrelationId = Guid.NewGuid(), Fsa = fsa, LinkedObjectId = awb.Id, CustomerId = 55, IataLocationId = il?.Id, EmailRequestSource = email });
            }
        }

        private Match AwbIdentifierParse(string awbIdentifier)
        {
            return awbPattern.Match(awbIdentifier ?? string.Empty);
        }

    }

}
