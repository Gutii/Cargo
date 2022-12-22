using AutoMapper;
using Cargo.Application.Services.CommercialPayload;
using Cargo.Contract.DTOs;
using Cargo.Contract.DTOs.Quotas;
using Cargo.Contract.Queries.Quotas;
using Cargo.Contract.Queries.Schedule;
using Cargo.Contract.Queries.Settings;
using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Data.Model;
using IDeal.Common.Components;
using IDeal.Common.Components.Paginator;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.QueryHandlers.Schedule
{
    public class FlightSchedulesQueryHandler : IQueryHandler<FlightSchedulesQuery, PagedResult<FlightSheduleDto>>
    {
        CargoContext CargoContext;
        IMapper mapper;
        ChainSearchPkz сhainSearchPkz;
        SearchQuotas searchQuotas;

        public FlightSchedulesQueryHandler(CargoContext CargoContext, IMapper mapper, ChainSearchPkz сhainSearchPkz, SearchQuotas searchQuotas)
        {
            this.CargoContext = CargoContext;
            this.mapper = mapper;
            this.сhainSearchPkz = сhainSearchPkz;
            this.searchQuotas = searchQuotas;
        }

        public async Task<PagedResult<FlightSheduleDto>> Handle(FlightSchedulesQuery request, CancellationToken cancellationToken)
        {
            //CommPayloadFlatDetail[] pkzs = (await commPayloaderService.Payloads()).Value.ToArray();

            var page = CargoContext
                .FlightShedules
                .Where(f => string.IsNullOrEmpty(request.CarrierCode) || f.Number.Substring(0, 2) == request.CarrierCode)
                .Where(f => string.IsNullOrEmpty(request.Destination) || f.Destination == request.Destination)
                .Where(f => string.IsNullOrEmpty(request.Origin) || f.Origin == request.Origin)
                .Where(f => string.IsNullOrEmpty(request.FlightNumber) || f.Number.Contains(request.FlightNumber))
                .Where(f => !request.FlightDate.HasValue || request.FlightDate == DateTime.MinValue || f.FlightDate == request.FlightDate)
                .Where(f => !request.DateFrom.HasValue || request.DateFrom == DateTime.MinValue || f.FlightDate >= request.DateFrom.Value.Date)
                .Where(f => !request.DateTo.HasValue || request.DateTo == DateTime.MaxValue || f.FlightDate <= request.DateTo.Value.Date + new TimeSpan(23, 59, 59))
                .OrderBy(f => f.StOrigin)
                .AsNoTracking()
                .Page(new PageInfo { PageIndex = request.PageIndex ?? 0, PageSize = request.PageSize ?? 20 });

            ulong[] fids = page.Items.Select(f => f.Id).ToArray();
            var bookings = CargoContext.Bookings.Where(b => fids.Contains(b.FlightScheduleId)).ToList();

            var pageDto = mapper.Map<PagedResult<FlightSheduleDto>>(page);
            var parametersFindPkz = mapper.Map<PagedResult<FindPkzQuery>>(pageDto);
            var parametersFindQuotas = mapper.Map<PagedResult<FindQuotasQuery>>(pageDto);
            var pkzs = await сhainSearchPkz.FindPkzList(parametersFindPkz.Items.ToList());

            var quotas = await searchQuotas.FindQuotasList(parametersFindQuotas.Items.ToList());
            
            bookings = DeleteBookingContainsQuotas(bookings, quotas);

            pageDto.Items
            // .Join(CargoContext.IataLocations, fs=>fs.Origin, il=>il.Code, (fs, il) =>
            // {
            //     fs.StOriginLocal = fs.StOrigin?.Add(il.TimeZone);
            //     return fs;
            // })
            // .Join(CargoContext.IataLocations, fs => fs.Destination, il => il.Code, (fs, il) =>
            // {
            //     fs.StDestinationLocal = fs.StDestination?.Add(il.TimeZone);
            //     return fs;
            // })
            .Select(fs =>
            {
                fs.StdOriginLocalDayChange = 0;
                fs.StdOriginUtcDayChange = (fs.StOrigin?.Date - fs.FlightDate?.Date)?.Days;
                fs.StaDestLocalDayChange = (fs.StDestinationLocal - fs.FlightDate?.Date)?.Days;
                fs.StaDestUtcDayChange = (fs.StDestination?.Date - fs.FlightDate?.Date)?.Days;
                return fs;
            })
            .GroupJoin(pkzs, fs => fs.AircraftType, cpl => cpl.AircraftType, (fs, cpls) =>
            {
                fs.CommPayloadInfo = new FlightCommPayloadInfoDto
                {
                    FlightId = fs.Id,
                    DateFlight = fs.FlightDate,
                    FlightShedule = fs,
                    NumberFlight = fs.Number,
                    WeightPlan = (decimal?)fs.PayloadWeight == 0 ? cpls.FirstOrDefault()?.CommPayloadWeight ?? 0 : (decimal)fs.PayloadWeight,
                    VolumePlan = (decimal?)fs.PayloadVolume == 0 ? cpls.FirstOrDefault()?.CommPayloadVolume ?? 0 : (decimal)fs.PayloadVolume,
                    MailVolume = cpls.FirstOrDefault()?.MailVolume ?? 0,
                    MailWeight = cpls.FirstOrDefault()?.MailWeight ?? 0
                }; return fs;
            })
            .GroupJoin(bookings, f => f.Id, b => b?.FlightScheduleId, (fs, bs) =>
            {
                
                fs.CommPayloadInfo.VolumeFact = bs.Where(b => b.SpaceAllocationCode == "KK" || b.SpaceAllocationCode == "CA")
                .Sum(b => b.VolumeAmount);
                fs.CommPayloadInfo.WeightFact = bs.Where(b => b.SpaceAllocationCode == "KK" || b.SpaceAllocationCode == "CA")
                .Sum(b => b.Weight);

                var volumeRemain = fs.CommPayloadInfo.VolumePlan - (fs.CommPayloadInfo.VolumeFact + fs.CommPayloadInfo.MailVolume);
                var weightRemain = fs.CommPayloadInfo.WeightPlan - (fs.CommPayloadInfo.WeightFact + fs.CommPayloadInfo.MailWeight);

                if (request.SelectedRoleNameEn == Role.Carrier.Value || volumeRemain >= 0)
                {
                    fs.CommPayloadInfo.VolumeRemain = volumeRemain;
                }
                else
                {
                    fs.CommPayloadInfo.VolumeRemain = 0;
                }

                if (request.SelectedRoleNameEn == Role.Carrier.Value || weightRemain >= 0)
                {
                    fs.CommPayloadInfo.WeightRemain = weightRemain;
                }
                else
                {
                    fs.CommPayloadInfo.WeightRemain = 0;
                }

                return fs;
            })
            .GroupJoin(quotas, fs => fs.Id, cpl => cpl?.FlightSheduleId, (fs, cpls) =>
            {
                var quota = cpls.FirstOrDefault();
                if (quota?.QuotaWeight != null)
                {
                    var weight = quota.QuotaWeight;
                    if (!quota.IsHardAllotment && quota.BusyWeight > quota.QuotaWeight)
                    {
                        weight = quota.BusyWeight;
                    }
                    fs.CommPayloadInfo.WeightFact += weight;
                    var WeightRemain = fs.CommPayloadInfo.WeightRemain - weight;
                    fs.CommPayloadInfo.WeightRemain =
                    WeightRemain >= 0 || request.SelectedRoleNameEn == Role.Carrier.Value ?
                    WeightRemain : 0;
                }
                if (quota?.QuotaVolume != null)
                {
                    var volume = quota.QuotaVolume;
                    if (!quota.IsHardAllotment && quota.BusyVolume > quota.QuotaVolume)
                    {
                        volume = quota.BusyVolume;
                    }
                    fs.CommPayloadInfo.VolumeFact += volume;
                    var VolumeRemain = fs.CommPayloadInfo.VolumeRemain - volume;
                    fs.CommPayloadInfo.VolumeRemain =
                    VolumeRemain >= 0 || request.SelectedRoleNameEn == Role.Carrier.Value ?
                    VolumeRemain : 0;
                }
                return fs;
            })
            .ToList();

            return pageDto;
        }

        private List<Booking> DeleteBookingContainsQuotas(List<Booking> bookings, List<PkzQuotaDto> quotas)
        {
            if (quotas == null || bookings == null)
                return null;

            List<int> withoutAwbId = new List<int>();

            foreach (var quota in quotas)
            {
                if(quota?.AwbsId != null)
                {
                    withoutAwbId = withoutAwbId.Concat(quota.AwbsId).ToList();
                }                
            }

            var withoutBookings = bookings.Where(c => withoutAwbId.Contains(c.AwbId)).ToList();

            return bookings.Except(withoutBookings).ToList();
        }
    }
}
