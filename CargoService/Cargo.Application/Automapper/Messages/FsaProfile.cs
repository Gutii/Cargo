using AutoMapper;
using Cargo.Infrastructure.Data.Model;
using IDeal.Common.Components.Messages.ObjectStructures.Fsas;
using System;
using System.Globalization;

namespace Cargo.Application.Automapper.Messages
{
    public class FsaProfile : Profile
    {
        public FsaProfile()
        {
            Fsa4ConsignmentStatus();

            #region FSA/BKD

            //FsaDto bkd = new FsaDto
            CreateMap<Awb, Fsa>()
                .ForMember(fsaDto => fsaDto.StandardMessageIdentification, mce => mce.MapFrom(awb => awb))
                .ForMember(fsaDto => fsaDto.ConsignmentDetail, mce => mce.MapFrom(awb => awb))
                .ForMember(fsaDto => fsaDto.StatusDetailsBkd, mce => mce.MapFrom(awb => awb.Bookings));

            CreateMap<Awb, StandardMessageIdentification>()
                .ForMember(smi => smi.MessageTypeVersionNumber, r => r.MapFrom(t => "15"))
                .ForMember(smi => smi.StandardMessageIdentifier, r => r.MapFrom(t => "FSA"));

            CreateMap<Awb, ConsignmentDetail>()
                .ForMember(cdd => cdd.AwbIdentification, mce => mce.MapFrom(awb => new AwbIdentification { AirlinePrefix = awb.AcPrefix, AwbSerialNumber = awb.SerialNumber }))
                .ForMember(cdd => cdd.AwbOriginAndDestination, mce => mce.MapFrom(awb => new AwbOriginAndDestination { AirportCodeOfOrigin = awb.Origin, AirportCodeOfDestitation = awb.Destination }))
                .ForMember(cdd => cdd.QuantityDetail, mce => mce.MapFrom(awb => awb))
                .ForMember(cdd => cdd.TotalConsignmentPieces, mce =>
                {
                    mce.PreCondition(awb => awb.QuanDetShipmentDescriptionCode == "P");
                    mce.MapFrom(awb => awb);
                });

            CreateMap<Awb, TotalConsignmentPieces>()
                .ForMember(tcpd => tcpd.ShipmentDescriptionCode, mce => mce.MapFrom(awb => awb.QuanDetShipmentDescriptionCode))
                .ForMember(tcpd => tcpd.NumberOfPieces, mce => mce.MapFrom(awb => awb.NumberOfPieces));

            CreateMap<Awb, QuantityDetail>()
                .ForMember(qdd => qdd.ShipmentDescriptionCode, mce => mce.MapFrom(awb => awb.QuanDetShipmentDescriptionCode))
                .ForMember(qdd => qdd.NumberOfPieces, mce => mce.MapFrom(awb => awb.NumberOfPieces))
                .ForMember(qdd => qdd.WeightCode, mce => mce.MapFrom(awb => awb.WeightCode))
                .ForMember(qdd => qdd.Weight, mce => mce.MapFrom(awb => $"{Math.Round(awb.Weight, 3):G29}".Replace(',', '.')));



            CreateMap<Booking, StatusDetailsBkd>()
                .ForMember(rci => rci.MovementDetailWithOrigAndDest, mce => mce.MapFrom(booking => booking.FlightSchedule))
                .ForMember(rci => rci.QuantityDetail, mce => mce.MapFrom(booking => booking))
                .ForMember(rci => rci.TimeOfDepartureInformation, mce => mce.MapFrom(booking => booking.FlightSchedule))
                .ForMember(rci => rci.TimeOfArrivalInformation, mce => mce.MapFrom(booking => booking.FlightSchedule))
                .ForMember(rci => rci.VolumeDetail, mce => mce.MapFrom(booking => booking));

            CreateMap<FlightShedule, MovementDetailWithOrigAndDest>()
                .ForMember(mdwoadd => mdwoadd.CarrierCode, mce => mce.MapFrom(f => f.Number.Substring(0, 2)))
                .ForMember(mdwoadd => mdwoadd.FlightNumber, mce => mce.MapFrom(f => f.Number.Substring(2)))
                .ForMember(mdwoadd => mdwoadd.AirportCodeOfDeparture, mce => mce.MapFrom(f => f.Origin))
                .ForMember(mdwoadd => mdwoadd.AirportCodeOfArrival, mce => mce.MapFrom(f => f.Destination))
                .ForMember(mdwoadd => mdwoadd.Day, b => b.MapFrom(f => f.StOrigin.ToString("dd").ToUpper()))
                .ForMember(mdwoadd => mdwoadd.Month, b => b.MapFrom(f => f.StOrigin.ToString("MMM", new CultureInfo("en-US")).ToUpper()));


            CreateMap<Booking, QuantityDetail>()
                .ForMember(mdwoadd => mdwoadd.ShipmentDescriptionCode, mce => mce.MapFrom(b => b.QuanDetShipmentDescriptionCode))
                .ForMember(mdwoadd => mdwoadd.NumberOfPieces, mce => mce.MapFrom(b => b.NumberOfPieces))
                .ForMember(mdwoadd => mdwoadd.WeightCode, mce => mce.MapFrom(b => b.WeightCode))
                .ForMember(mdwoadd => mdwoadd.Weight, mce => mce.MapFrom(b => $"{Math.Round(b.Weight, 3):G29}".Replace(',', '.')));

            CreateMap<FlightShedule, TimeOfDepartureInformation>()
                .ForMember(todid => todid.TypeOfTimeIndicator, mce => mce.MapFrom(f => "S"))
                .ForMember(todid => todid.Time, mce => mce.MapFrom(f => f.StOrigin.ToString("HHmm")))
                .ForMember(todid => todid.DayChangeIndicator, mce => mce.MapFrom(b => ""));

            CreateMap<FlightShedule, TimeOfArrivalInformation>()
                .ForMember(todid => todid.TypeOfTimeIndicator, mce => mce.MapFrom(f => "S"))
                .ForMember(todid => todid.Time, mce => mce.MapFrom(f => f.StDestination.ToString("HHmm")))
                .ForMember(todid => todid.DayChangeIndicator, mce => mce.MapFrom(b => ""));

            CreateMap<Booking, VolumeDetail>()
                .ForMember(vdd => vdd.VolumeCode, mce => mce.MapFrom(b => b.VolumeCode))
                .ForMember(vdd => vdd.VolumeAmount, mce => mce.MapFrom(b => $"{Math.Round(b.VolumeAmount, 3):G29}".Replace(',', '.')))
                ;
            #endregion
        }

