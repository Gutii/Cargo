using AutoMapper;
using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Data.Model.Settings.MyFlights;
using IDeal.Common.Components.Paginator;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.Services;

public class MyFlightsService : IMyFlightsService
{
    private CargoContext _cargoContext;
    private TelegrammService _telegrammService;
    IMapper mapper;

    public MyFlightsService(CargoContext cargoContext, IMapper mapper, TelegrammService telegrammService)
    {
        _cargoContext = cargoContext;
        this.mapper = mapper;
        _telegrammService = telegrammService;
    }

    public async Task<PagedResult<MyFlight>> Get(PageInfo pageInfo)
    {
        return await _cargoContext
           .MyFlights
           .Include(mf => mf.MyFlightsNumbers)
           .Include(mf => mf.MyFlightsRoutes)
           .Join(_cargoContext.Carriers, flight => flight.CarrierId, customer => customer.Id, (flight, customer) => new MyFlight
           {
               Agreement = flight.Agreement,
               Carrier = customer,
               Id = flight.Id,
               CarrierId = flight.CarrierId,
               DateAt = flight.DateAt,
               DateTo = flight.DateTo,
               MyFlightsNumbers = flight.MyFlightsNumbers,
               MyFlightsRoutes = flight.MyFlightsRoutes
           })
           .AsNoTracking()
           .PageAsync(pageInfo, CancellationToken.None);
    }

    public async Task<bool> CreateOrUpdateMyFlights(MyFlight myFlightDto)
    {

        try
        {
            var dbMyFlight = _cargoContext.MyFlights
                .Include(mf => mf.MyFlightsNumbers)
                .Include(mf => mf.MyFlightsRoutes)
                .FirstOrDefault(mf => mf.Id == myFlightDto.Id);
            if (dbMyFlight != null)
            {
                mapper.Map(myFlightDto, dbMyFlight);
            }
            else
            {
                _cargoContext.MyFlights.Add(myFlightDto);
            }
            await _cargoContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            await _telegrammService.SendError(e);
            return false;
        }
    }



    public async Task<MyFlight> MyFlightById(int myFlightId)
    {
        return await _cargoContext.MyFlights
            .Include(il => il.MyFlightsNumbers)
            .Include(il => il.MyFlightsRoutes)
            .Join(_cargoContext.Carriers, flight => flight.CarrierId, customer => customer.Id, (flight, customer) => new MyFlight
            {
                Agreement = flight.Agreement,
                Carrier = customer,
                Id = flight.Id,
                CarrierId = flight.CarrierId,
                DateAt = flight.DateAt,
                DateTo = flight.DateTo,
                MyFlightsNumbers = flight.MyFlightsNumbers,
                MyFlightsRoutes = flight.MyFlightsRoutes
            })
            .AsNoTracking()
            .FirstOrDefaultAsync(il => il.Id == myFlightId) ?? new MyFlight { Id = -1, Agreement = "MyFlight failed" };
    }
}

public interface IMyFlightsService
{
    /// <summary>
    /// Список авиакомпаний с рейсами в управлении
    /// </summary>
    /// <returns>Список авиакомпаний</returns>
    Task<PagedResult<MyFlight>> Get(PageInfo _pageInfo);

    /// <summary>
    /// Create/Update MyFlight
    /// </summary>
    /// <param name="myFlightDto">MyFlight</param>
    Task<bool> CreateOrUpdateMyFlights(MyFlight myFlightDto);


    /// <summary>
    /// Получение рейсов в управлении по Id
    /// </summary>
    /// <param name="searchQuery"></param>
    /// <returns></returns>
    Task<MyFlight> MyFlightById(int myFlightId);
}