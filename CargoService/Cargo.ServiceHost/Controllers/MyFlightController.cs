using Cargo.Application.Services;
using Cargo.Infrastructure.Data.Model.Settings.MyFlights;
using IDeal.Common.Components.Paginator;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cargo.ServiceHost.Controllers;

[Route("Api/[controller]/V1")]
[ApiController]
public class MyFlightController : ControllerBase, IMyFlightsService
{
    private MyFlightsService _myFlightsService;

    public MyFlightController(MyFlightsService myFlightsService)
    {
        _myFlightsService = myFlightsService;
    }

    [HttpPost(nameof(Get))]
    public async Task<PagedResult<MyFlight>> Get(PageInfo _pageInfo)
    {
        return await _myFlightsService.Get(_pageInfo);
    }

    [HttpPost(nameof(CreateOrUpdateMyFlights))]
    public async Task<bool> CreateOrUpdateMyFlights(MyFlight myFlightDto)
    {
        return await _myFlightsService.CreateOrUpdateMyFlights(myFlightDto);
    }

    [HttpGet(nameof(MyFlightById))]
    public async Task<MyFlight> MyFlightById(int myFlightId)
    {
        return await _myFlightsService.MyFlightById(myFlightId);
    }
}