        void Fsa4ConsignmentStatus()
        {
            CreateMap<Fsu, Awb>()
                .ForMember(a => a.AcPrefix, opt => opt.MapFrom(f => f.ConsignmentDetail.AwbIdentification.AirlinePrefix))
                .ForMember(a => a.SerialNumber, opt => opt.MapFrom(f => f.ConsignmentDetail.AwbIdentification.AwbSerialNumber))
                .ForMember(a => a.Origin, opt => opt.MapFrom(f => f.ConsignmentDetail.AwbOriginAndDestination.AirportCodeOfOrigin))
                .ForMember(a => a.Destination, opt => opt.MapFrom(f => f.ConsignmentDetail.AwbOriginAndDestination.AirportCodeOfDestitation))
                .ForMember(a => a.CreatedDate, opt => opt.MapFrom(f => DateTime.UtcNow))
                .ForMember(a => a.NumberOfPieces, opt => opt.MapFrom(f => f.ConsignmentDetail.QuantityDetail.NumberOfPieces))
                .ForMember(a => a.Weight, opt => opt.MapFrom(f => f.ConsignmentDetail.QuantityDetail.Weight))
                ;

            CreateMap<StatusDetailsBkd, ConsignmentStatus>()
                .ForMember(a => a.StatusCode, opt => opt.MapFrom(f => "BKD"))
                .ForMember(a => a.Source, opt => opt.MapFrom(f => "FSU"))
                .ForMember(a => a.DateChange, opt => opt.MapFrom(f => DateTime.UtcNow))
                .ForMember(a => a.AirportCode, opt => opt.MapFrom(f => f.MovementDetailWithOrigAndDest.AirportCodeOfDeparture))
                .ForMember(a => a.NumberOfPiece, opt => opt.MapFrom(f => int.Parse(f.QuantityDetail.NumberOfPieces)))
                .ForMember(a => a.Weight, opt => opt.MapFrom(f => int.Parse(f.QuantityDetail.Weight)))
                .ForMember(a => a.VolumeAmount, opt => opt.MapFrom(f => int.Parse(f.VolumeDetail.VolumeAmount)))
                .ForMember(a => a.TitleRu, opt => opt.MapFrom(f => $"Забронировано \"{f.MovementDetailWithOrigAndDest.AirportCodeOfDeparture} - {f.MovementDetailWithOrigAndDest.AirportCodeOfArrival}\""))
                .ForMember(a => a.TitleEn, opt => opt.MapFrom(f => $"Booked \"{f.MovementDetailWithOrigAndDest.AirportCodeOfDeparture} - {f.MovementDetailWithOrigAndDest.AirportCodeOfArrival}\""))
                ;

            CreateMap<StatusDetailsFoh, ConsignmentStatus>()
                .ForMember(a => a.StatusCode, opt => opt.MapFrom(f => "FOH"))
                .ForMember(a => a.Source, opt => opt.MapFrom(f => "FSU"))
                .ForMember(a => a.DateChange, opt => opt.MapFrom(f => DateTime.UtcNow))
                .ForMember(a => a.AirportCode, opt => opt.MapFrom(f => f.MovementDetail.AirportCode))
                .ForMember(a => a.NumberOfPiece, opt => opt.MapFrom(f => int.Parse(f.QuantityDetail.NumberOfPieces)))
                .ForMember(a => a.Weight, opt => opt.MapFrom(f => int.Parse(f.QuantityDetail.Weight)))
                .ForMember(a => a.VolumeAmount, opt => opt.MapFrom(f => int.Parse(f.VolumeDetail.VolumeAmount)))
                .ForMember(a => a.TitleRu, opt => opt.MapFrom(f => $"Груз сдан"))
                .ForMember(a => a.TitleEn, opt => opt.MapFrom(f => $"Freight is on hand"))
                ;

            CreateMap<StatusDetailsRcs, ConsignmentStatus>()
                .ForMember(a => a.StatusCode, opt => opt.MapFrom(f => "RCS"))
                .ForMember(a => a.Source, opt => opt.MapFrom(f => "FSU"))
                .ForMember(a => a.DateChange, opt => opt.MapFrom(f => DateTime.UtcNow))
                .ForMember(a => a.AirportCode, opt => opt.MapFrom(f => f.MovementDetail.AirportCode))
                .ForMember(a => a.NumberOfPiece, opt => opt.MapFrom(f => int.Parse(f.QuantityDetail.NumberOfPieces)))
                .ForMember(a => a.Weight, opt => opt.MapFrom(f => int.Parse(f.QuantityDetail.Weight)))
                .ForMember(a => a.VolumeAmount, opt => opt.MapFrom(f => int.Parse(f.VolumeDetail.VolumeAmount)))
                .ForMember(a => a.TitleRu, opt => opt.MapFrom(f => "Принято к перевозке"))
                .ForMember(a => a.TitleEn, opt => opt.MapFrom(f => "Cargo is ready for carriage"))
                ;

            CreateMap<StatusDetailsMan, ConsignmentStatus>()
                .ForMember(a => a.StatusCode, opt => opt.MapFrom(f => "MAN"))
                .ForMember(a => a.Source, opt => opt.MapFrom(f => "FSU"))
                .ForMember(a => a.DateChange, opt => opt.MapFrom(f => DateTime.UtcNow))
                .ForMember(a => a.AirportCode, opt => opt.MapFrom(f => f.MovementDetailWithOrigAndDest.AirportCodeOfDeparture))
                .ForMember(a => a.NumberOfPiece, opt => opt.MapFrom(f => int.Parse(f.QuantityDetail.NumberOfPieces)))
                .ForMember(a => a.Weight, opt => opt.MapFrom(f => int.Parse(f.QuantityDetail.Weight)))
                .ForMember(a => a.VolumeAmount, opt => opt.MapFrom(f => 0))
                .ForMember(a => a.TitleRu, opt => opt.MapFrom(f => $"Заманифестирован на рейс \"{f.MovementDetailWithOrigAndDest.CarrierCode}{f.MovementDetailWithOrigAndDest.FlightNumber}/{f.MovementDetailWithOrigAndDest.Day}{f.MovementDetailWithOrigAndDest.Month}\""))
                .ForMember(a => a.TitleEn, opt => opt.MapFrom(f => $"Manifested on a flight \"{f.MovementDetailWithOrigAndDest.CarrierCode}{f.MovementDetailWithOrigAndDest.FlightNumber}/{f.MovementDetailWithOrigAndDest.Day}{f.MovementDetailWithOrigAndDest.Month}\""))
                ;

            CreateMap<StatusDetailsDep, ConsignmentStatus>()
                .ForMember(a => a.StatusCode, opt => opt.MapFrom(f => "DEP"))
                .ForMember(a => a.Source, opt => opt.MapFrom(f => "FSU"))
                .ForMember(a => a.DateChange, opt => opt.MapFrom(f => DateTime.UtcNow))
                .ForMember(a => a.AirportCode, opt => opt.MapFrom(f => f.MovementDetailWithOrigAndDest.AirportCodeOfDeparture))
                .ForMember(a => a.NumberOfPiece, opt => opt.MapFrom(f => int.Parse(f.QuantityDetail.NumberOfPieces)))
                .ForMember(a => a.Weight, opt => opt.MapFrom(f => int.Parse(f.QuantityDetail.Weight)))
                .ForMember(a => a.VolumeAmount, opt => opt.MapFrom(f => Convert.ToDouble(f.QuantityDetail.Volume, CultureInfo.InvariantCulture)))
                .ForMember(a => a.TitleRu, opt => opt.MapFrom(f => $"Убыло на рейсе \"{f.MovementDetailWithOrigAndDest.CarrierCode}{f.MovementDetailWithOrigAndDest.FlightNumber}/{f.MovementDetailWithOrigAndDest.Day}{f.MovementDetailWithOrigAndDest.Month}\" \"{f.MovementDetailWithOrigAndDest.AirportCodeOfDeparture} - {f.MovementDetailWithOrigAndDest.AirportCodeOfArrival}\""))
                .ForMember(a => a.TitleEn, opt => opt.MapFrom(f => $"Departed on \"{f.MovementDetailWithOrigAndDest.CarrierCode}{f.MovementDetailWithOrigAndDest.FlightNumber}/{f.MovementDetailWithOrigAndDest.Day}{f.MovementDetailWithOrigAndDest.Month}\" \"{f.MovementDetailWithOrigAndDest.AirportCodeOfDeparture} - {f.MovementDetailWithOrigAndDest.AirportCodeOfArrival}\""))
                ;

            CreateMap<StatusDetailsArr, ConsignmentStatus>()
                .ForMember(a => a.StatusCode, opt => opt.MapFrom(f => "ARR"))
                .ForMember(a => a.Source, opt => opt.MapFrom(f => "FSU"))
                .ForMember(a => a.DateChange, opt => opt.MapFrom(f => DateTime.UtcNow))
                .ForMember(a => a.AirportCode, opt => opt.MapFrom(f => f.MovementDetailWithFlight.AirportCode))
                .ForMember(a => a.NumberOfPiece, opt => opt.MapFrom(f => int.Parse(f.QuantityDetail.NumberOfPieces)))
                .ForMember(a => a.Weight, opt => opt.MapFrom(f => int.Parse(f.QuantityDetail.Weight)))
                .ForMember(a => a.VolumeAmount, opt => opt.MapFrom(f => 0))
                .ForMember(a => a.TitleRu, opt => opt.MapFrom(f => $"Прибыло на рейсе \"{f.MovementDetailWithFlight.CarrierCode}{f.MovementDetailWithFlight.FlightNumber}/{f.MovementDetailWithFlight.Day}{f.MovementDetailWithFlight.Month}\""))
                .ForMember(a => a.TitleEn, opt => opt.MapFrom(f => $"Arrived on \"{f.MovementDetailWithFlight.CarrierCode}{f.MovementDetailWithFlight.FlightNumber}/{f.MovementDetailWithFlight.Day}{f.MovementDetailWithFlight.Month}\""))
                ;

            CreateMap<StatusDetailsRcf, ConsignmentStatus>()
                .ForMember(a => a.StatusCode, opt => opt.MapFrom(f => "RCF"))
                .ForMember(a => a.Source, opt => opt.MapFrom(f => "FSU"))
                .ForMember(a => a.DateChange, opt => opt.MapFrom(f => DateTime.UtcNow))
                .ForMember(a => a.AirportCode, opt => opt.MapFrom(f => f.MovementDetailWithFlight.AirportCode))
                .ForMember(a => a.NumberOfPiece, opt => opt.MapFrom(f => int.Parse(f.QuantityDetail.NumberOfPieces)))
                .ForMember(a => a.Weight, opt => opt.MapFrom(f => int.Parse(f.QuantityDetail.Weight)))
                .ForMember(a => a.VolumeAmount, opt => opt.MapFrom(f => 0))
                .ForMember(a => a.TitleRu, opt => opt.MapFrom(f => $"Принято с рейса \"{f.MovementDetailWithFlight.CarrierCode}{f.MovementDetailWithFlight.FlightNumber}/{f.MovementDetailWithFlight.Day}{f.MovementDetailWithFlight.Month}\""))
                .ForMember(a => a.TitleEn, opt => opt.MapFrom(f => $"Received from flight \"{f.MovementDetailWithFlight.CarrierCode}{f.MovementDetailWithFlight.FlightNumber}/{f.MovementDetailWithFlight.Day}{f.MovementDetailWithFlight.Month}\""))
                ;

            CreateMap<StatusDetailsAwr, ConsignmentStatus>()
                .ForMember(a => a.StatusCode, opt => opt.MapFrom(f => "AWR"))
                .ForMember(a => a.Source, opt => opt.MapFrom(f => "FSU"))
                .ForMember(a => a.DateChange, opt => opt.MapFrom(f => DateTime.UtcNow))
                .ForMember(a => a.AirportCode, opt => opt.MapFrom(f => f.MovementDetailWithFlight.AirportCode))
                .ForMember(a => a.NumberOfPiece, opt => opt.MapFrom(f => int.Parse(f.QuantityDetail.NumberOfPieces)))
                .ForMember(a => a.Weight, opt => opt.MapFrom(f => int.Parse(f.QuantityDetail.Weight)))
                .ForMember(a => a.VolumeAmount, opt => opt.MapFrom(f => 0))
                .ForMember(a => a.TitleRu, opt => opt.MapFrom(f => $"Получены документы с рейса \"{f.MovementDetailWithFlight.CarrierCode}{f.MovementDetailWithFlight.FlightNumber}/{f.MovementDetailWithFlight.Day}{f.MovementDetailWithFlight.Month}\""))
                .ForMember(a => a.TitleEn, opt => opt.MapFrom(f => $"Documents recived from flight \"{f.MovementDetailWithFlight.CarrierCode}{f.MovementDetailWithFlight.FlightNumber}/{f.MovementDetailWithFlight.Day}{f.MovementDetailWithFlight.Month}\""))
                ;

            CreateMap<StatusDetailsNfd, ConsignmentStatus>()
                .ForMember(a => a.StatusCode, opt => opt.MapFrom(f => "NFD"))
                .ForMember(a => a.Source, opt => opt.MapFrom(f => "FSU"))
                .ForMember(a => a.DateChange, opt => opt.MapFrom(f => DateTime.UtcNow))
                .ForMember(a => a.AirportCode, opt => opt.MapFrom(f => f.MovementDetail.AirportCode))
                .ForMember(a => a.NumberOfPiece, opt => opt.MapFrom(f => int.Parse(f.QuantityDetail.NumberOfPieces)))
                .ForMember(a => a.Weight, opt => opt.MapFrom(f => int.Parse(f.QuantityDetail.Weight)))
                .ForMember(a => a.VolumeAmount, opt => opt.MapFrom(f => 0))
                .ForMember(a => a.TitleRu, opt => opt.MapFrom(f => $"Груз готов к выдаче, грузополучатель уведомлен"))
                .ForMember(a => a.TitleEn, opt => opt.MapFrom(f => $"Cargo is ready for delivery"))
                ;

            CreateMap<StatusDetailsAwd, ConsignmentStatus>()
                .ForMember(a => a.StatusCode, opt => opt.MapFrom(f => "AWD"))
                .ForMember(a => a.Source, opt => opt.MapFrom(f => "FSU"))
                .ForMember(a => a.DateChange, opt => opt.MapFrom(f => DateTime.UtcNow))
                .ForMember(a => a.AirportCode, opt => opt.MapFrom(f => f.MovementDetail.AirportCode))
                .ForMember(a => a.NumberOfPiece, opt => opt.MapFrom(f => int.Parse(f.QuantityDetail.NumberOfPieces)))
                .ForMember(a => a.Weight, opt => opt.MapFrom(f => int.Parse(f.QuantityDetail.Weight)))
                .ForMember(a => a.VolumeAmount, opt => opt.MapFrom(f => 0))
                .ForMember(a => a.TitleRu, opt => opt.MapFrom(f => $"Документы выданы грузополучателю"))
                .ForMember(a => a.TitleEn, opt => opt.MapFrom(f => $"Documents are provided to Consignee"))
                ;

            CreateMap<StatusDetailsDlv, ConsignmentStatus>()
                .ForMember(a => a.StatusCode, opt => opt.MapFrom(f => "DLV"))
                .ForMember(a => a.Source, opt => opt.MapFrom(f => "FSU"))
                .ForMember(a => a.DateChange, opt => opt.MapFrom(f => DateTime.UtcNow))
                .ForMember(a => a.AirportCode, opt => opt.MapFrom(f => f.MovementDetail.AirportCode))
                .ForMember(a => a.NumberOfPiece, opt => opt.MapFrom(f => int.Parse(f.QuantityDetail.NumberOfPieces)))
                .ForMember(a => a.Weight, opt => opt.MapFrom(f => int.Parse(f.QuantityDetail.Weight)))
                .ForMember(a => a.VolumeAmount, opt => opt.MapFrom(f => 0))
                .ForMember(a => a.TitleRu, opt => opt.MapFrom(f => $"Груз выдан грузополучателю"))
                .ForMember(a => a.TitleEn, opt => opt.MapFrom(f => $"Cargo is delivered to Consignee"))
                ;


            /* Это пока не нужно в Tracking переносить, просто
             * привязывать к сужествующей накладной
            CreateMap<StatusDetailsPre, ConsignmentStatus>();
            CreateMap<StatusDetailsTrm, ConsignmentStatus>();
            CreateMap<StatusDetailsTfd, ConsignmentStatus>();
            CreateMap<StatusDetailsTgc, ConsignmentStatus>();
            CreateMap<StatusDetailsCcd, ConsignmentStatus>();
            CreateMap<StatusDetailsDdl, ConsignmentStatus>();
            CreateMap<StatusDetailsDis, ConsignmentStatus>();
            CreateMap<StatusDetailsCrc, ConsignmentStatus>();
            CreateMap<StatusDetailsRct, ConsignmentStatus>();*/
        }
    }
}
