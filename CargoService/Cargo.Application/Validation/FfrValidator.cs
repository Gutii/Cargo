using Cargo.Infrastructure.Data;
using FluentValidation;
using IDeal.Common.Components.Messages.ObjectStructures.Ffrs.Ver8;
using IDeal.Common.Messaging.Messages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;

namespace Cargo.Application.Validation
{
    public class ProcessFfrValidator : AbstractValidator<ProcessFfr>
    {
        private CargoContext dbContext;
        public ProcessFfrValidator(FlightDetailsValidator flightDelVal, CargoContext dbContext)
        {
            this.dbContext = dbContext;

            RuleFor(x => x.EmailAgent).NotNull().EmailAddress().Must(IsEmailAgentAddress).WithMessage("INVALID SENDING ADRESS"); //Проверка адреса отправителя FFR.
            RuleFor(x => x.Ffr.ConsignmentDetail.VolumeDetail).Must(CheckVolumeDetail).WithMessage("INVALID VOLUME"); //Проверка наличия объема.

            When(x => IsExistAwb(x.Ffr.ConsignmentDetail.AwbIdentification), () => //Если накладная существует.
            {
                RuleFor(x => x.Ffr.ConsignmentDetail.AwbIdentification).Must(CheckStatusAwb).WithMessage("CARGO STATUS RCS"); //Проверка cтатуса накладной.
                RuleFor(x => x.Ffr).Must(CheсkRouteForAwb).WithMessage("INVALID ROUTE"); //Проверка полноты маршрута для существующей накладной.
            }).Otherwise(() => //Если накладная новая
            {
                RuleFor(x => x.Ffr.ConsignmentDetail.AwbIdentification.AwbSerialNumber).NotNull().NotEmpty().Must(ValidAwbNumber).WithMessage("INVALID AWB NUMBER");//Проверка корректности номера AWB.
                RuleFor(x => x).Must(CheckPoolAwb).WithMessage("INVALID AWB NUMBER IN POOL"); //Проверка номера awb в пуле агента
                RuleFor(x => x.Ffr).Must(CheсkRouteForNewAwb).WithMessage("INVALID ROUTE"); //Проверка полноты маршрута для новой накладной.
            });

            RuleFor(x => x.Ffr.ConsignmentDetail.QuantityDetail.ShipmentDescriptionCode).Must(sdс => sdс == "T").WithMessage("ALLOWED TO BOOKING ONLY FULL SHIPMENTS"); //Бронирование только полных партий.
            RuleForEach(pf => pf.Ffr.FlightDetails).SetValidator(flightDelVal); // Проверка рейсов для бронирования.
        }

