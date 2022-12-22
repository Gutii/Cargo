using AutoMapper;
using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Data.Model;
using IDeal.Common.Components;
using IDeal.Common.Messaging.Histories;
using IDeal.Common.Messaging.Messages;
using IDeal.Common.Messaging.Shedule;


namespace Cargo.Application.Automapper
{
    public class MessagingProfile : Profile
    {
        public MessagingProfile()
        {
            CreateMap<MessageChanged, Message>()
                .ForMember(dst => dst.Id, mf => mf.Ignore())
                .ForMember(dst => dst.LinkedFlight, mf => mf.Ignore())
                .ForMember(dst => dst.LinkedAwb, mf => mf.Ignore())
                ;

            CreateMap<FlightSheduleChanged, FlightShedule>()
                .ForMember(dst => dst.ExternalId, mf => mf.MapFrom(x => x.Id))
                .ForMember(dst => dst.PayloadVolume, mf => mf.Ignore())
                .ForMember(dst => dst.PayloadWeight, mf => mf.Ignore())
                .ForMember(dst => dst.Messages, mf => mf.Ignore())
                .ForMember(dst => dst.Bookings, mf => mf.Ignore())
                .ForMember(dst => dst.BookingRcs, mf => mf.Ignore())
                .ForMember(dst => dst.AircraftRegistration,
                    mf => mf.MapFrom(src => src.AircraftRegistration ?? string.Empty))
                .ForMember(dst => dst.AircraftType, mf => mf.MapFrom(src => src.AircraftType ?? string.Empty))
                .ForMember(dst => dst.SHR, mf => mf.MapFrom(src => src.SHR ?? string.Empty))
                .ForMember(dst => dst.SaleState, mf => mf.MapFrom(changed => (FlightSaleState)changed.SaleState));

            this.CreateMap<ChangeTrack, Change>();

        }

    }
}
