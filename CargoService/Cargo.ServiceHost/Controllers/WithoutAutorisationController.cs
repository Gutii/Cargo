using Cargo.Application.Services;
using Cargo.Contract.DTOs;
using IdealResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cargo.ServiceHost.Controllers;

[Route("Api/[controller]/V1")]
[ApiController]
public class WithoutAutorisationController : ControllerBase
{
    private readonly ScheduleService scheduleService;
    private readonly FblService fblService;
    private readonly FsaService fsaService;

    public WithoutAutorisationController(ScheduleService scheduleService, FblService fblService, FsaService fsaService)
    {
        this.scheduleService = scheduleService;
        this.fblService = fblService;
        this.fsaService = fsaService;
    }

    /// <summary>
    /// Изменение рейса в расписании
    /// </summary>
    /// <param name="flightSheduleChanged"></param>
    [HttpPost(nameof(FlightSсheduleChange))]
    public async Task<Result> FlightSсheduleChange([FromBody] FlightScheduleMessage flightSheduleChanged)
    {
        await scheduleService.FlightChanged(flightSheduleChanged);
        return Result.Ok();
    }

    /// <summary>
    /// Запускает отправку FBL телекса
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet(nameof(SendFbl))]
    public async Task<Result> SendFbl(ulong id)
    {
        await fblService.SendFbl(id);
        return Result.Ok();
    }

    /// <summary>
    /// Запускает отправку FSA(BKD) телекса
    /// </summary>
    /// <param name="awbId">Если null, будет найден по awbIdentifier</param>
    /// <param name="awbIdentifier">Если null awbId обязательно</param>
    /// <param name="email"> Если null будет отправка только по адресу из TelexSettings</param>
    /// <returns></returns>
    [HttpGet(nameof(SendFsaBkd))]
    public async Task<Result> SendFsaBkd(int? awbId = null, string awbIdentifier = null, string email = null)
    {
        await fsaService.SendFsaBkd(awbId, awbIdentifier, email);
        return Result.Ok();
    }
}