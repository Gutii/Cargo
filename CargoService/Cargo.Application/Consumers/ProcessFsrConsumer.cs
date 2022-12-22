using AutoMapper;
using Cargo.Application.Services;
using Cargo.Infrastructure.Data;
using IDeal.Common.Messaging.Messages;
using MassTransit;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cargo.Application.Consumers
{
    public class ProcessFsrConsumer : IConsumer<ProcessFsr>
    {
        private Regex awbPattern;
        private IMapper mapper;
        private CargoContext dbContext;
        private IPublishEndpoint endpoint;
        private AwbService awbBookingsService;
        private readonly FsaService fsaService;
        public ProcessFsrConsumer(IMapper mapper, CargoContext dbContext, IPublishEndpoint endpoint, AwbService awbBookingsService, FsaService fsaService)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.endpoint = endpoint;
            this.awbBookingsService = awbBookingsService;
            this.fsaService = fsaService;
            awbPattern = new Regex(@"\b(?<acPrefix>\d{3})-(?<serialNumber>\d{8})\b");
        }

        public async Task Consume(ConsumeContext<ProcessFsr> context)
        {
            var acPrefix = context.Message.Fsr.ConsignmentDetail.AwbIdentification.AirlinePrefix;
            var serialNumber = context.Message.Fsr.ConsignmentDetail.AwbIdentification.AwbSerialNumber;
            await fsaService.SendFsaBkd(null, $"{acPrefix}-{serialNumber}", context.Message.Email);


            //var awb = awbBookingsService.Awb(null, $"{acPrefix}-{serialNumber}").Result.Value;
            //// Добавление email аэропорта из TelexSettings
            //var il = dbContext.IataLocations.AsNoTracking().FirstOrDefault(s => s.Code == awb.Origin);
            //var ts = dbContext.TelexSettings.AsNoTracking().Where(s => s.IataLocationId == il.Id && s.Type == "FSA (BKD)").ToList();
            //ts.ForEach(s => { context.Message.Email = string.Concat(context.Message.Email, ";", s.Emails); });

            //if (awb.Bookings.All(b => b.SpaceAllocationCode == "KK"))
            //{
            //    awb = dbContext.Awbs
            //        .AsNoTracking()
            //        .Include(a => a.Bookings)
            //        .ThenInclude(b => b.FlightSchedule)
            //        .Single(a => a.Id == awb.Id);

            //    var fsa = mapper.Map<Fsa>(awb);
            //    await endpoint.Publish<BuildMessage>(new { __CorrelationId = Guid.NewGuid(), Fsa = fsa, LinkedObjectId = awb.Id, CustomerId = 55, EmailRequestSource = context.Message.Email });
            //}
        }
    }
}
