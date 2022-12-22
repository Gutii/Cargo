using AutoMapper;
using Cargo.Contract.DTOs;
using Cargo.Contract.DTOs.Bookings;
using Cargo.Contract.Queries.Report;
using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Data.Model;
using Cargo.Infrastructure.Report;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.QueryHandlers.Report
{
    public class ReportFwbBlankByAwbIdQueryHandler : IQueryHandler<ReportFwbBlankByAwbIdQuery, byte[]>
    {
        CargoContext CargoContext;
        IMapper mapper;
        IPublishEndpoint endpoint;
        AwbDtoExtension awbDtoExtension;

        public ReportFwbBlankByAwbIdQueryHandler(CargoContext CargoContext, IMapper mapper, IPublishEndpoint endpoint)
        {
            this.CargoContext = CargoContext;
            this.mapper = mapper;
            this.endpoint = endpoint;
            awbDtoExtension = new AwbDtoExtension();
        }

        public async Task<byte[]> Handle(ReportFwbBlankByAwbIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Awb awb = this.CargoContext.Awbs
                    .AsNoTracking()
                    .Include(a => a.Bookings)
                    .ThenInclude(b => b.FlightSchedule)
                    .Include(a => a.BookingRcs)
                    .ThenInclude(b => b.FlightSchedule)
                    .Include(a => a.Agent)
                    .ThenInclude(c => c.SalesAgent)
                    .Include(a => a.Consignee)
                    .Include(a => a.Consignor)
                    .Include(a => a.SizeGroups)
                    .Include(a => a.Prepaid)
                    .Include(a => a.Carrier)
                    .Include(a => a.Collect)
                    .Include(a => a.Messages)
                    .Include(a => a.OtherCharges)
                    .FirstOrDefault(il => il.Id == (int)request.awbId)
                ;

                var OriginInfo = this.CargoContext.IataLocations
                    .Where(a => a.Code == awb.Origin).FirstOrDefault();
                var DestinationInfo = this.CargoContext.IataLocations
                    .Where(a => a.Code == awb.Destination).FirstOrDefault();

                var ConsigneeCountryISOInfo = CargoContext.Countries
                    .Where(c => c.Alpha2 == awb.Consignee.CountryISO).FirstOrDefault();
                var ConsignorCountryISOInfo = CargoContext.Countries
                    .Where(c => c.Alpha2 == awb.Consignor.CountryISO).FirstOrDefault();



                var OriginInfoDto = this.mapper.Map<IataLocationDto>(OriginInfo);
                var DestinationInfoDto = this.mapper.Map<IataLocationDto>(DestinationInfo);

                var ConsigneeCountryISOInfoDto = this.mapper.Map<CountryDto>(ConsigneeCountryISOInfo);
                var ConsignorCountryISOInfoDto = this.mapper.Map<CountryDto>(ConsignorCountryISOInfo);

                AwbDto awbExtDto = this.mapper.Map<AwbDto>(awb);

                awbExtDto.OriginInfo = OriginInfoDto;
                awbExtDto.DestinationInfo = DestinationInfoDto;

                awbExtDto.Consignee.CountryISOInfo = ConsigneeCountryISOInfoDto;
                awbExtDto.Consignor.CountryISOInfo = ConsignorCountryISOInfoDto;

                byte[] content = awbDtoExtension.ToPdf(awbExtDto);

                return await Task.FromResult(content);
            }
            catch (Exception e)
            {
                Console.Write("ReportFwbBlankByAwbIdQueryHandler: " + e.Message);
                return await Task.FromResult(new byte[0]);
            }
        }
    }
}
