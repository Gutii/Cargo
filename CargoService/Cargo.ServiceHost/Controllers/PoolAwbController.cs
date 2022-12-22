using Cargo.Application.Services.PoolAwbNum;
using Cargo.Infrastructure.Data.Model.PoolAwbService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cargo.ServiceHost.Controllers;

[Route("Api/[controller]/V1")]
[ApiController]
public class PoolAwbController : ControllerBase, IPoolAwbService
{
    private PoolAwbService _poolAwbService { get; set; }

    public PoolAwbController(PoolAwbService poolAwbService)
        => _poolAwbService = poolAwbService;

    /// <inheritdoc />
    [HttpGet(nameof(GetAllPools))]
    public Task<List<PoolAwb>> GetAllPools(int page = 0, int limit = 1000)
        => _poolAwbService.GetAllPools(page, limit);

    /// <inheritdoc />
    [HttpGet(nameof(ReservePool))]
    public Task<ReservePoolAwb> ReservePool(int poolLen, int agentId, int prefix, int beginNum = 0)
        => _poolAwbService.ReservePool(poolLen, agentId, prefix, beginNum);

    /// <inheritdoc />
    [HttpGet(nameof(ReserveNumber))]
    public Task<ReserveNumberAwb> ReserveNumber(int agentId, int prefix, int beginNum = 0)
        => _poolAwbService.ReserveNumber(agentId, prefix, beginNum);
}