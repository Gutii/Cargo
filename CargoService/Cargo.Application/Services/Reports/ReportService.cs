using Cargo.Infrastructure.Data;
using IDeal.Common.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cargo.Application.Services.Reports;

/// <summary>
/// Сервис отчетов
/// </summary>
public class ReportService
{
    private readonly CargoContext dbContext;
    private readonly SettingsService commPayloaderService;
    public ReportService(CargoContext dbContext, SettingsService commPayloaderService)
    {
        this.dbContext = dbContext;
        this.commPayloaderService = commPayloaderService;
    }

    /// <summary>
    /// 14.2.1.	Отчет «Общее бронирование на рейсах»
    /// </summary>
    /// <param name="beginDate"></param>
    /// <param name="endDate"></param>
    /// <param name="agentId"></param>
    /// <param name="carrierId"></param>
    /// <param name="lang"></param>
    /// <returns></returns>
    public async Task<byte[]> BookingOnFlights(DateTime beginDate, DateTime endDate, int? agentId, int? carrierId, string lang)
    {
        var result = from flight in dbContext.FlightShedules
                     join booking in dbContext.Bookings on flight.Id equals booking.FlightScheduleId
                     join awb in dbContext.Awbs on booking.AwbId equals awb.Id
                     where flight.FlightDate >= beginDate && flight.FlightDate <= endDate
                     where flight.SaleState != FlightSaleState.Closed
                     where awb.AgentId == null || awb.AgentId == agentId
                     where awb.CarrierId == null || awb.CarrierId == carrierId
                     where booking.SpaceAllocationCode == "KK"
                     select new ReportBookingOnFlightsDto
                     {
                         FlightOrigin = flight.Origin,
                         FlightDestination = flight.Destination,
                         FlightDate = flight.FlightDate,
                         FlightStDestination = flight.StDestination,
                         FlightNumber = flight.Number,
                         FlightAircraftType = flight.AircraftType
                     };

        result.AsNoTracking().ToList().ForEach(async x =>
        {
            var bookings = dbContext.Bookings
                .Where(booking => booking.FlightScheduleId == x.FlightId && booking.SpaceAllocationCode == "KK")
                .AsNoTracking().ToList();
            var awbs = bookings.Select(awb => awb.Awb).ToList();
            var planCommPayload = await commPayloaderService.FindPayload(x.FlightAircraftType, x.FlightDate);
            x.WeightFact = (double)awbs.Sum(w => w.Weight);
            x.VolumeFact = (double)awbs.Sum(v => v.VolumeAmount);
            x.WeightPlan = (double)planCommPayload.Value.Weight;
            x.VolumePlan = (double)planCommPayload.Value.Volume;
        });

        return await Task.FromResult(result.AsNoTracking().ToList().ToExcelArrayBytes(lang));
    }

    /// <summary>
    /// 14.2.2.	Отчет «Бронирование на рейсах по авианакладным»
    /// </summary>
    /// <param name="beginDate"></param>
    /// <param name="endDate"></param>
    /// <param name="agentId"></param>
    /// <param name="carrierId"></param>
    /// <param name="lang"></param>
    /// <returns></returns>
    public async Task<byte[]> BookingsPerPeriod(DateTime beginDate, DateTime endDate, int? agentId, int? carrierId, string lang)
    {
        var result = from awb in dbContext.Awbs
                     join agent in dbContext.Contragents on awb.Id equals agent.Id
                     join booking in dbContext.Bookings on awb.Id equals booking.AwbId
                     join flight in dbContext.FlightShedules on booking.FlightScheduleId equals flight.Id
                     where awb.AgentId == null || awb.AgentId == agentId
                     where awb.CarrierId == null || awb.CarrierId == carrierId
                     where flight.FlightDate >= beginDate && flight.FlightDate <= endDate
                     where flight.SaleState != FlightSaleState.Closed
                     select new ReportBookingsPerPeriodDto
                     {
                         AwbNumder = $"{awb.AcPrefix}-{awb.SerialNumber}",
                         AwbOrigin = awb.Origin,
                         AwbDestination = awb.Destination,
                         AwbNumberOfPieces = awb.NumberOfPieces,
                         AwbWeight = awb.Weight,
                         AwbVolume = awb.VolumeAmount,
                         AwbAgentId = awb.AgentId,
                         AwbAgent = agent.InternationalName,
                         BookingFlightSceduleId = booking.FlightScheduleId,
                         BookingNumberOfPieces = booking.NumberOfPieces,
                         BookingWeight = booking.Weight,
                         BookingVolume = booking.VolumeAmount,
                         BookingSpaceAllocationCode = booking.SpaceAllocationCode,
                         FlightNumber = flight.Number,
                         FlightDate = flight.FlightDate,
                         FlightOrigin = flight.Origin,
                         FlightDestination = flight.Destination
                     };

        return await Task.FromResult(result.AsNoTracking().ToList().ToExcelArrayBytes(lang));
    }
}