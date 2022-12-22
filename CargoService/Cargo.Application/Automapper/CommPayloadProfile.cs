using AutoMapper;
using Cargo.Contract.DTOs;
using Cargo.Contract.DTOs.Quotas;
using Cargo.Contract.DTOs.Settings.CommercialPayload;
using Cargo.Contract.DTOs.Settings;
using Cargo.Contract.Queries.Settings;
using Cargo.Infrastructure.Data.Model.Dictionary;
using Cargo.Infrastructure.Data.Model.Quotas;
using Cargo.Infrastructure.Data.Model.Settings;
using Cargo.Contract.Queries.Quotas;

namespace Cargo.Application.Automapper
{
    public class CommPayloadProfile : Profile
    {
        public CommPayloadProfile()
        {
            PkzMaps();
            Quotas();
        }

        /// <summary>
        /// Квоты
        /// </summary>
        void Quotas()
        {
            CreateMap<QuotasOperative, QuotasOperativeDto>().ReverseMap();
            CreateMap<QuotasDirectory, QuotasDirectoryDto>().ReverseMap();
            CreateMap<QuotasCorrect, QuotasCorrectDto>().ReverseMap();

            CreateMap<FindQuotasQuery, FlightSheduleDto>().ReverseMap();

            CreateMap<FindQuotasQuery, FlightSheduleDto>()
               .ForMember(a => a.Id, cmd => cmd.MapFrom(s => s.FlightScheduleId))
               .ForMember(a => a.AircraftType, cmd => cmd.Ignore())
               .ForMember(a => a.Number, cmd => cmd.MapFrom(s => s.Flight))
               .ForMember(a => a.StOrigin, cmd => cmd.MapFrom(s => s.StartDate))
               .ForMember(a => a.StDestination, cmd => cmd.MapFrom(s => s.FinishDate))
               .ForMember(a => a.SHR, cmd => cmd.MapFrom(s => s.Shc))
               .ForMember(a => a.Origin, cmd => cmd.MapFrom(s => s.FlightLegFrom))
               .ForMember(a => a.Destination, cmd => cmd.MapFrom(s => s.FlightLegTo))
               .ForMember(a => a.OriginInfo, cmd => cmd.Ignore())
               .ForMember(a => a.DestinationInfo, cmd => cmd.Ignore())
               .ForMember(a => a.FlightDate, cmd => cmd.Ignore())
               .ForMember(a => a.StOriginLocal, cmd => cmd.Ignore())
               .ForMember(a => a.StDestinationLocal, cmd => cmd.Ignore())
               .ForMember(a => a.StdOriginLocalDayChange, cmd => cmd.Ignore())
               .ForMember(a => a.StdOriginUtcDayChange, cmd => cmd.Ignore())
               .ForMember(a => a.StaDestLocalDayChange, cmd => cmd.Ignore())
               .ForMember(a => a.StaDestUtcDayChange, cmd => cmd.Ignore())
               .ForMember(a => a.State, cmd => cmd.Ignore())
               .ForMember(a => a.AircraftRegistration, cmd => cmd.Ignore())
               .ForMember(a => a.ProcentId, cmd => cmd.Ignore())
               .ForMember(a => a.Messages, cmd => cmd.Ignore())
               .ForMember(a => a.CommPayloadInfo, cmd => cmd.Ignore())
               .ForMember(a => a.PayloadWeight, cmd => cmd.Ignore())
               .ForMember(a => a.PayloadVolume, cmd => cmd.Ignore())
               .ForMember(a => a.Id, cmd => cmd.Ignore())
               .ReverseMap()
               .ForMember(a => a.FlightScheduleId, cmd => cmd.MapFrom(s => s.Id))
               .ForMember(a => a.Shc, cmd => cmd.MapFrom(s => s.SHR))
               .ForMember(a => a.Flight, cmd => cmd.MapFrom(s => s.Number))
               .ForMember(a => a.StartDate, cmd => cmd.MapFrom(s => s.StOrigin))
               .ForMember(a => a.FinishDate, cmd => cmd.MapFrom(s => s.StDestination))
               .ForMember(a => a.FlightLegFrom, cmd => cmd.MapFrom(s => s.Origin))
               .ForMember(a => a.FlightLegTo, cmd => cmd.MapFrom(s => s.Destination))
               .ForMember(a => a.Name, cmd => cmd.Ignore())
               .ForMember(a => a.VolumeLimit, cmd => cmd.Ignore())
               .ForMember(a => a.WeightLimit, cmd => cmd.Ignore())
               .ForMember(a => a.Currency, cmd => cmd.Ignore())
               .ForMember(a => a.Id, cmd => cmd.Ignore())
               .ForMember(a => a.SalesProduct, cmd => cmd.Ignore())
               .ForMember(a => a.WeekDay, cmd => cmd.Ignore())
               .ForMember(a => a.AwbDest, cmd => cmd.Ignore())
               .ForMember(a => a.AwbOrigin, cmd => cmd.Ignore())
               .ForMember(a => a.AwbPrefix, cmd => cmd.Ignore())
               .ForMember(a => a.CarrierId, cmd => cmd.Ignore())
               ;

        }

