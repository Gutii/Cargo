#nullable enable
using Cargo.Infrastructure.Data.Model.PoolAwbService;
using IDeal.Common.Components;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Cargo.Application.Services.PoolAwbNum;

public class PoolAwbService : IPoolAwbService
{
    private readonly RequestClient _client;

    public PoolAwbService(IOptions<TarantoolSettings> tarantoolSettings)
    {
        if (!string.IsNullOrEmpty(tarantoolSettings.Value.ConnectionString))
            _client = new RequestClient(tarantoolSettings.Value.ConnectionString);

        if (_client == null)
            throw new ReserveException("Не удалось создать HTTP клиент");
    }

    /// <inheritdoc />
    public async Task<List<PoolAwb>> GetAllPools(int page, int limit)
    {
        //http://localhost:8081/pool_awb_service/get_all?page=0&limit=1000
        var arr = await _client.GetRequest<List<JsonArray>>($"get_all?page={page}&limit={1000}");
        return arr == null
            ? new List<PoolAwb>()
            : arr.Select(item => item.AsPoolAwb()).ToList()!;
    }

    /// <inheritdoc />
    public async Task<ReservePoolAwb> ReservePool(int poolLen, int agentId, int prefix, int beginNum = 0)
    {
        //http://localhost:8081/pool_awb_service/reserve_pool_awb?pool_len=100000&id_agent=3&prefix=555&begin_num=4000000
        var beginNumQuery = string.Empty;
        if (beginNum != 0)
            beginNumQuery = $"&begin_num={beginNum}";

        var res = await _client.GetRequest<ReservePool>(
            $"reserve_pool_awb?pool_len={poolLen}&id_agent={agentId}&prefix={prefix}{beginNumQuery}");
        if (res == null)
            throw new ReserveException("Не удалось получить ответ от БД");
        if (res.HasError)
            throw new ReserveException(res.Message);
        return (ReservePoolAwb)res!;
    }

    /// <inheritdoc />
    public async Task<ReserveNumberAwb> ReserveNumber(int agentId, int prefix, int beginNum = 0)
    {
        //http://localhost:8081/pool_awb_service/reserve_number?id_agent=3&prefix=555&begin_num=100000
        var beginNumQuery = string.Empty;
        if (beginNum != 0)
            beginNumQuery = $"&begin_num={beginNum}";
        var res = await _client.GetRequest<ReserveNumber>(
            $"reserve_number?id_agent={agentId}&prefix={prefix}{beginNumQuery}");
        if (res == null)
            throw new ReserveException("Не удалось получить ответ от БД");
        if (res.HasError)
            throw new ReserveException(res.Message);
        return (ReserveNumberAwb)res!;
    }
}