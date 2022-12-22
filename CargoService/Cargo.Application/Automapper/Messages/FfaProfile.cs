using AutoMapper;
using Cargo.Infrastructure.Data.Model;
using IDeal.Common.Components.Messages.ObjectStructures.Ffas.Ver4;
using System;
using System.Globalization;
using System.Linq;

namespace Cargo.Application.Automapper.Messages
{
    public class FfaProfile : Profile
    {
        public FfaProfile()
        {
            CreateMap<Awb, Ffa>()
                .ForMember(ffa => ffa.StandardMessageIdentification, mce => mce.MapFrom(awb => awb))
                .ForMember(ffa => ffa.ConsignmentDetail, mce => mce.MapFrom(awb => awb))
                .ForMember(ffa => ffa.SpecialServiceRequest, mce => mce.MapFrom(awb => awb))
                .ForMember(ffa => ffa.FlightDetails, mce => mce.MapFrom(awb => awb.Bookings));

            CreateMap<Awb, StandardMessageIdentification>()
                .ForMember(smi => smi.MessageTypeVersionNumber, opt => opt.MapFrom(n => "4"))
                .ForMember(smi => smi.StandardMessageIdentifier, opt => opt.MapFrom(t => "FFA"));

            CreateMap<Awb, ConsignmentDetail>()
                .ForMember(cdd => cdd.AwbIdentification, mce => mce.MapFrom(awb => new AwbIdentification { AirlinePrefix = awb.AcPrefix, AwbSerialNumber = awb.SerialNumber }))
                .ForMember(cdd => cdd.AwbOriginAndDestination, mce => mce.MapFrom(awb => new AwbOriginAndDestination { AirportCodeOfOrigin = awb.Origin, AirportCodeOfDestitation = awb.Destination }))
                .ForMember(cdd => cdd.QuantityDetail, mce => mce.MapFrom(awb => awb))
                .ForMember(cdd => cdd.NatureOfGoods, mce => mce.MapFrom(awb => awb))
                .ForMember(cdd => cdd.SpecialHandlingRequirements, mce =>
                {
                    mce.PreCondition(awb => !string.IsNullOrEmpty(awb.SpecialHandlingRequirements));
                    mce.MapFrom(awb => awb.SpecialHandlingRequirements
                    .Trim('/')
                    .Split('/', StringSplitOptions.None)
                    .Select(s => new SpecialHandlingRequirements { SpecialHandlingCode = s })
                    .ToList());
                })
                .ForMember(cdd => cdd.TotalConsignmentPieces, mce =>
                {
                    mce.PreCondition(awb => awb.QuanDetShipmentDescriptionCode == "P");
                    mce.MapFrom(awb => awb);
                })
                ;

            CreateMap<Awb, TotalConsignmentPieces>()
                .ForMember(tcpd => tcpd.ShipmentDescriptionCode, mce => mce.MapFrom(awb => awb.QuanDetShipmentDescriptionCode))
                .ForMember(tcpd => tcpd.NumberOfPieces, mce => mce.MapFrom(awb => awb.NumberOfPieces));

            CreateMap<Awb, QuantityDetail>()
                .ForMember(qd => qd.ShipmentDescriptionCode, mce => mce.MapFrom(awb => awb.QuanDetShipmentDescriptionCode))
                .ForMember(qd => qd.NumberOfPieces, mce => mce.MapFrom(awb => awb.NumberOfPieces))
                .ForMember(qd => qd.WeightCode, mce => mce.MapFrom(awb => awb.WeightCode))
                .ForMember(qd => qd.Weight, mce => mce.MapFrom(awb => $"{Math.Round(awb.Weight, 3):G29}".Replace(',', '.')));

            CreateMap<Awb, NatureOfGoods>()
                .ForMember(nog => nog.ManifestDescriptionOfGoods, mce => mce.MapFrom(awb => awb.ManifestDescriptionOfGoods));

            CreateMap<Awb, SpecialServiceRequest>()
                .ForMember(ssr => ssr.SsrDetails1, mce => mce.MapFrom(awb => awb));

            CreateMap<Awb, SsrDetails1>()
                .ForMember(ssrd => ssrd.SpecialServiceRequestInner, mce => mce.MapFrom(awb => awb.SpecialServiceRequest));


            CreateMap<Booking, FlightDetails>()
                .ForMember(fd => fd.FlightIdentification, mce => mce.MapFrom(b => b.FlightSchedule))
                .ForMember(fd => fd.AirportsOfDepartureAndArrival, mce => mce.MapFrom(b => b.FlightSchedule))
                .ForMember(fd => fd.SpaceAllocationCode, mce => mce.MapFrom(b => b.SpaceAllocationCode))
                ;
            CreateMap<FlightShedule, FlightIdentification>()
                .ForMember(fi => fi.CarrierCode, mce => mce.MapFrom(f => f.Number.Substring(0, 2)))
                .ForMember(fi => fi.FlightNumber, mce => mce.MapFrom(f => f.Number.Substring(2)))
                .ForMember(fi => fi.Month, b => b.MapFrom(f => f.FlightDate.ToString("MMM", new CultureInfo("en-US")).ToUpper()))
                .ForMember(fi => fi.Day, b => b.MapFrom(f => f.FlightDate.ToString("dd").ToUpper()))
            ;

            CreateMap<FlightShedule, AirportsOfDepartureAndArrival>()
                .ForMember(fi => fi.AirportCodeOfArrival, mce => mce.MapFrom(f => f.Origin))
                .ForMember(fi => fi.AirportCodeOfDeparture, mce => mce.MapFrom(f => f.Destination))
                ;

        }
    }
}
