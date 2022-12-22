using Cargo.Contract.DTOs.Settings;
using Cargo.Contract.Queries.Settings;
using Cargo.Infrastructure.Data.Model.Dictionary;
using Cargo.Infrastructure.Data.Model.Settings;
using Cargo.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cargo.Contract.Queries.Quotas;
using Cargo.Contract.DTOs.Quotas;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using Cargo.Application.Parsers;

namespace Cargo.Application.Services.CommercialPayload
{
    public class SearchQuotas
    {
        CargoContext dbContext;
        public SearchQuotas(CargoContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<PkzQuotaDto> FindQuotas(FindQuotasQuery parametersFindPkz)
        {

            return Task.Run(() =>
            {
                return PkzQuotaDto(parametersFindPkz);
            });
        }

        public Task<List<PkzQuotaDto>> FindQuotasList(List<FindQuotasQuery> parametersFindPkz)
        {

            var task = Task.Run(() =>
            {
                List<PkzQuotaDto> pkzDtoList = new List<PkzQuotaDto>();
                foreach (var item in parametersFindPkz)
                {
                    pkzDtoList.Add(PkzQuotaDto(item));
                }
                return pkzDtoList;
            });

            return task;
        }

        private PkzQuotaDto PkzQuotaDto(FindQuotasQuery quotasOperativeDto)
        {

            var quotasOperative = dbContext.QuotasOperative
                .MyWhereisNullParam(c => c.CarrierId == quotasOperativeDto.CarrierId, quotasOperativeDto.CarrierId)
                .MyWhereisNullParam(c => c.Flight == quotasOperativeDto.Flight, quotasOperativeDto.Flight)
                .MyWhereisNullParam(c => c.StartDate < quotasOperativeDto.FinishDate, quotasOperativeDto.FinishDate)
                .MyWhereisNullParam(c => c.FinishDate > quotasOperativeDto.StartDate, quotasOperativeDto.StartDate)
                .MyWhereisNullParam(c => c.FlightLegFrom == quotasOperativeDto.FlightLegFrom, quotasOperativeDto.FlightLegFrom)
                .MyWhereisNullParam(c => c.FlightLegTo == quotasOperativeDto.FlightLegTo, quotasOperativeDto.FlightLegTo)
                .FirstOrDefault();

            if (quotasOperative == null)
                return null;

            bool IncludedDayOfWeek = StringParsers.IncludedDayOfWeek(quotasOperative.WeekDay, quotasOperativeDto.StartDate, quotasOperativeDto.FinishDate);

            if(!IncludedDayOfWeek)
            {
                return null;
            }

            var bookings = dbContext.Bookings.
                Include(c => c.FlightSchedule)
                .Where(b => b.SpaceAllocationCode == "KK" || b.SpaceAllocationCode == "CA")
                .MyWhereisNullParam(c => c.FlightScheduleId == quotasOperativeDto.FlightScheduleId, quotasOperativeDto.FlightScheduleId)
                .MyWhereisNullParam(c => c.FlightSchedule.Number == quotasOperativeDto.Flight, quotasOperativeDto.Flight)
                .MyWhereisNullParam(c => c.FlightSchedule.StOrigin == quotasOperativeDto.StartDate, quotasOperativeDto.StartDate)
                .MyWhereisNullParam(c => c.FlightSchedule.StDestination == quotasOperativeDto.FinishDate, quotasOperativeDto.FinishDate)
                .MyWhereisNullParam(c => c.FlightSchedule.Origin == quotasOperativeDto.FlightLegFrom, quotasOperativeDto.FlightLegFrom)
                .MyWhereisNullParam(c => c.FlightSchedule.Destination == quotasOperativeDto.FlightLegTo, quotasOperativeDto.FlightLegTo)
                .Select( c => new { c.Weight, c.VolumeAmount, c.AwbId});            

            var quotasDirectory = dbContext.QuotasDirectory
                .MyWhereisNullParam(c => c.Name == quotasOperative?.Name, quotasOperative?.Name)
                .FirstOrDefault();

            var quotasCorrect = dbContext.QuotasCorrect
                .Where(c => c.QuotasOperativeId ==quotasOperativeDto.Id)
                .FirstOrDefault();

            

            if(quotasCorrect != null)
            {
                quotasOperative.VolumeLimit = quotasCorrect.VolumeLimit;
                quotasOperative.WeightLimit = quotasCorrect.WeightLimit;
            }

            List<int> awbsId = new List<int>();
            awbsId.AddRange(bookings.Select(a => a.AwbId).ToList());

            PkzQuotaDto pkzQuotaDto = new PkzQuotaDto()
            {
                FlightSheduleId = quotasOperativeDto.FlightScheduleId ?? 0,
                IsHardAllotment = quotasOperative?.IsHardAllotment ?? false,
                AwbsId = awbsId,
                AgentId = quotasDirectory?.AgentId ?? 0,
                QuotaVolume = quotasOperative?.VolumeLimit ?? 0,
                QuotaWeight = quotasOperative?.WeightLimit ?? 0,
                BusyVolume = bookings?.Sum(c => c.VolumeAmount) ?? 0,
                BusyWeight = bookings?.Sum(c => c.Weight) ?? 0,
            };  


            return pkzQuotaDto;
        }

    }
}
