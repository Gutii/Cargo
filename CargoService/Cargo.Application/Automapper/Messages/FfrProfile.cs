using AutoMapper;
using Cargo.Application.Services;
using Cargo.Infrastructure.Data.Model;
using IDeal.Common.Components.Messages.ObjectStructures.Ffrs.Ver8;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cargo.Application.Automapper.Messages
{
    public class FfrProfile : Profile
    {
        public FfrProfile()
        {
            this.Ffr2Model();
        }

        /// <summary>
        /// маппинг на  модель
        /// </summary>
        private void Ffr2Model()
        {
            CreateMap<Ffr, Awb>()
                .ForMember(awb => awb.Status, opt => opt.MapFrom(ffr => "Draft"))
                .ForMember(awb => awb.CreatedDate, opt => opt.MapFrom(ffr => DateTime.UtcNow))
                .ForMember(awb => awb.AcPrefix, opt => opt.MapFrom(ffr => ffr.ConsignmentDetail.AwbIdentification.AirlinePrefix))
                .ForMember(awb => awb.SerialNumber, opt => opt.MapFrom(ffr => ffr.ConsignmentDetail.AwbIdentification.AwbSerialNumber))
                .ForMember(awb => awb.Origin, opt => opt.MapFrom(ffr => ffr.ConsignmentDetail.AwbOriginAndDestination.AirportCodeOfOrigin))
                .ForMember(awb => awb.Destination, opt => opt.MapFrom(ffr => ffr.ConsignmentDetail.AwbOriginAndDestination.AirportCodeOfDestitation))
                .ForMember(awb => awb.NumberOfPieces, opt => opt.MapFrom(ffr => ffr.ConsignmentDetail.QuantityDetail.ShipmentDescriptionCode == "P" ? Convert.ToInt32(ffr.ConsignmentDetail.TotalConsignmentPieces.NumberOfPieces) : Convert.ToInt32(ffr.ConsignmentDetail.QuantityDetail.NumberOfPieces)))
                .ForMember(awb => awb.QuanDetShipmentDescriptionCode, opt => opt.MapFrom(ffr => ffr.ConsignmentDetail.QuantityDetail.ShipmentDescriptionCode))
                .ForMember(awb => awb.Weight, opt => opt.MapFrom(ffr => Convert.ToDecimal(ffr.ConsignmentDetail.QuantityDetail.Weight.Replace('.', ','))))
                .ForMember(awb => awb.WeightCode, opt => opt.MapFrom(ffr => ffr.ConsignmentDetail.QuantityDetail.WeightCode))
                .ForMember(awb => awb.ManifestDescriptionOfGoods, opt => opt.MapFrom(ffr => ffr.ConsignmentDetail.NatureOfGoods.ManifestDescriptionOfGoods))
                .ForMember(awb => awb.SpecialHandlingRequirements, opt =>
                {
                    opt.Condition(ffr => ffr.ConsignmentDetail?.SpecialHandlingRequirements != null);
                    opt.MapFrom(ffr => string.Join(string.Empty, ffr.ConsignmentDetail.SpecialHandlingRequirements.Select(shl => $"/{shl.SpecialHandlingCode}")));
                })
                .ForMember(awb => awb.SpecialServiceRequest, opt => opt.MapFrom(ffr => ffr.SpecialServiceRequest.SsrDetails1.SpecialServiceRequestInner + ", " + ffr.SpecialServiceRequest.SsrDetails2.SpecialServiceRequestInner))
                .ForMember(awb => awb.Bookings, opt => opt.MapFrom(opt => opt.FlightDetails))

                // ---- В FFR данные по объему могут отсутствовать. ----
                .ForMember(awb => awb.VolumeAmount, opt => opt.MapFrom(ffr => ffr.ConsignmentDetail.VolumeDetail.VolumeAmount != null ? Convert.ToDecimal(ffr.ConsignmentDetail.VolumeDetail.VolumeAmount.Replace('.', ',')) : 0))
                .ForMember(awb => awb.VolumeCode, opt => opt.MapFrom(ffr => "MC" /*ffr.ConsignmentDetail.VolumeDetail.VolumeCode*/))

                //---??? Не понятно откуда брать обязательные к заполнению данные для AWB. В FFR этого нет. ---
                .ForMember(awb => awb.ChargeWeight, opt => opt.MapFrom(ffr => Convert.ToDecimal(ffr.ConsignmentDetail.QuantityDetail.Weight.Replace('.', ','))))
                .ForMember(awb => awb.ManifestDescriptionOfGoodsRu, opt => opt.MapFrom(ffr => string.Empty))
                .ForMember(awb => awb.AllIn, opt => opt.MapFrom(ffr => 0))
                .ForMember(awb => awb.BaseTariffRate, opt => opt.MapFrom(ffr => 0))
                .ForMember(awb => awb.TariffRate, opt => opt.MapFrom(ffr => 0))
                .ForMember(awb => awb.Total, opt => opt.MapFrom(ffr => 0))
                .ForMember(awb => awb.PoolAwbId, opt => opt.Ignore())
                .AfterMap(async (ffr, awb) =>
                {
                    foreach (Booking b in awb.Bookings)
                    {
                        b.NumberOfPieces = Convert.ToInt32(ffr.ConsignmentDetail.QuantityDetail.NumberOfPieces);
                        b.Weight = Convert.ToDecimal(ffr.ConsignmentDetail.QuantityDetail.Weight.Replace('.', ','));
                        b.WeightCode = ffr.ConsignmentDetail.QuantityDetail.WeightCode;
                        b.VolumeAmount = ffr.ConsignmentDetail.VolumeDetail.VolumeAmount != null ? Convert.ToDecimal(ffr.ConsignmentDetail.VolumeDetail.VolumeAmount.Replace('.', ',')) : 0;
                        b.VolumeCode = "MC" /*ffr.ConsignmentDetail.VolumeDetail.VolumeCode*/;
                        b.QuanDetShipmentDescriptionCode = ffr.ConsignmentDetail.QuantityDetail.ShipmentDescriptionCode;
                        b.FlightScheduleId = b.FlightSchedule.Id;
                    }
                })
                ;

            //CreateMap<ConsignmentDetail, BookingDto>()
            //    .ForMember(b => b.CreatedDate, opt => opt.MapFrom(cd => DateTime.UtcNow))
            //    .ForMember(b => b.NumberOfPieces, opt => opt.MapFrom(cd => Convert.ToInt32(cd.QuantityDetail.NumberOfPieces)))
            //    .ForMember(b => b.Weight, opt => opt.MapFrom(cd => Convert.ToDecimal(cd.QuantityDetail.Weight.Replace('.', ','))))
            //    .ForMember(b => b.WeightCode, opt => opt.MapFrom(cd => cd.QuantityDetail.WeightCode))
            //    .ForMember(b => b.VolumeAmount, opt => opt.MapFrom(cd => cd.VolumeDetail.VolumeAmount != null ? Convert.ToDecimal(cd.VolumeDetail.VolumeAmount.Replace('.', ',')) : 0))
            //    .ForMember(b => b.VolumeCode, opt => opt.MapFrom(ffr => "MC" /*ffr.ConsignmentDetail.VolumeDetail.VolumeCode*/))
            //    .ForMember(b => b.QuanDetShipmentDescriptionCode, opt => opt.MapFrom(cd => cd.QuantityDetail.ShipmentDescriptionCode))
            //    ;

            CreateMap<FlightDetails, Booking>()
                .ForMember(b => b.CreatedDate, opt => opt.MapFrom(fd => DateTime.UtcNow))
                .ForMember(b => b.SpaceAllocationCode, opt => opt.MapFrom(fd => fd.SpaceAllocationCode))
                .ForMember(b => b.FlightSchedule, opt => opt.MapFrom(fd => fd))
                ;

            CreateMap<FlightDetails, FlightShedule>().ConvertUsing<FlightDetails2FlightSheduleConverter>();
            ;
        }
    }

    public class FlightDetails2FlightSheduleConverter : ITypeConverter<FlightDetails, FlightShedule>
    {
        ScheduleService scheduleService;

        public FlightDetails2FlightSheduleConverter(ScheduleService scheduleService)
        {
            this.scheduleService = scheduleService;
        }

        public FlightShedule Convert(FlightDetails source, FlightShedule destination, ResolutionContext context)
        {
            string flightNumber = source.FlightIdentification.CarrierCode + source.FlightIdentification.FlightNumber;
            DateTime flightDate = ScheduleService.ParseFlightDate(source.FlightIdentification.Day, source.FlightIdentification.Month);
            var task = Task.Run(async () => await this.scheduleService.Flight(flightNumber, flightDate));
            var result = task.Result;
            if (result.IsSuccess)
            {
                return result.Value;
            }

            return default;
        }
    }
}
