#nullable enable
using System.Text.Json.Nodes;

namespace Cargo.Infrastructure.Data.Model.PoolAwbService;

public class ReservePool
{
    /// <summary>
    /// Флаг ошибки
    /// </summary>
    public bool HasError { get; set; }

    /// <summary>
    /// Сопуствующее сообщение
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Выделенный пул
    /// </summary>
    public JsonArray? Pool { get; set; }

    /// <summary>
    /// Предстоящий пул
    /// </summary>
    public JsonArray? PrevPool { get; set; }

    /// <summary>
    /// Следующий пул
    /// </summary>
    public JsonArray? NextPool { get; set; }
}