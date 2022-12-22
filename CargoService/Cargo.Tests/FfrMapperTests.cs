using AutoMapper;
using Cargo.Application.Automapper;
using Cargo.Contract.DTOs.Bookings;
using IDeal.Common.Components.Messages.ObjectStructures.Ffrs.Ver8;
using Xunit;

namespace Cargo.Tests
{
    public class FfrMapperTests
    {
        IMapper mapper;
        public FfrMapperTests()
        {
            var mapperCfg = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(BookingProfile).Assembly);
            });
            this.mapper = mapperCfg.CreateMapper();
        }
        #region AM
        [Fact]
        public void FfrEmptyTest()
        {
            //Arrange

            Ffr ffr = new Ffr()
            { };
            AwbDto awb = mapper.Map<AwbDto>(ffr);
            //Act

            //Assert
            Assert.NotNull(awb);
        }

        [Fact]
        public void FullFfrTest()
        {
            //Arrange
            Ffr ffr = new Ffr
            {
                StandardMessageIdentification = new StandardMessageIdentification
                {
                    StandardMessageIdentifier = "FFR",
                    MessageTypeVersionNumber = "8"
                },
                ConsignmentDetail = new ConsignmentDetail
                {
                    AwbIdentification = new AwbIdentification
                    {
                        AirlinePrefix = "555",
                        AwbSerialNumber = "12345675"
                    },
                    AwbOriginAndDestination = new AwbOriginAndDestination
                    {
                        AirportCodeOfOrigin = "SVO",
                        AirportCodeOfDestitation = "VVO"
                    },
                    QuantityDetail = new QuantityDetail
                    {
                        ShipmentDescriptionCode = "T",
                        NumberOfPieces = "56",
                        WeightCode = "K",
                        Weight = "4545.45"
                    },
                    VolumeDetail = new VolumeDetail
                    {
                        VolumeCode = "MC",
                        VolumeAmount = "232323.45"
                    },
                    NatureOfGoods = new NatureOfGoods
                    {
                        ManifestDescriptionOfGoods = "BIKE PARTS"
                    },
                    SpecialHandlingRequirements = new List<SpecialHandlingRequirements> {
                        new SpecialHandlingRequirements{SpecialHandlingCode = "AVI" },
                        new SpecialHandlingRequirements{SpecialHandlingCode = "TRE" },
                        new SpecialHandlingRequirements{SpecialHandlingCode = "HTS" }
                    }
                },
                FlightDetails = new List<FlightDetails> {
                    new FlightDetails {
                        FlightIdentification = new FlightIdentification {
                            CarrierCode = "SU",
                            FlightNumber = "4324",
                            Day = "08",
                            Month = "MAY"
                        },
                        AirportsOfDepartureAndArrival = new AirportsOfDepartureAndArrival{
                            AirportCodeOfDeparture = "SVO",
                            AirportCodeOfArrival = "KJA"
                        },
                        SpaceAllocationCode = "KK"
                    },
                    new FlightDetails {
                        FlightIdentification = new FlightIdentification {
                            CarrierCode = "SU",
                            FlightNumber = "024",
                            Day = "09",
                            Month = "MAY"
                        },
                        AirportsOfDepartureAndArrival = new AirportsOfDepartureAndArrival{
                            AirportCodeOfDeparture = "KJA",
                            AirportCodeOfArrival = "KHV"
                        },
                        SpaceAllocationCode = "XA"
                    },
                    new FlightDetails {
                        FlightIdentification = new FlightIdentification {
                            CarrierCode = "SU",
                            FlightNumber = "094",
                            Day = "09",
                            Month = "MAY"
                        },
                        AirportsOfDepartureAndArrival = new AirportsOfDepartureAndArrival{
                            AirportCodeOfDeparture = "KHV",
                            AirportCodeOfArrival = "VVO"
                        },
                        SpaceAllocationCode = "NN"
                    }
                }
            };
            AwbDto awb = mapper.Map<AwbDto>(ffr);
            //Act

            //Assert
            Assert.NotNull(awb);
            Assert.Equal(3, awb.Bookings?.Count);
            Assert.Equal("555", awb.AcPrefix);
            Assert.Equal("12345675", awb.SerialNumber);
            Assert.Equal("/AVI/TRE/HTS", awb.SpecialHandlingRequirements);
            Assert.Equal(56, awb.Bookings?.Skip(1)?.FirstOrDefault()?.NumberOfPieces);
            //  Assert.True(ffa.FlightDetails.Any());
        }

        #endregion
    }
}