        public bool CheckPoolAwb(ProcessFfr pf) //Проверка номера awb в пуле агента
        {
            var agentId = dbContext.Agents.AsNoTracking().Where(email => email.Email == pf.EmailAgent).AsNoTracking().FirstOrDefault()!.ContragentId; //Поиск AgentId
            var pools = this.dbContext.AgentContracts.AsNoTracking()
                .Include(ac => ac.PoolAwbs).ThenInclude(ac => ac.UsedAwbNumbers)
                .Where(ac => ac.SaleAgentId == agentId && ac.DateAt <= DateTime.UtcNow.Date && (!ac.DateTo.HasValue || ac.DateTo.Value >= DateTime.UtcNow.Date))
                .SelectMany(ac => ac.PoolAwbs).ToList();

            var expectedNum = int.Parse(pf.Ffr.ConsignmentDetail.AwbIdentification.AwbSerialNumber.Substring(0, 7));
            var poolWithFreeNumber = pools.FirstOrDefault(p => p.StartNumber <= expectedNum && expectedNum <= p.StartNumber + p.PoolLenght && !p.UsedAwbNumbers.Select(uan => uan.SerialNumber).Contains(expectedNum));
            if (poolWithFreeNumber != null) return true;
            return false;
        }
        private bool CheckVolumeDetail(VolumeDetail vd) //Проверка наличия объема.
        {
            if (vd != null) return Convert.ToDecimal(vd.VolumeAmount.Replace('.', ',')) > 0 & vd.VolumeCode == "MC";
            return false;
        }
        private bool IsEmailAgentAddress(string address)//Проверка сушествования адреса у агента.
        {
            return dbContext.Agents.AsNoTracking().Any(ea => ea.Email == address);
        }
        private static bool ValidAwbNumber(string num)//Проверка корректности номера AWB.
        {
            var number = Convert.ToInt32(num.Substring(0, 7));
            var check = Convert.ToInt32(num.Substring(7, 1));
            Math.DivRem(number, 7, out var result);
            return result.Equals(check);
        }
        private bool IsExistAwb(AwbIdentification ai) //Проверка существования накладной.
        {
            return dbContext.Awbs.AsNoTracking().Any(awb => awb.AcPrefix == ai.AirlinePrefix &
                                             awb.SerialNumber == ai.AwbSerialNumber);
        }
        private bool CheckStatusAwb(AwbIdentification ai) //Проверка cтатуса накладной.
        {
            return dbContext.Awbs.AsNoTracking().Any(awb => awb.AcPrefix == ai.AirlinePrefix &
                                             awb.SerialNumber == ai.AwbSerialNumber &
                                             awb.Status != "RCS" & awb.Status != "Validated");
        }
        private bool CheсkRouteForNewAwb(Ffr ffr)//Проверка полноты маршрута для новой накладной.
        {
            var ro = ffr.FlightDetails[0].AirportsOfDepartureAndArrival.AirportCodeOfDeparture;
            var rd = ffr.FlightDetails[^1].AirportsOfDepartureAndArrival.AirportCodeOfArrival;
            var result = ffr.ConsignmentDetail.AwbOriginAndDestination.AirportCodeOfOrigin == ro &&
                       ffr.ConsignmentDetail.AwbOriginAndDestination.AirportCodeOfDestitation == rd;
            return result;
        }
        private bool CheсkRouteForAwb(Ffr ffr)//Проверка полноты маршрута для существующей накладной.
        {
            var rd = ffr.FlightDetails[^1].AirportsOfDepartureAndArrival.AirportCodeOfArrival;
            var result = ffr.ConsignmentDetail.AwbOriginAndDestination.AirportCodeOfDestitation == rd;
            return result;
        }
    }


    public class FlightDetailsValidator : AbstractValidator<FlightDetails>
    {
        private CargoContext dbContext;
        public FlightDetailsValidator(CargoContext dbContext)
        {
            this.dbContext = dbContext;
            RuleFor(x => x.FlightIdentification).NotNull().Must(FiRuleDescr).WithMessage("INVALID FLIGHT");
            RuleFor(x => x.SpaceAllocationCode).NotNull().WithMessage("SKIPPED ACTION CODE").Must(ValidSpaceAllocationCode).WithMessage("INVALID ACTION CODE");
        }
        private bool FiRuleDescr(FlightIdentification fi) //Проверка наличия рейса в расписании.
        {
            return dbContext.FlightShedules.AsNoTracking().Any(fs => fs.Number == fi.GetFlightNumber() && fs.FlightDate.Date == fi.GetFlightDate().Date);
        }
        private bool ValidSpaceAllocationCode(string code) //Проверка кода бронирования
        {
            string[] correctCodes = { "NN", "NA", "XX", "CA" };
            return correctCodes.Contains(code);
        }
    }


    internal static class FlightIdentificationExt
    {
        public static DateTime GetFlightDate(this FlightIdentification fi)
        {
            var dt = (fi.Day ?? "01") + (fi.Month ?? "JAN") + DateTime.Now.Year;
            return DateTime.ParseExact(dt, "ddMMMyyyy", new CultureInfo("en-US"));
        }
        public static string GetFlightNumber(this FlightIdentification fi)
        {
            return fi.CarrierCode + fi.FlightNumber;
        }

    }
}
