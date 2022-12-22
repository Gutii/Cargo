#nullable enable
using Cargo.Infrastructure.Data.Model.PoolAwbService;

namespace Cargo.Application.Services.PoolAwbNum;

public class ReservePoolAwb : ReservePool
{
    /// <inheritdoc cref="ReservePool.Pool"/>
    public PoolAwb? PoolAwb => Pool.AsPoolAwb();

    /// <inheritdoc cref="ReservePool.PrevPool"/>
    public PoolAwb? PrevPoolAwb => PrevPool.AsPoolAwb();

    /// <inheritdoc cref="ReservePool.NextPool"/>
    public PoolAwb? NextPoolAwb => NextPool.AsPoolAwb();
}

public class ReserveNumberAwb : ReserveNumber
{
    /// <inheritdoc cref="ReserveNumber.UsedPool"/>
    public PoolAwb? UsedPoolAwb => UsedPool.AsPoolAwb();
}