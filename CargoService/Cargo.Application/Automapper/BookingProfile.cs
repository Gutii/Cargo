using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Cargo.Contract.DTOs;
using Cargo.Contract.DTOs.Bookings;
using Cargo.Infrastructure.Data.Model;
using Cargo.Infrastructure.Data.Model.Dictionary;
using Cargo.Infrastructure.Data.Model.Settings;
using System;
using System.Globalization;
using System.Linq;

namespace Cargo.Application.Automapper
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CloneMaps();
            AwbLightMaps();
            ModelDestMaps();
        }

        /// <summary>
        /// maps-ы для клонирования объектов
        /// </summary>
        void CloneMaps()
        {
            CreateMap<Awb, Awb>().ForMember(to => to.Id, opt => opt.Ignore());
            CreateMap<Booking, Booking>().ForMember(to => to.Id, opt => opt.Ignore());
            CreateMap<AwbContragent, AwbContragent>();
            CreateMap<SizeGroup, SizeGroup>();
            CreateMap<FlightShedule, FlightShedule>();
            CreateMap<Contragent, Contragent>();
            CreateMap<Customer, CustomerDto>();


            CreateMap<Booking, BookingRcs>()
                .ForMember(to => to.FlightSchedule, opt => opt.Ignore())
                .ForMember(to => to.Awb, opt => opt.Ignore())
                .ForMember(to => to.Id, opt => opt.Ignore())
                .EqualityComparison((b1, b2) => b1.Id == b2.Id);
        }

        /// <summary>
        /// maps-ы на объект AwbLightDto (реестровая форма)
        /// </summary>
        void AwbLightMaps()
        {
            CreateMap<Awb, AwbLightDto>()
                .ForMember(bwd => bwd.BookingId, awb => awb.MapFrom(a => a.Id))
                .ForMember(bwd => bwd.AwbId, awb => awb.MapFrom(a => a.Id))
                .ForMember(bwd => bwd.FlightId, awb => awb.MapFrom(a => a.Bookings.FirstOrDefault().FlightScheduleId))
                .ForMember(bwd => bwd.AwbIdentification, awb => awb.MapFrom(a => a.AcPrefix + "-" + a.SerialNumber))
                .ForMember(bwd => bwd.OriginAwb, awb => awb.MapFrom(a => a.Origin))
                .ForMember(bwd => bwd.DestinationAwb, awb => awb.MapFrom(a => a.Destination))
                .ForMember(bwd => bwd.NumberOfPieces, awb => awb.MapFrom(a => a.NumberOfPieces))
                .ForMember(bwd => bwd.Weight, awb => awb.MapFrom(a => a.Weight))
                .ForMember(bwd => bwd.VolumeAmount, awb => awb.MapFrom(a => a.VolumeAmount))
                .ForMember(bwd => bwd.ManifestDescriptionOfGoods, awb => awb.MapFrom(a => a.ManifestDescriptionOfGoods))
                .ForMember(bwd => bwd.SpecialHandlingRequirements, awb => awb.MapFrom(a => a.SpecialHandlingRequirements))
                .ForMember(bwd => bwd.ForwardingAgentId, b => b.MapFrom(f => f.AgentId))
                .ForMember(bwd => bwd.ForwardingAgent, b => b.MapFrom(f => f.Agent.InternationalName))
                .ForMember(bwd => bwd.SpecialServiceRequest, awb => awb.MapFrom(a => a.SpecialServiceRequest))
                .ForMember(bwd => bwd.FlightDestination, b => b.MapFrom(f => f.Bookings.FirstOrDefault().FlightSchedule.Destination))
                .ForMember(bwd => bwd.SpaceAllocationCode, b => b.MapFrom(f => f.Bookings.FirstOrDefault().SpaceAllocationCode))
                .ForMember(bwd => bwd.Product, b => b.MapFrom(a => a.Product))
                .ForMember(bwd => bwd.DiIndicator, b => b.MapFrom<DiIndicatorResolver>())
                .ForMember(bwd => bwd.Status, b => b.MapFrom(f => f.Status))


                .ForMember(bwd => bwd.Carrier, opt => opt.MapFrom(f => f.Bookings.FirstOrDefault().FlightSchedule.Number.Substring(0, 2)))
                .ForMember(bwd => bwd.FlightNumber, opt => opt.MapFrom(f => f.Bookings.FirstOrDefault().FlightSchedule.Number.Substring(2)))
                .ForMember(bwd => bwd.FlightOrigin, opt => opt.MapFrom(f => f.Bookings.FirstOrDefault().FlightSchedule.Origin))
                .ForMember(bwd => bwd.FlightDestination, opt => opt.MapFrom(f => f.Bookings.FirstOrDefault().FlightSchedule.Destination))
                .ForMember(bwd => bwd.FullFlights, opt => opt.MapFrom(f => string.Join("; ", f.Bookings.Select(fs => $"{fs.FlightSchedule.Number}/{fs.FlightSchedule.FlightDate.ToString("dd")}{fs.FlightSchedule.FlightDate.ToString("MMM", new CultureInfo("en-US")).ToUpper()} ({fs.SpaceAllocationCode})"))))
                .ForMember(bwd => bwd.FlightDate, opt => opt.MapFrom(f => f.Bookings.FirstOrDefault().FlightSchedule.FlightDate))
                .ForMember(bwd => bwd.StDeparture, opt => opt.MapFrom(f => f.Bookings.FirstOrDefault().FlightSchedule.StOrigin))
                .ForMember(bwd => bwd.StArrival, opt => opt.MapFrom(f => f.Bookings.FirstOrDefault().FlightSchedule.StDestination));


            CreateMap<Booking, AwbLightDto>()
                .ForMember(to => to.BookingId, opt => opt.MapFrom(b => b.Id))
                .ForMember(to => to.AwbId, opt => opt.MapFrom(b => b.AwbId))
                .ForMember(to => to.FlightId, opt => opt.MapFrom(b => b.FlightScheduleId))
                .ForMember(to => to.AwbIdentification, opt => opt.MapFrom(b => b.Awb.AcPrefix + "-" + b.Awb.SerialNumber))
                .ForMember(to => to.OriginAwb, opt => opt.MapFrom(b => b.Awb.Origin))
                .ForMember(to => to.DestinationAwb, opt => opt.MapFrom(b => b.Awb.Destination))
                .ForMember(to => to.NumberOfPieces, opt => opt.MapFrom(b => b.NumberOfPieces))
                .ForMember(to => to.Weight, opt => opt.MapFrom(b => b.Weight))
                .ForMember(to => to.VolumeAmount, opt => opt.MapFrom(b => b.VolumeAmount))
                .ForMember(to => to.ManifestDescriptionOfGoods, opt => opt.MapFrom(b => b.Awb.ManifestDescriptionOfGoods))
                .ForMember(to => to.SpecialHandlingRequirements, opt => opt.MapFrom(b => b.Awb.SpecialHandlingRequirements))
                .ForMember(to => to.ForwardingAgentId, b => b.MapFrom(b => b.Awb.AgentId))
                .ForMember(to => to.ForwardingAgent, b => b.MapFrom(b => b.Awb.Agent.InternationalName))
                .ForMember(to => to.SpecialServiceRequest, opt => opt.MapFrom(b => b.Awb.SpecialServiceRequest))
                .ForMember(to => to.FlightDestination, b => b.MapFrom(b => b.FlightSchedule.Destination))
                .ForMember(to => to.SpaceAllocationCode, b => b.MapFrom(b => b.SpaceAllocationCode))
                .ForMember(to => to.Product, b => b.MapFrom(b => b.Awb.Product))
                .ForMember(to => to.Status, b => b.MapFrom(b => b.Awb.Status))

                .ForMember(bwd => bwd.Carrier, opt => opt.MapFrom(f => f.FlightSchedule.Number.Substring(0, 2)))
                .ForMember(bwd => bwd.FlightNumber, opt => opt.MapFrom(f => f.FlightSchedule.Number.Substring(2)))
                .ForMember(bwd => bwd.FlightOrigin, opt => opt.MapFrom(f => f.FlightSchedule.Origin))
                .ForMember(bwd => bwd.FlightDestination, opt => opt.MapFrom(f => f.FlightSchedule.Destination))
                .ForMember(bwd => bwd.FullFlights, opt => { opt.PreCondition(f => f.FlightSchedule.FlightDate > DateTime.MinValue); opt.MapFrom(f => $"{f.FlightSchedule.Number}/{f.FlightSchedule.FlightDate.ToString("dd")}{f.FlightSchedule.FlightDate.ToString("MMM", new CultureInfo("en-US")).ToUpper()}"); })
                .ForMember(bwd => bwd.FlightDate, opt => { opt.PreCondition(f => f.FlightSchedule.FlightDate > DateTime.MinValue); opt.MapFrom(f => f.FlightSchedule.FlightDate); })
                .ForMember(bwd => bwd.StDeparture, opt => opt.MapFrom(f => f.FlightSchedule.StOrigin))
                .ForMember(bwd => bwd.StArrival, opt => opt.MapFrom(f => f.FlightSchedule.StDestination));
        }

        /// <summary>
        /// maps-ы на модель иои из модели
        /// </summary>
        void ModelDestMaps()
        {
            CreateMap<TaxCharge, TaxChargeDto>();
            CreateMap<OtherCharge, OtherChargeDto>().ReverseMap();
            CreateMap<Awb, AwbDto>()
                .ForMember(to => to.DiIndicator, b => b.MapFrom<DiIndicatorResolver>())
                .ForMember(to => to.ForwardingAgentId, opt => opt.MapFrom(f => f.AgentId))
                .ForMember(to => to.ForwardingAgent, opt => opt.MapFrom(f => f.Agent.InternationalName))
                .ForPath(to => to.Charge.Prepaid, from => from.MapFrom(x => x.Prepaid))
                .ForPath(to => to.Charge.Collect, from => from.MapFrom(x => x.Collect))
                .ReverseMap()
                .ForMember(to => to.Id, opt => opt.Ignore())
                .ForMember(to => to.CreatedDate, opt => { opt.PreCondition(f => !f.Id.HasValue || f.Id == 0); opt.MapFrom(f => DateTime.UtcNow); })
                .ForMember(to => to.PoolAwbId, opt => { opt.PreCondition(f => f.PoolAwbNumId.HasValue && f.PoolAwbNumId > 0); opt.MapFrom(f => f.PoolAwbNumId); })
                .ForMember(to => to.AgentId, opt => { opt.PreCondition(f => !f.Id.HasValue || f.Id == 0); opt.MapFrom(f => f.ForwardingAgentId ?? (int?)f.Agent.Id); })
                .ForMember(to => to.Agent, opt => opt.Ignore())
                .ForMember(to => to.ConsigneeId, opt => opt.Ignore())
                .ForMember(to => to.ConsignorId, opt => opt.Ignore())
                 .ForMember(to => to.Status, opt => { opt.PreCondition(f => !f.Id.HasValue || f.Id == 0); opt.MapFrom(f => "Draft"); })
                // .ForMember(to => to.Prepaid, opt => opt.Ignore())
                // .ForMember(to => to.Collect, opt => opt.Ignore())
                // .ForMember(to => to.BookingRcs, opt => opt.Ignore())
                //.ForMember(to => to.OtherCharges, opt => opt.Ignore())
                .ForMember(to => to.Messages, opt => opt.Ignore());
            CreateMap<Booking, BookingDto>().ReverseMap()
                .ForMember(to => to.FlightScheduleId, opt => opt.MapFrom(b => b.FlightSchedule.Id))
                .ForMember(to => to.FlightSchedule, opt => opt.Ignore())
                .ForMember(to => to.QuanDetShipmentDescriptionCode, opt => opt.MapFrom(f => f.QuanDetShipmentDescriptionCode ?? "T"))
                .ForMember(to => to.SpaceAllocationCode, opt => opt.MapFrom(f => f.SpaceAllocationCode ?? "NN"))
                .ForMember(to => to.WeightCode, opt => opt.MapFrom(f => f.WeightCode ?? "K"))
                .ForMember(to => to.VolumeCode, opt => opt.MapFrom(f => f.VolumeCode ?? "MC"))
                .EqualityComparison((b1, b2) => b1.Id == b2.Id)
                ;

            CreateMap<BookingRcs, BookingDto>();
            CreateMap<AwbContragent, AwbContragentDto>().ReverseMap().ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<SizeGroupDto, SizeGroup>()
                .ForMember(sg => sg.AwbId, opt => opt.Ignore())
                .ForMember(to => to.Lenght, opt => opt.MapFrom(b => b.Length))
                .ReverseMap()
                .ForMember(to => to.Length, opt => opt.MapFrom(b => b.Lenght));

            CreateMap<Message, MessageDto>()
                .ReverseMap();


            CreateMap<FlightShedule, FlightSheduleDto>().ReverseMap();



            CreateMap<Contragent, AwbContragentDto>()
                .ForMember(c => c.Id, opt => opt.MapFrom(caed => caed.Id))
                .ForMember(c => c.NameEn, opt => opt.MapFrom(caed => caed.InternationalName))
                .ForMember(c => c.NameRu, opt => opt.MapFrom(caed => caed.RussianName))
                .ForMember(c => c.CityEn, opt => opt.MapFrom(caed => caed.Place))
                .ForMember(c => c.CityRu, opt => opt.MapFrom(caed => caed.PlaceRus))


                .ForMember(c => c.AddressEn, opt => opt.MapFrom(caed => caed.StreetAddressName))
                .ForMember(c => c.AddressRu, opt => opt.MapFrom(caed => caed.StreetAddressNameRus))
                .ForMember(c => c.CodeEn, opt => opt.MapFrom(caed => caed.StateCode))
                .ForMember(c => c.CountryISO, opt => opt.MapFrom(caed => caed.Country.Alpha2))

                .ForMember(c => c.NameExEn, opt => opt.MapFrom(caed => caed.NameEngAdditional))
                .ForMember(c => c.NameExRu, opt => opt.MapFrom(caed => caed.NameRusAdditional))
                //.ForMember(c => c.Passport, opt => opt.MapFrom(caed => caed.))

                .ForMember(c => c.RegionEn, opt => opt.MapFrom(caed => caed.StateProvince))
                .ForMember(c => c.RegionRu, opt => opt.MapFrom(caed => caed.StateProvince))


                /*.ForMember(c => c.Phone, opt => opt.MapFrom(caed => caed.Phone))
                .ForMember(c => c.Fax, opt => opt.MapFrom(caed => caed.Fax))
                .ForMember(c => c.AgentCass, opt => opt.MapFrom(caed => caed.IataCargoAgentCassAddress))
                .ForMember(c => c.IataCode, opt => opt.MapFrom(caed => caed.IataCargoAgentNumericCode))*/

                .ReverseMap()
                ;
        }
    }

    public class DiIndicatorResolver : IValueResolver<Awb, object, string>
    {
        public string Resolve(Awb source, object destination, string destMember, ResolutionContext context)
        {
            return "DOM";
        }
    }
}
