using Cargo.Contract.DTOs;
using Cargo.Contract.DTOs.Bookings;
using Cargo.Infrastructure.Report;
using Xunit;

namespace Cargo.Tests
{
    public class AwbPdfTest
    {
        AwbDtoExtension awbDtoExtension;
        public AwbPdfTest()
        {
            awbDtoExtension = new AwbDtoExtension();
        }

        [Fact]
        public void FullAwbPdfTest()
        {
            var bookingList = new List<BookingDto>();
            var booking = new BookingDto()
            {
                Seq = 0,
                QuanDetShipmentDescriptionCode = "",
                NumberOfPieces = 1,
                WeightCode = "K",
                Weight = (decimal)100.00000,
                VolumeCode = "MC",
                VolumeAmount = (decimal)1.00000,
                CreatedDate = new DateTime(),
                SpaceAllocationCode = "KK",
                FlightSchedule = new FlightSheduleDto()
                {
                    Origin = "KJA",
                    Destination = "LED",
                    Number = "SU006",
                    FlightDate = new DateTime(1993, 5, 3, 1, 2, 3),
                },

                PrevRouting = null,
                NextRoutings = null,
                AwbOrigin = "Московский офис 64",
                AwbDestination = ""
            };

            bookingList.Add(booking);

            AwbDto awbDto = new AwbDto()
            {
                AcPrefix = "555",
                SerialNumber = "10000804",
                PoolAwbNumId = 2,
                ForwardingAgentId = 1,
                ForwardingAgent = "Agent forwarding",
                Origin = "KJA",
                OriginInfo = new IataLocationDto()
                {
                    Code = "KJA",
                    Name = "Yemelyanovo Airport",
                    RussianName = "Емельяново",
                    CityName = "Krasnoyarsk",
                },
                Destination = "LED",
                DestinationInfo = new IataLocationDto()
                {
                    Code = "LED",
                    Name = "Pulkovo Airport",
                    RussianName = "Пулково",
                    CityName = "St. Petersburg",
                },
                DiIndicator = "",
                QuanDetShipmentDescriptionCode = "T",
                NumberOfPieces = 1,
                WeightCode = "K",
                Weight = (decimal)100.00000,
                ChargeWeight = (decimal)50.00000,
                VolumeCode = "MC",
                VolumeAmount = (decimal)1.00000,
                ManifestDescriptionOfGoods = "TELEVIZOR",
                ManifestDescriptionOfGoodsRu = "ТЕЛЕВИЗОР",
                SpecialHandlingRequirements = "/BIG",
                Product = "100",
                Status = "Booked",
                SpecialServiceRequest = "OBRASHCHATSIA OSTOROZHNO",
                SpecialServiceRequestRu = "ОБРАЩАТЬСЯ ОСТОРОЖНО",
                PlaceOfIssue = "Московский офис 64",
                NCV = "NCV",
                NDV = "NDV",
                Currency = "RUR",
                Bookings = bookingList,
                BookingRcs = bookingList,
                CreatedDate = new DateTime(1993, 5, 3, 1, 2, 3),
                Consignor = new AwbContragentDto()
                {
                    NameRu = "ХОЗЯИН",
                    NameEn = "OWNER",
                    NameExRu = "ПЕСИКА",
                    NameExEn = "DOGGIE",
                    CityRu = "ПЕТЕРБУРГ",
                    CityEn = "PETERSBURG",
                    CountryISO = "RU",
                    ZipCode = "7898775",
                    Passport = "1256321546",
                    RegionRu = "Санкт-Петербург",
                    RegionEn = "Sankt-Peterburg",
                    CodeEn = "812",
                    Phone = "9854414194",
                    Fax = "1111",
                    AddressRu = "Московский пр-т., 86",
                    AddressEn = "Moscow str. 86",
                    PreviewRu = "Предварительный просмотр",
                    PreviewEn = "Preview",
                    Email = "smoltock81@gmail.com",
                    IataCode = "SR22",
                    AgentCass = "DARK FANTASI",
                    CountryISOInfo = new CountryDto() { EnglishShortName = "Russia" }
                },
                Consignee = new AwbContragentDto()
                {
                    NameRu = "ХОЗЯИН",
                    NameEn = "OWNER",
                    NameExRu = "ПЕСИКА",
                    NameExEn = "DOGGIE",
                    CityRu = "ПЕТЕРБУРГ",
                    CityEn = "PETERSBURG",
                    CountryISO = "RU",
                    ZipCode = "7898775",
                    Passport = "1256321546",
                    RegionRu = "Санкт-Петербург",
                    RegionEn = "Sankt-Peterburg",
                    CodeEn = "812",
                    Phone = "9854414194",
                    Fax = "1111",
                    AddressRu = "Московский пр-т., 86",
                    AddressEn = "Moscow str. 86",
                    PreviewRu = "Предварительный просмотр",
                    PreviewEn = "Preview",
                    Email = "smoltock81@gmail.com",
                    IataCode = "SR22",
                    AgentCass = "DARK FANTASI",
                    CountryISOInfo = new CountryDto() { EnglishShortName = "Russia" }
                },
                Agent = new AwbContragentDto()
                {
                    NameRu = "ХОЗЯИН",
                    NameEn = "OWNER",
                    NameExRu = "ПЕСИКА",
                    NameExEn = "DOGGIE",
                    CityRu = "ПЕТЕРБУРГ",
                    CityEn = "PETERSBURG",
                    CountryISO = "RU",
                    ZipCode = "7898775",
                    Passport = "1256321546",
                    RegionRu = "Санкт-Петербург",
                    RegionEn = "Sankt-Peterburg",
                    CodeEn = "812",
                    Phone = "9854414194",
                    Fax = "1111",
                    AddressRu = "Московский пр-т., 86",
                    AddressEn = "Moscow str. 86",
                    PreviewRu = "Предварительный просмотр",
                    PreviewEn = "Preview",
                    Email = "smoltock81@gmail.com",
                    IataCode = "SR22",
                    AgentCass = "DARK FANTASI",
                    CountryISOInfo = new CountryDto() { EnglishShortName = "Russia" }
                },
                Carrier = new CustomerDto()
                {
                    Id = 0,
                    IataCode = "SU",
                    AcPrefix = "555",
                    AcMailPrefix = "800",
                    Name = "Aeroflot",
                    Email = "system-su@i-deal.tech",
                },
                SizeGroups = null,
                TariffsSolutionCode = "",
                SalesChannel = "",
                PaymentProcedure = "",
                WeightCharge = "",
                AllIn = false,
                TariffClass = "TariffClass",
                BaseTariffRate = (decimal)5.00000,
                AddOn = "AddOn",
                TariffRate = (decimal)50.00000,
                Total = (decimal)50.00000,
                Charge = new TotalChargeDto()
                {
                    Id = 1,
                    Collect = new TaxChargeDto()
                    {
                        Id = 1,
                        AwbId = 4,
                        Charge = (decimal)2250.00000,
                        ValuationCharge = (decimal)333.00000,
                        Tax = (decimal)34.00000,
                        TotalOtherChargesDueAgent = (decimal)777.00000,
                        TotalOtherChargesDueCarrier = (decimal)674.00000,
                        Total = (decimal)122.00000
                    },
                    Prepaid = new TaxChargeDto()
                    {
                        Id = 1,
                        AwbId = 4,
                        Charge = (decimal)2250.00000,
                        ValuationCharge = (decimal)333.00000,
                        Tax = (decimal)34.00000,
                        TotalOtherChargesDueAgent = (decimal)777.00000,
                        TotalOtherChargesDueCarrier = (decimal)674.00000,
                        Total = (decimal)122.00000
                    }
                },
                OtherCharges = null,
                Messages = null,
                History = null,
            };
            byte[] res = awbDtoExtension.ToPdf(awbDto, "D:\\Working\\Git\\cargo\\CargoService\\Cargo.ServiceHost\\");
            string base64String = Convert.ToBase64String(res, 0, res.Length);
        }
    }
}
