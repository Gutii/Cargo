using AutoMapper;
using Cargo.Application.Services;
using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Data.Model;
using IDeal.Common.Messaging.Messages;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cargo.Application.CommandHandlers
{
    public class ProcessFsuConsumer : IConsumer<ProcessFsu>
    {
        private IMapper mapper;
        private CargoContext dbContext;
        private ILogger<ProcessFsuConsumer> logger;
        private AwbService awbService;

        public ProcessFsuConsumer(ILogger<ProcessFsuConsumer> logger, IMapper mapper, CargoContext dbContext, AwbService awbService)
        {
            this.logger = logger;
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.awbService = awbService;
        }

        public async Task Consume(ConsumeContext<ProcessFsu> context)
        {
            var fsu = context.Message.Fsu;
            var acPrefic = fsu.ConsignmentDetail.AwbIdentification.AirlinePrefix;
            var serialNumber = fsu.ConsignmentDetail.AwbIdentification.AwbSerialNumber;

            if (!dbContext.Carriers.Any(p => p.AcPrefix == acPrefic || p.AcMailPrefix == acPrefic))
            {
                logger.LogError($"Префикс {acPrefic} не имеет отношение к перевозчику-пользователю системы");
                return;
            } // + Должна быть проверка на принадлежность рейса в управлени перевозчика.

            var originAwb = dbContext.Awbs
                .Include(a => a.Tracking)
                .FirstOrDefault(a => a.AcPrefix == acPrefic && a.SerialNumber == serialNumber && EF.Functions.DateDiffMonth(a.CreatedDate, DateTime.UtcNow) < 6);
            if (originAwb == null)
            {
                try
                {
                    Awb awb = mapper.Map<Awb>(fsu);
                    var carrier = await dbContext.Carriers.FirstOrDefaultAsync(x => x.AcPrefix == awb.AcPrefix || x.AcMailPrefix == awb.AcPrefix);//Определяем перевозчика по префиксу
                    awb.CarrierId = carrier?.Id ?? 55;

                    awb.ManifestDescriptionOfGoods = " ";
                    awb.ManifestDescriptionOfGoodsRu = " ";
                    awb.PoolAwbId = 0;
                    awb.QuanDetShipmentDescriptionCode = "T";
                    awb.WeightCode = "K";
                    awb.VolumeCode = "MC";
                    awb.Status = "Draft";

                    dbContext.Awbs.Add(awb);
                    await dbContext.SaveChangesAsync();
                    //await awbService.SaveAwb(awb, StatusAwb.Draft.Value);
                    originAwb = originAwb = dbContext.Awbs.Include(a => a.Tracking).FirstOrDefault(a => a.AcPrefix == acPrefic && a.SerialNumber == serialNumber);
                }
                catch
                {
                    return;
                }
            }

            var statuses = new List<ConsignmentStatus>();
            if (fsu.StatusDetailsBkd != null) statuses.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsBkd));
            if (fsu.StatusDetailsFoh != null) statuses.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsFoh));
            if (fsu.StatusDetailsRcs != null) statuses.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsRcs));
            if (fsu.StatusDetailsMan != null) statuses.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsMan));
            if (fsu.StatusDetailsDep != null) statuses.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsDep));
            if (fsu.StatusDetailsArr != null) statuses.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsArr));
            if (fsu.StatusDetailsRcf != null) statuses.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsRcf));
            if (fsu.StatusDetailsAwr != null) statuses.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsAwr));
            if (fsu.StatusDetailsNfd != null) statuses.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsNfd));
            if (fsu.StatusDetailsAwd != null) statuses.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsAwd));
            if (fsu.StatusDetailsDlv != null) statuses.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsDlv));

            foreach (var status in statuses) originAwb.Tracking.Add(status);
            await dbContext.SaveChangesAsync();
        }
    }
}
