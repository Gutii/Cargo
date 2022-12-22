using Cargo.Infrastructure.Data;
using IDeal.Common.Messaging.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Cargo.Application.CommandHandlers
{
    public class ProcessCgoConsumer : IConsumer<ProcessCgo>
    {
        ILogger<ProcessCgoConsumer> logger;
        CargoContext dbContext;

        public ProcessCgoConsumer(ILogger<ProcessCgoConsumer> logger, CargoContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<ProcessCgo> context)
        {
            int day = int.Parse(context.Message.Cgo.FlightInformation.Day);
            int month = DateTime.ParseExact(context.Message.Cgo.FlightInformation.Month, "MMM", new CultureInfo("en-US")).Month;
            int year = DateTime.UtcNow.Year;
            DateTime date = new DateTime(year, month, day);
            var flight = this.dbContext.FlightShedules.FirstOrDefault(f => f.Number == string.Concat(context.Message.Cgo.FlightInformation.CarrierCode, context.Message.Cgo.FlightInformation.FlightNumber) && f.FlightDate.Date == date.Date);
            if (flight != null)
            {
                double pv;
                if (double.TryParse(context.Message.Cgo.QuantityDetail?.Wgt2?.Replace('.', ','), out pv))
                {
                    flight.PayloadWeight = pv;
                }
                if (double.TryParse(context.Message.Cgo.QuantityDetail?.Vol2?.Replace('.', ','), out pv))
                {
                    flight.PayloadVolume = pv;
                }
                this.dbContext.SaveChanges();
            }
        }
    }
}
