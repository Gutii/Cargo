using AutoMapper;
using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Data.Model;
using IDeal.Common.Components;
using IDeal.Common.Messaging.Shedule;
using IdealResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Cargo.Application.Services
{
    public class ScheduleService
    {
        IMapper mapper;
        CargoContext dbContext;
        private TelegrammService _telegrammService;
        public ScheduleService(CargoContext dbContext, IMapper mapper, TelegrammService telegrammService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            _telegrammService = telegrammService;
        }

        public async Task<Result<FlightShedule>> Flight(string flightNumber, string day, string month = null)
        {
            try
            {
                if (string.IsNullOrEmpty(flightNumber) || string.IsNullOrEmpty(day))
                {
                    return Result.Invalid("Отсутствует обязательные к заполнению flightNumber, day");
                }

                DateTime flightDate = ParseFlightDate(day, month);

                FlightShedule flight = await this.dbContext.FlightShedules
                 .AsNoTracking()
                 .FirstOrDefaultAsync(f => f.Number == flightNumber && f.FlightDate.Date == flightDate);

                if (flight == null)
                {
                    return Result.Invalid($"Рейс не найден: {flightNumber}/{day}{month}");
                }

                return Result.Ok(flight);
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error($"Не удалось извлечь рейс {flightNumber}/{day}{month}").CausedBy(ex));
            }
        }

        public async Task<Result<FlightShedule>> Flight(string flightNumber, DateTime flightDate)
        {
            try
            {
                if (string.IsNullOrEmpty(flightNumber))
                {
                    return Result.Invalid("Отсутствует обязательные к заполнению flightNumber");
                }

                FlightShedule flight = await this.dbContext.FlightShedules
                 .AsNoTracking()
                 .FirstOrDefaultAsync(f => f.Number == flightNumber && f.FlightDate.Date == flightDate);

                if (flight == null)
                {
                    return Result.Invalid($"Рейс не найден: {flightNumber}/{flightDate.ToString("MMM", new CultureInfo("en-US")).ToUpper()}");
                }

                return Result.Ok(flight);
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error($"Не удалось извлечь рейс {flightNumber}/{flightDate.ToString("MMM", new CultureInfo("en-US")).ToUpper()}").CausedBy(ex));
            }
        }

        public static DateTime ParseFlightDate(string sDay, string sMonth = null)
        {
            int day = int.Parse(sDay);
            DateTime d = DateTime.Today;
            if (!string.IsNullOrEmpty(sMonth))
            {
                d = DateTime.ParseExact($"{day}{sMonth}", "dMMM", new CultureInfo("en-US"));
                DateTime today = DateTime.Now;
                int m = Math.Abs(((today.Year - d.Year) * 12) + today.Month - d.Month);
                if (m > 5)
                {
                    d = d.AddYears(1);
                }
            }

            if (d == DateTime.Today)
            {
                int month = DateTime.Today.Month;
                int year = DateTime.Today.Year;
                if (Math.Abs(day - DateTime.Today.Day) > 15)
                {
                    month++;
                    if (month > 12)
                    {
                        month = 1;
                        year++;
                    }
                }

                d = new DateTime(year, month, day);
            }

            return d;
        }

        public async Task FlightChanged(FlightSheduleChanged flightScheduleChanged)
        {
            try
            {
                FlightShedule flight =
                    await dbContext.FlightShedules.FirstOrDefaultAsync(x => x.ExternalId == flightScheduleChanged.Id);
                bool isNew = flight == null;
                flight = mapper.Map(flightScheduleChanged, flight);
                if (isNew)
                    dbContext.FlightShedules.Add(flight);

                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                await _telegrammService.SendError(e);
            }
        }

        public async Task<Result<FlightShedule>> FlightStateOpenClose(ulong Id)
        {
            var task = Task.Run(() =>
            {
                var flightShedule = dbContext.FlightShedules.Find(Id);

                if (flightShedule == null)
                    return Result.Fail("invalid id");

                if (flightShedule.SaleState == FlightSaleState.Open)
                {
                    flightShedule.SaleState = FlightSaleState.Closed;
                }
                else
                {
                    flightShedule.SaleState = FlightSaleState.Open;
                }

                dbContext.FlightShedules.Update(flightShedule);
                dbContext.SaveChanges();

                return Result.Ok(flightShedule);
            });

            return await task;
        }
    }
}
