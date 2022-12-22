using AutoMapper;
using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Data.Model;
using IDeal.Common.Components.Messages.ObjectStructures.Fsas;
using System.Collections.Generic;

namespace Cargo.Application.Services
{
    public class TrackingService
    {
        CargoContext context;
        IMapper mapper;

        public TrackingService(CargoContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public void SaveStatus(Fsu fsu)
        {
            List<ConsignmentStatus> consignmentStatus = new List<ConsignmentStatus>();

            consignmentStatus.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsArr));
            consignmentStatus.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsAwd));
            consignmentStatus.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsBkd));
            consignmentStatus.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsCcd));
            consignmentStatus.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsCrc));
            consignmentStatus.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsDdl));
            consignmentStatus.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsDep));
            consignmentStatus.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsDis));
            consignmentStatus.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsDlv));
            consignmentStatus.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsFoh));
            consignmentStatus.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsMan));
            consignmentStatus.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsNfd));
            consignmentStatus.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsPre));
            consignmentStatus.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsRcf));
            consignmentStatus.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsRcs));
            consignmentStatus.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.statusDetailsRct));
            consignmentStatus.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsTfd));
            consignmentStatus.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsTgc));
            consignmentStatus.AddRange(mapper.Map<List<ConsignmentStatus>>(fsu.StatusDetailsTrm));

            /*
             * если накладная не найдена, то не привязываем к ней трекинг
             */

            context.ConsignmentStatuses.AddRange(consignmentStatus);
        }
    }
}