        /// <summary>
        /// Почта и груз
        /// </summary>
        void PkzMaps()
        {
            CreateMap<AircraftType, AircraftTypeDto>();

            CreateMap<FindPkzQuery, FlightSheduleDto>()
               .ForMember(a => a.AircraftType, cmd => cmd.MapFrom(s => s.AircraftType))
               .ForMember(a => a.Number, cmd => cmd.MapFrom(s => s.FlightNumber))
               .ForMember(a => a.StOrigin, cmd => cmd.MapFrom(s => s.DateStart))
               .ForMember(a => a.StDestination, cmd => cmd.MapFrom(s => s.DateEnd))
               .ForMember(a => a.SHR, cmd => cmd.MapFrom(s => s.AccseptedShr))
               .ForMember(a => a.OriginInfo, cmd => cmd.Ignore())
               .ForMember(a => a.DestinationInfo, cmd => cmd.Ignore())
               .ForMember(a => a.FlightDate, cmd => cmd.Ignore())
               .ForMember(a => a.StOriginLocal, cmd => cmd.Ignore())
               .ForMember(a => a.StDestinationLocal, cmd => cmd.Ignore())
               .ForMember(a => a.StdOriginLocalDayChange, cmd => cmd.Ignore())
               .ForMember(a => a.StdOriginUtcDayChange, cmd => cmd.Ignore())
               .ForMember(a => a.StaDestLocalDayChange, cmd => cmd.Ignore())
               .ForMember(a => a.StaDestUtcDayChange, cmd => cmd.Ignore())
               .ForMember(a => a.State, cmd => cmd.Ignore())
               .ForMember(a => a.AircraftRegistration, cmd => cmd.Ignore())
               .ForMember(a => a.ProcentId, cmd => cmd.Ignore())
               .ForMember(a => a.Messages, cmd => cmd.Ignore())
               .ForMember(a => a.CommPayloadInfo, cmd => cmd.Ignore())
               .ForMember(a => a.PayloadWeight, cmd => cmd.Ignore())
               .ForMember(a => a.PayloadVolume, cmd => cmd.Ignore())
               .ForMember(a => a.Id, cmd => cmd.Ignore())
               .ReverseMap()
               .ForMember(a => a.DateStart, cmd => cmd.MapFrom(s => s.StOrigin))
               .ForMember(a => a.DateEnd, cmd => cmd.MapFrom(s => s.StDestination))
               .ForMember(a => a.FlightNumber, cmd => cmd.MapFrom(s => s.Number))
               .ForMember(a => a.AccseptedShr, cmd => cmd.MapFrom(s => s.SHR))
               .ForMember(a => a.IataCode, cmd => cmd.Ignore())
               .ForMember(a => a.OnboardNumber, cmd => cmd.Ignore())
               ;

            CreateMap<MailLimits, MailLimitsDto>()
               .ForMember(a => a.AircraftType, cmd => cmd.MapFrom(s => s.AircraftType.IataCode))
               .ForMember(a => a.Airline, cmd => cmd.MapFrom(s => s.Airline.IataCode))
               .ForMember(a => a.FromIataLocation, cmd => cmd.MapFrom(s => s.FromIataLocation.Name))
               .ForMember(a => a.InIataLocation, cmd => cmd.MapFrom(s => s.InIataLocation.Name))
               .ForMember(a => a.Volume, cmd => cmd.MapFrom(s => s.MaxPayloadVolume))
               .ForMember(a => a.Weight, cmd => cmd.MapFrom(s => s.MaxPayloadWeight))
               .ReverseMap()
               .ForMember(a => a.AircraftType, cmd => cmd.Ignore())
               .ForMember(a => a.Airline, cmd => cmd.Ignore())
               .ForMember(a => a.FromIataLocation, cmd => cmd.Ignore())
               .ForMember(a => a.InIataLocation, cmd => cmd.Ignore())
               .ForMember(a => a.MaxPayloadVolume, cmd => cmd.MapFrom(s => s.Volume))
               .ForMember(a => a.MaxPayloadWeight, cmd => cmd.MapFrom(s => s.Weight))
               ;

            CreateMap<CommPayloadAt, CommPayloadDto>()
                .ForMember(a => a.AircraftType, cmd => cmd.MapFrom(s => s.AircraftType.IataCode))
                .ForMember(a => a.Airline, cmd => cmd.MapFrom(s => s.Airline.IataCode))
                .ForMember(a => a.FromIataLocation, cmd => cmd.MapFrom(s => s.FromIataLocation.Name))
                .ForMember(a => a.InIataLocation, cmd => cmd.MapFrom(s => s.InIataLocation.Name))
                .ForMember(a => a.Volume, cmd => cmd.MapFrom(s => s.MaxPayloadVolume))
                .ForMember(a => a.Weight, cmd => cmd.MapFrom(s => s.MaxPayloadWeight))
                .ReverseMap()
                .ForMember(a => a.AircraftType, cmd => cmd.Ignore())
                .ForMember(a => a.Airline, cmd => cmd.Ignore())
                .ForMember(a => a.FromIataLocation, cmd => cmd.Ignore())
                .ForMember(a => a.InIataLocation, cmd => cmd.Ignore())
                .ForMember(a => a.MaxPayloadVolume, cmd => cmd.MapFrom(s => s.Volume))
                .ForMember(a => a.MaxPayloadWeight, cmd => cmd.MapFrom(s => s.Weight))
                ;

            CreateMap<Aircraft, AircraftsDto>()
                .ForMember(a => a.AircraftType, cmd => cmd.MapFrom(s => s.AircraftType.IataCode))
                .ForMember(a => a.Weight, cmd => cmd.MapFrom(s => s.MaxTakeOffWeight))
                .ForMember(a => a.Volume, cmd => cmd.MapFrom(s => s.MaxGrossPayload))
                .ReverseMap()
                .ForMember(a => a.AircraftType, cmd => cmd.Ignore())
                .ForMember(a => a.MaxTakeOffWeight, cmd => cmd.MapFrom(s => s.Weight))
                .ForMember(a => a.MaxGrossPayload, cmd => cmd.MapFrom(s => s.Volume));
        }

    }
}
