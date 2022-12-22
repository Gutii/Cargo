#nullable enable
using Cargo.Infrastructure.Data.Model.PoolAwbService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cargo.Application.Services.PoolAwbNum;

public interface IPoolAwbService
{
    /// <summary>
    /// Возвращает список пулов в БД
    /// </summary>
    /// <param name="page">Номер страницы</param>
    /// <param name="limit">Кол-во на странице</param>
    /// <returns></returns>
    public Task<List<PoolAwb>> GetAllPools(int page = 0, int limit = 1000);

    /// <summary>
    /// Резервирует пул номеров накладных за агентом
    /// </summary>
    /// <param name="poolLen">Длина пула</param>
    /// <param name="agentId">Идентиифкатор агента</param>
    /// <param name="prefix">Префикс накладной</param>
    /// <param name="beginNum">Стартовый номер (необязательно)</param>
    /// <returns></returns>
    public Task<ReservePoolAwb> ReservePool(int poolLen, int agentId, int prefix, int beginNum = 0);

    /// <summary>
    /// Выделение номера накладной из пула
    /// </summary>
    /// <param name="agentId">Идентифифкатор агента</param>
    /// <param name="prefix">Префикс накладной</param>
    /// <param name="beginNum">Начальный серийный номер (необязательно)</param>
    /// <returns></returns>
    public Task<ReserveNumberAwb> ReserveNumber(int agentId, int prefix, int beginNum = 0);
}