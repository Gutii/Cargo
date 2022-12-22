using Cargo.Infrastructure.Data;
using FluentValidation;
using IDeal.Common.Components.Messages.ObjectStructures.Fbrs.Ver2;
using IDeal.Common.Messaging.Messages;
using System;
using System.Globalization;
using System.Linq;

namespace Cargo.Application.Validation
{
    public class ProcessFbrValidator : AbstractValidator<ProcessFbr>
    {
        private CargoContext dbContext;
        public ProcessFbrValidator(CargoContext dbContext)
        {
            this.dbContext = dbContext;

            RuleFor(x => x.Fbr.FlightInformation.FlightIdentification).NotNull().Must(CheckFlight).WithMessage("INVALID FLIGHT"); //Проверка рейса FBR.
        }

        private bool CheckFlight(FlightIdentification fi) //Проверка существования рейса.
        {
            return dbContext.FlightShedules.Any(fs => fs.Number == this.GetFlightNumber(fi) && fs.FlightDate.Date == this.GetFlightDate(fi));
        }

        private DateTime GetFlightDate(FlightIdentification fi)
        {
            var dt = (fi.Day ?? "01") + (fi.Month ?? "JAN") + DateTime.Now.Year;
            return DateTime.ParseExact(dt, "ddMMMyyyy", new CultureInfo("en-US"));
        }
        private string GetFlightNumber(FlightIdentification fi)
        {
            return fi.CarrierCode + fi.FlightNumber;
        }
    }
}
