#nullable enable
using System.Text.Json.Nodes;

namespace Cargo.Infrastructure.Data.Model.PoolAwbService;

public class ReserveNumber
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
    public JsonArray? UsedPool { get; set; }

    /// <summary>
    /// Префикс накладной
    /// </summary>
    public int Prefix { get; set; }

    /// <summary>
    /// Серийный номер с проверочным числом
    /// </summary>
    public int SerialNumber { get; set; }

    /// <summary>
    /// Полный номер накладной
    /// </summary>
    public string FullNumber { get; set; }
}