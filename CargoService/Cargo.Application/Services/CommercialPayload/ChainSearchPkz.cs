using Cargo.Contract.DTOs.Settings;
using Cargo.Contract.Queries.Settings;
using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Data.Model.Dictionary;
using Cargo.Infrastructure.Data.Model.Settings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cargo.Application.Services.CommercialPayload
{

    public class ChainSearchPkz
    {
        CargoContext dbContext;
        public ChainSearchPkz(CargoContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<PkzDto> FindPkz(FindPkzQuery parametersFindPkz)
        {

            return Task.Run(() =>
            {
                return PkzDto(parametersFindPkz);
            });
        }

        public Task<List<PkzDto>> FindPkzList(List<FindPkzQuery> parametersFindPkz)
        {

            var task = Task.Run(() =>
            {
                List<PkzDto> pkzDtoList = new List<PkzDto>();
                foreach (var item in parametersFindPkz)
                {
                    pkzDtoList.Add(PkzDto(item));
                }
                return pkzDtoList;
            });

            return task;
        }

        private PkzDto PkzDto(FindPkzQuery parametersFindPkz)
        {
            PkzDto pkzDto = new PkzDto();
            Aircraft aircraft = null;
            CommPayloadAt commPayloads = null;
            AircraftType aircraftType = null;
            MailLimits mailLimits = null;

            mailLimits = dbContext.MailLimits
                .Include(c => c.AircraftType)
                .Include(c => c.FromIataLocation)
                .Include(c => c.InIataLocation)
                .Where(c => c.IataCode == parametersFindPkz.IataCode)
                .ToList()
                .MyWhereisNullParam(c => c.AircraftType.IataCode == parametersFindPkz.AircraftType,
                parametersFindPkz.AircraftType)
                .MyWhereisNullParam(c => c.InIataLocation.Code == parametersFindPkz.Origin,
                parametersFindPkz.Origin)
                .MyWhereisNullParam(c => c.FromIataLocation.Code == parametersFindPkz.Destination,
                parametersFindPkz.Destination)
                .MyWhereisNullParam(c => c.DateStart == parametersFindPkz.DateStart,
                parametersFindPkz.DateStart)
                .MyWhereisNullParam(c => c.DateEnd == parametersFindPkz.DateEnd,
                parametersFindPkz.DateEnd)
                .MyWhereisNullParam(c => c.FlightNumber == parametersFindPkz.FlightNumber,
                parametersFindPkz.FlightNumber)
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(parametersFindPkz.OnboardNumber))
            {
                aircraft = dbContext.Aircraft
                .Include(c => c.AircraftType)
                .Where(c => c.OnboardNumber == parametersFindPkz.OnboardNumber
                && c.IataCode == parametersFindPkz.IataCode)
                .ToList().MyWhereisNullParam(c => c.AircraftType.IataCode == parametersFindPkz.AircraftType, parametersFindPkz.AircraftType)
                .ToList()
                .WithoutShr(c => c.AccseptedShr, parametersFindPkz.AccseptedShr)
                .FirstOrDefault();
            }

            if (aircraft == null)
            {
                commPayloads = dbContext.CommPayloads
                .Include(c => c.AircraftType)
                .Include(c => c.FromIataLocation)
                .Include(c => c.InIataLocation)
                .Where(c => c.IataCode == parametersFindPkz.IataCode)
                .ToList()
                .MyWhereisNullParam(c => c.AircraftType.IataCode == parametersFindPkz.AircraftType,
                parametersFindPkz.AircraftType)
                .MyWhereisNullParam(c => c.InIataLocation.Code == parametersFindPkz.Origin,
                parametersFindPkz.Origin)
                .MyWhereisNullParam(c => c.FromIataLocation.Code == parametersFindPkz.Destination,
                parametersFindPkz.Destination)
                .MyWhereisNullParam(c => c.DateStart == parametersFindPkz.DateStart,
                parametersFindPkz.DateStart)
                .MyWhereisNullParam(c => c.DateEnd == parametersFindPkz.DateEnd,
                parametersFindPkz.DateEnd)
                .MyWhereisNullParam(c => c.FlightNumber == parametersFindPkz.FlightNumber,
                parametersFindPkz.FlightNumber)
                .ToList()
                .WithoutShr(c => c.AccseptedShr, parametersFindPkz.AccseptedShr)
                .FirstOrDefault();
            }

            if (commPayloads == null)
            {
                aircraftType = dbContext.AircraftTypes.FirstOrDefault(c => c.IataCode == parametersFindPkz.AircraftType);
            }

            if (aircraftType == null)
            {
                return pkzDto;
            }

            pkzDto = SetPkzDtoAircraft(pkzDto, aircraft);
            pkzDto = SetPkzDtoCommPayload(pkzDto, commPayloads);
            pkzDto = SetPkzDtoAircraftType(pkzDto, aircraftType);
            pkzDto = SetPkzDtoMailLimits(pkzDto, mailLimits);
            return pkzDto;
        }

        private PkzDto SetPkzDtoMailLimits(PkzDto pkzDto, MailLimits mailLimits)
        {
            if (pkzDto == null || mailLimits == null)
                return pkzDto;

            pkzDto.AircraftType = mailLimits.AircraftType.IataCode;
            pkzDto.MailVolume = mailLimits.MaxPayloadVolume;
            pkzDto.MailWeight = mailLimits.MaxPayloadWeight;
            return pkzDto;
        }

        private PkzDto SetPkzDtoAircraftType(PkzDto pkzDto, AircraftType aircraftType)
        {
            if (aircraftType == null)
                return pkzDto;

            pkzDto.AircraftType = aircraftType.IataCode;
            pkzDto.CommPayloadVolume = aircraftType.MaxPayloadVolume;
            pkzDto.CommPayloadWeight = aircraftType.MaxPayloadWeight;
            return pkzDto;
        }

        private PkzDto SetPkzDtoCommPayload(PkzDto pkzDto, CommPayloadAt commPayloadAt)
        {
            if (commPayloadAt == null)
                return pkzDto;

            pkzDto.AircraftType = commPayloadAt.AircraftType.IataCode;
            pkzDto.CommPayloadVolume = commPayloadAt.MaxPayloadVolume;
            pkzDto.CommPayloadWeight = commPayloadAt.MaxPayloadWeight;
            return pkzDto;
        }

        private PkzDto SetPkzDtoAircraft(PkzDto pkzDto, Aircraft aircraft)
        {
            if (aircraft == null)
                return pkzDto;

            pkzDto.AircraftType = aircraft.AircraftType.IataCode;
            pkzDto.CommPayloadVolume = aircraft.MaxGrossPayload;
            pkzDto.CommPayloadWeight = aircraft.MaxTakeOffWeight;
            return pkzDto;
        }
    }
}
