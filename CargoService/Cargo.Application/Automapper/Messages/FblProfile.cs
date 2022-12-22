using AutoMapper;
using Cargo.Infrastructure.Data.Model;
using IDeal.Common.Components.Messages.ObjectStructures.Fbls.Ver4;
using System;
using System.Globalization;

namespace Cargo.Application.Automapper.Messages
{
    public class FblProfile : Profile
    {
        public FblProfile()
        {
            this.CreateMap<FlightShedule, Fbl>()
            .ForMember(fd => fd.FlightInformation, fs => fs.MapFrom(f => f))
            .ForMember(fd => fd.PortUnloading, fs => fs.MapFrom(f => f))
            .ForMember(fd => fd.AwbInfo, fs => fs.MapFrom(f => f.Bookings));

            this.CreateMap<FlightShedule, PortUnloading>()
                .ForMember(fid => fid.AirportCode, fs => fs.MapFrom(f => f.Destination))
                .ForMember(fid => fid.NilCargoIndication, fs => fs.Ignore());

            this.CreateMap<FlightShedule, FlightInformation>()
            .ForMember(fid => fid.AirportCode, fs => fs.MapFrom(f => f.Origin))
            .ForMember(fid => fid.CarrierCode, fs => fs.MapFrom(f => f.Number.Substring(0, 2)))
            .ForMember(fid => fid.Day, fs => fs.MapFrom(f => f.StOrigin.ToString("dd").ToUpper()))
            .ForMember(fid => fid.FlightNumber, fs => fs.MapFrom(f => f.Number.Substring(2)))
            .ForMember(fid => fid.Month, fs => fs.MapFrom(f => f.StOrigin.ToString("MMM", new CultureInfo("en-US")).ToUpper()));

            this.CreateMap<Booking, AwbInfo>()
            .ForMember(aid => aid.ConsignmentDetail, fs => fs.MapFrom(b => b))
            .ForMember(aid => aid.ConsignmentOriginInformation, fs => fs.MapFrom(b => b.Awb))
            .ForMember(aid => aid.SpecialServiceRequest, fs => fs.MapFrom(b => b.Awb));

            this.CreateMap<Booking, ConsignmentDetail>()
            .ForMember(cdd => cdd.AwbIdentification, fs => fs.MapFrom(b => $"{b.Awb.AcPrefix}-{b.Awb.SerialNumber}"))
            .ForMember(cdd => cdd.AwbOrigin, fs => fs.MapFrom(b => b.Awb.Origin))
            .ForMember(cdd => cdd.AwbDestination, fs => fs.MapFrom(b => b.Awb.Destination))
            .ForMember(cdd => cdd.QuantityDetail, fs => fs.MapFrom(b => b))
            .ForMember(cdd => cdd.NatureOfGood, fs => fs.MapFrom(b => b.Awb.ManifestDescriptionOfGoods))
            .ForMember(cdd => cdd.SpecialHandlingRequirements, fs => fs.MapFrom(b => b.Awb.SpecialHandlingRequirements));

            this.CreateMap<Booking, QuantityDetail>()
            .ForMember(qdd => qdd.ShipmentDescriptionCode, mce => mce.MapFrom(awb => awb.QuanDetShipmentDescriptionCode))
            .ForMember(qdd => qdd.NumberOfPieces, mce => mce.MapFrom(awb => awb.NumberOfPieces))
            .ForMember(qdd => qdd.WeightCode, mce => mce.MapFrom(awb => awb.WeightCode))
            .ForMember(qdd => qdd.Weight, mce => mce.MapFrom(awb => $"{Math.Round(awb.Weight, 3):G29}".Replace(',', '.')))
            .ForMember(vdd => vdd.VolumeCode, mce => mce.MapFrom(b => b.VolumeCode))
            .ForMember(vdd => vdd.Volume, mce => mce.MapFrom(b => $"{Math.Round(b.VolumeAmount, 3):G29}".Replace(',', '.')));

            this.CreateMap<Awb, ConsignmentOriginInformation>()
            .ForMember(coid => coid.ForwardingAgent, fs => fs.MapFrom(a => a.Agent.InternationalName));
            /* .ForMember(coid => coid.InwardFlightDetails, fs => fs.MapFrom(a => a))
             .ForMember(coid => coid.MovementPriority, fs => fs.MapFrom(a => a));*/

            this.CreateMap<Awb, InwardFlightDetails>();

            this.CreateMap<Awb, SpecialServiceRequest>()
            .ForMember(ssrd => ssrd.SpecialServiceRequest1, fs => fs.MapFrom(a => a.SpecialServiceRequest));
            // .ForMember(ssrd => ssrd.SpecialServiceRequest2, fs => fs.MapFrom(a => a));

        }
    }
}
