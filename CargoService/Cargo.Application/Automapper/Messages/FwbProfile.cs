using AutoMapper;
using Cargo.Infrastructure.Data.Model;
using Cargo.Infrastructure.Data.Model.Dictionary;
using IDeal.Common.Components.Messages.ObjectStructures.Fwbs.Ver17;
using System;
using System.Collections.Generic;
using System.Linq;
using Agent = IDeal.Common.Components.Messages.ObjectStructures.Fwbs.Ver17.Agent;
using ContactDetail = IDeal.Common.Components.Messages.ObjectStructures.Fwbs.Ver17.ContactDetail;

namespace Cargo.Application.Automapper.Messages
{
    public class FwbProfile : Profile
    {
        public FwbProfile()
        {
            this.CreateMap<Awb, Fwb>(MemberList.None)
                .ForMember(dst => dst.StandardMessageIdentification, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.AwbConsignmentDetail, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.FlightBookings, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.Routing, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.Consignee, mf => mf.MapFrom(src => src.Consignee))
                .ForMember(dst => dst.Agent, mf => mf.MapFrom(src => src.Agent))
                .ForMember(dst => dst.Shipper, mf => mf.MapFrom(src => src.Consignor))
                .ForMember(dst => dst.SpecialServiceRequest, mf => mf.MapFrom(src => src))
                //.ForMember(dst => dst.AlsoNotify, mf => mf.MapFrom(src => src))
                //.ForMember(dst => dst.ChargeDeclarations, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.OtherCharges, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.PrepaidChargeSummary, mf => mf.MapFrom(src => src.Prepaid))
                .ForMember(dst => dst.CollectChargeSummary, mf => mf.MapFrom(src => src.Collect))
                .ForMember(dst => dst.RateDescription, mf => mf.MapFrom(src => src))
                //.ForMember(dst => dst.ShippersCertification, mf => mf.MapFrom(src => src))
                ;
            /*
                
                .ForMember(dst => dst.Shipper                             , mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.SpecialServiceRequest               , mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.AlsoNotify                          , mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.AccountingInformation               , mf => mf.MapFrom(src => src))


                .ForMember(dst => dst.OtherCharges                        , mf => mf.MapFrom(src => src))


                .ForMember(dst => dst.CarriersExecution                   , mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.OtherServiceInformation             , mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.CcChargesInDestinationCurrency      , mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.SenderReference                     , mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.CustomsOrigin                       , mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.CommissionInformation               , mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.SalesIncentiveInformation           , mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.AgentReferenceData                  , mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.SpecialHandlingDetails              , mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.NominatedHandlingParty              , mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.ShipmentReferenceInformation        , mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.OtherParticipantInformation         , mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.OtherCustSecurityAndRegulatCtrlInfo , mf => mf.MapFrom(src => src))
            */

            this.CreateMap<Awb, StandardMessageIdentification>()
                .ForMember(dst => dst.StandardMessageIdentifier, opt => opt.MapFrom(c => "FWB"))
                .ForMember(dst => dst.MessageTypeVersionNumber, opt => opt.MapFrom(c => "17"))
            ;
            this.CreateMap<Awb, AwbConsignmentDetail>()
                .ForMember(dst => dst.AwbIdentification, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.AwbOriginAndDestination, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.QuantityDetail, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.VolumeDetail, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.DensityGroup, mf => mf.MapFrom(src => src))
                ;
            this.CreateMap<Awb, AwbIdentification>()
                .ForMember(dst => dst.AirlinePrefix, mf => mf.MapFrom(src => src.AcPrefix))
                .ForMember(dst => dst.AwbSerialNumber, mf => mf.MapFrom(src => src.SerialNumber))
                ;
            this.CreateMap<Awb, AwbOriginAndDestination>()
                .ForMember(dst => dst.AirportCodeOfDestitation, mf => mf.MapFrom(src => src.Destination))
                .ForMember(dst => dst.AirportCodeOfOrigin, mf => mf.MapFrom(src => src.Origin))
                ;
            this.CreateMap<Awb, QuantityDetail>()
                .ForMember(dst => dst.ShipmentDescriptionCode, mf => mf.MapFrom(src => src.QuanDetShipmentDescriptionCode))
                .ForMember(dst => dst.NumberOfPieces, mf => mf.MapFrom(src => src.NumberOfPieces))
                .ForMember(dst => dst.Weight, mf => mf.MapFrom(src => $"{Math.Round(src.Weight, 3):G29}".Replace(',', '.')))
                .ForMember(dst => dst.WeightCode, mf => mf.MapFrom(src => src.WeightCode))
                ;
            this.CreateMap<Awb, VolumeDetail>()
                .ForMember(dst => dst.VolumeAmount, mf => mf.MapFrom(src => $"{Math.Round(src.VolumeAmount, 3):G29}".Replace(',', '.')))
                .ForMember(dst => dst.VolumeCode, mf => mf.MapFrom(src => src.VolumeCode))
                ;
            this.CreateMap<Awb, DensityGroup>()
                //.ForMember(dst => dst.DensityGroupInner,mf=> mf.MapFrom(src => src.))
                //.ForMember(dst => dst.DensityIndicator,mf=> mf.MapFrom(src => src.))
                ;
            this.CreateMap<Awb, FlightBookings>()
                .ForMember(dst => dst.LineIdentifier, mf => mf.MapFrom(c => "FLT"))
                .ForMember(dst => dst.FlightIdentification, mf => mf.MapFrom(src => src.Bookings.ToList()))
                ;

            this.CreateMap<Booking, FlightIdentification>()
                .ForMember(dst => dst.FlightNumber, mf => mf.MapFrom(src => src.FlightSchedule.Number.Substring(2)))
                .ForMember(dst => dst.CarrierCode, mf => mf.MapFrom(src => src.FlightSchedule.Number.Substring(0, 2)))
                .ForMember(dst => dst.Day, mf => mf.MapFrom(src => src.FlightSchedule.FlightDate.ToString("dd")))
                ;
            /*this.CreateMap<FlightShedule, FlightIdentification>()
                .ForMember(dst => dst.FlightNumber, mf => mf.MapFrom(src => src.Number))
                .ForMember(dst => dst.CarrierCode, mf => mf.MapFrom(src => src.Number.Substring(0,2)))
                .ForMember(dst => dst.Day, mf => mf.MapFrom(src => src.FlightDate.Day))
                ;*/

            this.CreateMap<Awb, Routing>()
                .ForMember(dst => dst.LineIdentifier, mf => mf.MapFrom(c => "RTG"))
                .ForMember(dst => dst.FirstDestinationCarrier, mf => mf.MapFrom(src => src.Bookings.OrderByDescending(o => o.FlightSchedule.FlightDate).Select(s => s.FlightSchedule).FirstOrDefault()))

                ;

            this.CreateMap<FlightShedule, FirstDestinationCarrier>()
                .ForMember(dst => dst.CarrierCode, mf => mf.MapFrom(src => src.Number.Substring(0, 2)))
                .ForMember(dst => dst.AirportCode, mf => mf.MapFrom(src => src.Origin))
                ;
            /* this.CreateMap<FlightShedule, OnwardDestinationCarrier>()
                 .ForMember(dst => dst.CarrierCode, mf => mf.MapFrom(src => src.Number.Substring(0, 2)))
                 .ForMember(dst => dst.AirportCode, mf => mf.MapFrom(src => src.Origin))
                 ;*/


            this.CreateMap<AwbContragent, Consignee>()
                .ForMember(dst => dst.LineIdentifier, mf => mf.MapFrom(c => "CNE"))
                .ForMember(dst => dst.AccountDetail, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.Name, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.StreetAddress, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.Location, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.CodedLocation, mf => mf.MapFrom(src => src))
                ;

            this.CreateMap<AwbContragent, Location>()
                .ForMember(dst => dst.PlaceInner, mf => mf.MapFrom(src => src.CityEn.ToUpper()))
                .ForMember(dst => dst.StateProvince, mf => mf.MapFrom(src => src.RegionEn.ToUpper()))
                ;
            this.CreateMap<AwbContragent, CodedLocation>()
                .ForMember(dst => dst.IsoCountryCode, mf => mf.MapFrom(src => src.CountryISO))
                .ForMember(dst => dst.PostCode, mf => mf.MapFrom(src => src.ZipCode))
                ;
            this.CreateMap<AwbContragent, AccountDetail>()
                .ForMember(dst => dst.AccountNumber, mf => mf.MapFrom(c => c.AgentCass))
                ;
            this.CreateMap<AwbContragent, Name>()
                .ForMember(dst => dst.NameDetail, mf => mf.MapFrom(src => new List<NameDetail>
                {
                    new NameDetail {NameInner = src.NameEn.ToUpper()},
                    //new NameDetail {NameInner = src.NameExEn}
                    //new NameDetail {NameInner = src.NameRu},
                    //new NameDetail {NameInner = src.NameExRu}
                }));
            this.CreateMap<AwbContragent, StreetAddress>()
                .ForMember(dst => dst.StreetAddressDetail, mf => mf.MapFrom(src => new List<StreetAddressDetail>
                {
                    //new StreetAddressDetail { StreetAddressInner = src.AddressRu },
                    new StreetAddressDetail { StreetAddressInner = src.AddressEn }
                }));

            /////////// AGENT ////////////////////
            this.CreateMap<Contragent, Agent>()
                .ForMember(dst => dst.LineIdentifier, mf => mf.MapFrom(src => "AGT"))
                .ForMember(dst => dst.AccountDetailAgt, mf => mf.MapFrom(src => src.SalesAgent.FirstOrDefault()))
                .ForMember(dst => dst.Place, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.NameDetail, mf => mf.MapFrom(src => src))
                ;
            this.CreateMap<Infrastructure.Data.Model.Dictionary.Settings.Agent, AccountDetailAgt>()
                .ForMember(dst => dst.AccountNumber, mf => mf.MapFrom(src => src.Contragent.AccountNumber))
                .ForMember(dst => dst.CargoAgentCassAddress, mf => mf.MapFrom(src => src.IataCargoAgentCassAddress))
                .ForMember(dst => dst.IataCargoAgentNumericCode, mf => mf.MapFrom(src => src.IataCargoAgentNumericCode))
                .ForMember(dst => dst.ParticipantIdentifier, mf => mf.MapFrom(src => src.ParticipantIdentifier))
                ;

            this.CreateMap<Contragent, Place>()
                .ForMember(dst => dst.PlaceInner, mf => mf.MapFrom(src => src.Place.ToUpper()))
                ;
            this.CreateMap<Contragent, NameDetail>()
                .ForMember(dst => dst.NameInner, mf => mf.MapFrom(src => src.InternationalName.ToUpper()))
                ;
            /////////// SHIPPER ////////////////////
            this.CreateMap<AwbContragent, Shipper>()
                .ForMember(dst => dst.LineIdentifier, mf => mf.MapFrom(c => "SHP"))
                .ForMember(dst => dst.AccountDetail, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.CodedLocation, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.Location, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.Name, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.StreetAddress, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.ContactDetail, mf => mf.MapFrom(src => new List<ContactDetail>
                {
                    new ContactDetail { ContactIdentifier = "TEL", ContactNumber = src.Phone},
                    new ContactDetail { ContactIdentifier = "FAX", ContactNumber = src.Email}
                }))
                ;

            /////////// SpecialServiceRequest ////////////////////
            this.CreateMap<Awb, SpecialServiceRequest>()
                .ForMember(dst => dst.LineIdentifier, mf => mf.MapFrom(c => "SSR"))
                .ForMember(dst => dst.SsrDetail, mf => mf.MapFrom(src => new List<SsrDetail>
                {
                    new SsrDetail { SpecialServiceRequestInner = src.SpecialServiceRequest }
                }))
                ;
            /////////// Charge Declarations ////////////////////
            this.CreateMap<Awb, ChargeDeclarations>()
                .ForMember(dst => dst.LineIdentifier, mf => mf.MapFrom(c => "CVD"))
                .ForMember(dst => dst.IsoCurrencyCode, mf => mf.MapFrom(src => src.Currency))
                .ForMember(dst => dst.PrepaidCollectChargeDeclarations, mf => mf.MapFrom(src => src.Prepaid))
                ;

            this.CreateMap<TaxCharge, PrepaidCollectChargeDeclarations>()
                .ForMember(dst => dst.PcIndWeightValuation, mf => mf.MapFrom(src => src.ValuationCharge))


                ;





            /////////// Rate Description ////////////////////
            this.CreateMap<Awb, RateDescription>()
                .ForMember(dst => dst.LineIdentifier, mf => mf.MapFrom(c => "RTD"))
                .ForMember(dst => dst.ChargeLineCount, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.NumberOfPiecesRcpDetails, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.GrossWeightDetails, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.RateClassDetails, mf => mf.MapFrom(src => src))
                //.ForMember(dst => dst.CommodityItemNumberDetails, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.ChargeableWeightDetails, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.RateChargeDetails, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.TotalDetails, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.GoodsDescriptionOrConsolidation, mf => mf.MapFrom(src => src))
                //.ForMember(dst => dst.ElementsDetail, mf => mf.MapFrom(src => src))
                ;



            this.CreateMap<Awb, ChargeLineCount>()
                .ForMember(dst => dst.AwbRateLineNumber, mf => mf.MapFrom(c => "1"))
                ;

            this.CreateMap<Awb, NumberOfPiecesRcpDetails>()
                .ForMember(dst => dst.AwbColumnIdentifier, mf => mf.MapFrom(c => "P"))
                .ForMember(dst => dst.NumberOfPieces, mf => mf.MapFrom(src => src.NumberOfPieces))
                //.ForMember(dst => dst.RateCombinationPoint, mf => mf.MapFrom(src => src.))
                ;
            this.CreateMap<Awb, GrossWeightDetails>()
                .ForMember(dst => dst.Weight, mf => mf.MapFrom(src => $"{Math.Round(src.Weight, 3):G29}".Replace(',', '.')))
                .ForMember(dst => dst.WeightCode, mf => mf.MapFrom(src => src.WeightCode))
                ;
            this.CreateMap<Awb, RateClassDetails>()
                .ForMember(dst => dst.AwbColumnIdentifier, mf => mf.MapFrom(c => "C"))
                .ForMember(dst => dst.RateClassCode, mf => mf.MapFrom(src => src.TariffClass))
                ;
            this.CreateMap<Awb, ChargeableWeightDetails>()
                .ForMember(dst => dst.AwbColumnIdentifier, mf => mf.MapFrom(c => "W"))
                .ForMember(dst => dst.Weight, mf => mf.MapFrom(src => $"{Math.Round(src.ChargeWeight, 3):G29}".Replace(',', '.')))
                ;
            this.CreateMap<Awb, RateChargeDetails>()
                .ForMember(dst => dst.AwbColumnIdentifier, mf => mf.MapFrom(c => "R"))
                .ForMember(dst => dst.RateOrCharge, mf => mf.MapFrom(src => $"{Math.Round(src.TariffRate, 3):G29}".Replace(',', '.')))
                ;
            this.CreateMap<Awb, TotalDetails>()
                .ForMember(dst => dst.AwbColumnIdentifier, mf => mf.MapFrom(c => "T"))
                .ForMember(dst => dst.ChargeAmount, mf => mf.MapFrom(src => $"{Math.Round(src.Total, 3):G29}".Replace(',', '.')))
                //.ForMember(dst => dst.Discount, mf => mf.MapFrom(src => src.TariffClass))
                ;
            this.CreateMap<Awb, GoodsDescriptionOrConsolidation>()
                .ForMember(dst => dst.AwbColumnIdentifier, mf => mf.MapFrom(c => "N"))
                .ForMember(dst => dst.GoodsDataIdentifier, mf => mf.MapFrom(c => "G"))
                .ForMember(dst => dst.NatureAndQuantityOfGoods, mf => mf.MapFrom(src => src.ManifestDescriptionOfGoods))
                ;

            /////////// ALSO NOTIFY ////////////////////


            /////////// Other charges ////////////////////
            this.CreateMap<Awb, OtherCharges>()
                .ForMember(dst => dst.LineIdentifier, mf => mf.MapFrom(c => "OTH"))
                .ForMember(dst => dst.OtherChargeDetail, mf => mf.MapFrom(src => src.OtherCharges))
                ;
            this.CreateMap<OtherCharge, OtherChargeDetail>()
                .ForMember(dst => dst.ChargeLine, mf =>
                {
                    mf.PreCondition(s => s.Collect > 0 || s.Prepaid > 0);
                    mf.MapFrom(src => new ChargeLine { PcIndOtherCharges = src.Collect > 0 ? "C" : "P" });
                })
                .ForMember(dst => dst.OtherChargeItems, mf =>
                {
                    mf.PreCondition(s => s.Collect > 0 || s.Prepaid > 0);
                    mf.MapFrom(src => new List<OtherChargeItems>
                    {
                        new OtherChargeItems
                        {
                            ChargeAmount = src.Collect > 0 ? $"{Math.Round(src.Collect, 2)}".Replace(',', '.') : $"{Math.Round(src.Prepaid, 2):G29}".Replace(',', '.'),
                            EntitlementCode = src.CA,
                            OtherChargeCode = src.TypeCharge
                        }
                    });
                })
                /*.ForMember(dst => dst.OtherChargeItems, mf => mf.MapFrom(src => new List<OtherChargeItems>
                {
                    new OtherChargeItems {
                        ChargeAmount =  src.Collect > 0 ? $"{Math.Round(src.Collect, 2)}".Replace(',', '.') : $"{Math.Round(src.Prepaid, 2):G29}".Replace(',', '.'),
                        EntitlementCode = src.CA,
                        OtherChargeCode =src.TypeCharge
                    }
                }))*/
                ;


            /////////// Prepaid Charge Summary ////////////////////
            this.CreateMap<TaxCharge, PrepaidChargeSummary>()
                .ForMember(dst => dst.LineIdentifier, mf => mf.MapFrom(c => "PPD"))
                .ForMember(dst => dst.TotalWeightCharge, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.ValuationCharge, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.Taxes, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.TotalOtherChargesDueAgent, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.TotalOtherChargesDueCarrier, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.ChargeSummaryTotal, mf => mf.MapFrom(src => src))
                ;
            this.CreateMap<TaxCharge, CollectChargeSummary>()
                .ForMember(dst => dst.LineIdentifier, mf => mf.MapFrom(c => "COL"))
                .ForMember(dst => dst.TotalWeightCharge, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.ValuationCharge, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.Taxes, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.TotalOtherChargesDueAgent, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.TotalOtherChargesDueCarrier, mf => mf.MapFrom(src => src))
                .ForMember(dst => dst.ChargeSummaryTotal, mf => mf.MapFrom(src => src))
                ;


            this.CreateMap<TaxCharge, TotalWeightCharge>()
                .ForMember(dst => dst.ChargeIdentifier, mf => mf.MapFrom(c => "WT"))
                .ForMember(dst => dst.ChargeAmount, mf => mf.MapFrom(src => $"{Math.Round(src.Charge, 2):G29}".Replace(',', '.')))
                ;
            this.CreateMap<TaxCharge, ValuationCharge>()
                .ForMember(dst => dst.ChargeIdentifier, mf => mf.MapFrom(c => "VC"))
                .ForMember(dst => dst.ChargeAmount, mf => mf.MapFrom(src => $"{Math.Round(src.ValuationCharge, 2):G29}".Replace(',', '.')))
                ;
            this.CreateMap<TaxCharge, Taxes>()
                .ForMember(dst => dst.ChargeIdentifier, mf => mf.MapFrom(c => "TX"))
                .ForMember(dst => dst.ChargeAmount, mf => mf.MapFrom(src => $"{Math.Round(src.Tax, 2):G29}".Replace(',', '.')))
                ;
            this.CreateMap<TaxCharge, TotalOtherChargesDueAgent>()
                .ForMember(dst => dst.ChargeIdentifier, mf => mf.MapFrom(c => "OA"))
                .ForMember(dst => dst.ChargeAmount, mf => mf.MapFrom(src => $"{Math.Round(src.TotalOtherChargesDueAgent, 2):G29}".Replace(',', '.')))
                ;
            this.CreateMap<TaxCharge, TotalOtherChargesDueCarrier>()
                .ForMember(dst => dst.ChargeIdentifier, mf => mf.MapFrom(c => "OC"))
                .ForMember(dst => dst.ChargeAmount, mf => mf.MapFrom(src => $"{Math.Round(src.TotalOtherChargesDueCarrier, 2):G29}".Replace(',', '.')))
                ;
            this.CreateMap<TaxCharge, ChargeSummaryTotal>()
                .ForMember(dst => dst.ChargeIdentifier, mf => mf.MapFrom(c => "CT"))
                .ForMember(dst => dst.ChargeAmount, mf => mf.MapFrom(src => $"{Math.Round(src.Total, 2):G29}".Replace(',', '.')))
                ;

            /////////// Shippers Certification ////////////////////
            /*this.CreateMap<Awb, ShippersCertification>()
                .ForMember(dst => dst.LineIdentifier, mf => mf.MapFrom(c => "CER"))
                .ForMember(dst => dst.Signature, mf => mf.MapFrom(c => "SHPR CERTIFICATE"))
                ;*/
        }
    }
}

