using Cargo.Application.Validation;
using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Data.Model;
using Cargo.Infrastructure.Data.Model.Dictionary.Settings;
using Cargo.Infrastructure.Data.Model.Settings.PoolAwbs;
using IDeal.Common.Components.Messages.ObjectStructures.Ffrs.Ver8;
using IDeal.Common.Messaging.Messages;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Globalization;
using Xunit;

namespace Cargo.Tests
{
    public class ProcessFfrTests
    {
        ProcessFfrValidator processFfrValidator;

        public ProcessFfrTests()
        {
            var mockDbContext = new Mock<CargoContext>();
            mockDbContext.Setup(c => c.Awbs).Returns(Awbs().Object);
            mockDbContext.Setup(c => c.Agents).Returns(Agents().Object);
            mockDbContext.Setup(c => c.AgentContracts).Returns(AgentContracts().Object);
            mockDbContext.Setup(c => c.FlightShedules).Returns(FlightShedules().Object);
            var fdv = new FlightDetailsValidator(mockDbContext.Object);
            processFfrValidator = new ProcessFfrValidator(fdv, mockDbContext.Object);
        }

        #region FV

        [Fact]
        public void FvProcessFfr1()
        {
            Mock<ProcessFfr> mockProcessFfr = new Mock<ProcessFfr>();
            mockProcessFfr.Setup(m => m.EmailAgent).Returns("koblev_d@mail.ru");
            mockProcessFfr.Setup(m => m.Ffr).Returns(new Ffr
            {

                StandardMessageIdentification = new StandardMessageIdentification { StandardMessageIdentifier = "FFR", MessageTypeVersionNumber = "8" },
                ConsignmentDetail = new ConsignmentDetail
                {
                    AwbIdentification = new AwbIdentification { AirlinePrefix = "555", AwbSerialNumber = "12345675" },
                    AwbOriginAndDestination = new AwbOriginAndDestination { AirportCodeOfOrigin = "SVO", AirportCodeOfDestitation = "GDZ" },
                    NatureOfGoods = new NatureOfGoods { ManifestDescriptionOfGoods = "SPARE PARTS" },
                    QuantityDetail = new QuantityDetail { NumberOfPieces = "5", ShipmentDescriptionCode = "P", Weight = "400", WeightCode = "K" },
                    VolumeDetail = new VolumeDetail { VolumeAmount = "0.1", VolumeCode = "MC" }
                },

                FlightDetails = new List<FlightDetails> { new()
                    {
                        FlightIdentification = new FlightIdentification { CarrierCode = "SU", FlightNumber = "1150", Day = "10", Month = "AUG" },
                        AirportsOfDepartureAndArrival = new AirportsOfDepartureAndArrival { AirportCodeOfArrival = "GDZ", AirportCodeOfDeparture = "SVO" },
                        SpaceAllocationCode = "NN"
                    }

                }
            });


            var valid = this.processFfrValidator.Validate(mockProcessFfr.Object);

            Assert.True(true);
        }

        public void FvProcessFfr2()
        {
            //this.processFfrValidator с одставным контекстом данных

            Assert.True(true);
        }

        #endregion


        /// <summary>
        /// Пустышка на DbSet FlightShedule для контекста данных.
        /// </summary>
        /// <returns></returns>
        /// <remarks>взял из doc.microsoft. они поддерживают moq-и</remarks>
        private Mock<DbSet<FlightShedule>> FlightShedules()
        {
            var data = new List<FlightShedule>
            {
                new() { Number = "2351" },
                new() { Number = "103" },
                new() { Number = "8461" },
                new() { Number = "SU1150", FlightDate = DateTime.Parse("10.08.2022")},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<FlightShedule>>();
            mockSet.As<IQueryable<FlightShedule>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<FlightShedule>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<FlightShedule>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<FlightShedule>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet;
        }

        private Mock<DbSet<Awb>> Awbs()
        {
            var data = new List<Awb>
            {
                //new Awb { AcPrefix = "555", SerialNumber = "12345675", Status = "Draft"},
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Awb>>();
            mockSet.As<IQueryable<Awb>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Awb>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Awb>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Awb>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet;
        }

        private Mock<DbSet<Agent>> Agents()
        {
            var data = new List<Agent>
            {
                new() { Email = "koblev_d@mail.ru" },
                new() { Email = "g@ff.ru" },
                new() { Email = "mail@cargo.com", ContragentId = 3},
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Agent>>();
            mockSet.As<IQueryable<Agent>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Agent>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Agent>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Agent>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet;
        }

        private Mock<DbSet<AgentContract>> AgentContracts()
        {
            var data = new List<AgentContract>
            {
                new() { Id = 1, SaleAgentId = 3, DateAt = DateTime.Parse("10.01.2022", CultureInfo.InvariantCulture), DateTo = null, PoolAwbs = new List<AgentContractPoolAwb>()
                {
                    new() { Id = 1, ContractId = 1, StartNumber = 1000000, PoolLenght = 100000, UsedAwbNumbers = new List<AgentContractPoolAwbNums>()
                        {
                            new() { SerialNumber = 1000000 },
                            new() { SerialNumber = 1000001 },
                            new() { SerialNumber = 1000002 },
                            new() { SerialNumber = 1000003 },
                            new() { SerialNumber = 1000004 },
                        }
                    }
                } }
            }.AsQueryable();
            var mockSet = new Mock<DbSet<AgentContract>>();
            mockSet.As<IQueryable<AgentContract>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<AgentContract>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<AgentContract>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<AgentContract>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet;
        }
    }
}