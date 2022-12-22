using AutoMapper;
using Cargo.Application.Automapper;
using Cargo.Infrastructure.Data.Model;
using IDeal.Common.Components.Messages.ObjectStructures.Ffas.Ver4;
using Xunit;

namespace Cargo.Tests
{
    public class FfaTests
    {
        IMapper mapper;
        public FfaTests()
        {
            var mapperCfg = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(BookingProfile).Assembly);
            });
            this.mapper = mapperCfg.CreateMapper();
        }
        #region AM
        [Fact]
        public void AwbEmptyTest()
        {
            //Arrange

            Awb awb = new Awb()
            { };
            Ffa ffa = mapper.Map<Ffa>(awb);
            //Act

            //Assert
            Assert.NotNull(ffa);
        }

        [Fact]
        public void AwbWithoutBookingTest()
        {
            //Arrange

            Awb awb = new Awb()
            {
                Id = 13,
                AcPrefix = "555",
                SerialNumber = "12345675",
                Origin = "UUS",
                Destination = "VVO",
                NumberOfPieces = 12,
                QuanDetShipmentDescriptionCode = "T",
                WeightCode = "K",
                Weight = 162.73M,
                VolumeCode = "MC",
                VolumeAmount = 0.742M,
                SpecialServiceRequest = "GENERAL",
                ManifestDescriptionOfGoods = "CAT",
                SpecialHandlingRequirements = "/AVI/EAW",

                // Currency// = "GENERAL",
            };
            Ffa ffa = mapper.Map<Ffa>(awb);
            //Act

            //Assert
            Assert.NotNull(ffa);
            Assert.Equal(awb.AcPrefix, ffa.ConsignmentDetail.AwbIdentification.AirlinePrefix);
            Assert.Equal(awb.SerialNumber, ffa.ConsignmentDetail.AwbIdentification.AwbSerialNumber);
            Assert.Equal(awb.Origin, ffa.ConsignmentDetail.AwbOriginAndDestination.AirportCodeOfOrigin);
            Assert.Equal(awb.Destination, ffa.ConsignmentDetail.AwbOriginAndDestination.AirportCodeOfDestitation);
            Assert.Equal(awb.QuanDetShipmentDescriptionCode, ffa.ConsignmentDetail.QuantityDetail.ShipmentDescriptionCode);
            Assert.Equal(awb.NumberOfPieces.ToString(), ffa.ConsignmentDetail.QuantityDetail.NumberOfPieces);
            Assert.Equal(awb.WeightCode, ffa.ConsignmentDetail.QuantityDetail.WeightCode);
            Assert.Equal(awb.Weight.ToString().Replace(',', '.'), ffa.ConsignmentDetail.QuantityDetail.Weight);
            Assert.Equal(awb.ManifestDescriptionOfGoods, ffa.ConsignmentDetail.NatureOfGoods.ManifestDescriptionOfGoods);
            Assert.Equal(awb.SpecialHandlingRequirements, string.Join(string.Empty, ffa.ConsignmentDetail.SpecialHandlingRequirements.Select(shr => $"/{shr.SpecialHandlingCode}")));

            Assert.Equal(awb.SpecialServiceRequest, ffa.SpecialServiceRequest.SsrDetails1.SpecialServiceRequestInner);



            Assert.NotNull(ffa.FlightDetails);
            //  Assert.True(ffa.FlightDetails.Any());
        }

        [Fact]
        public void AwbWithBookingTest()
        {
            //Arrange

            Awb awb = new Awb()
            {
                Id = 13,
                AcPrefix = "555",
                SerialNumber = "12345675",
                Bookings = new List<Booking>() {
                    new Booking{FlightSchedule = new FlightShedule{Number = "SU1234", Origin = "UUS", Destination = "VVO",FlightDate = new DateTime(2022,4,23) }, SpaceAllocationCode="UU" },
                    new Booking{FlightSchedule = new FlightShedule{Number = "SU034", Origin = "KHV", Destination = "OVB",FlightDate = new DateTime(2022,4,30) }, SpaceAllocationCode="HK" },
                }
            };
            // Currency// = "GENERAL",
            Ffa ffa = mapper.Map<Ffa>(awb);
            //Act

            //Assert
            Assert.NotNull(ffa);
            Assert.Equal(2, ffa.FlightDetails.Count);
        }
        #endregion
    }
}