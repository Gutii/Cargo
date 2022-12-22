#nullable enable
namespace Cargo.Infrastructure.Data.Model.PoolAwbService;

/// <summary>
/// Модель пула номеров накладных
/// </summary>
public class PoolAwb
{
    /// <summary>
    /// Идентифифкатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентифифкатор агента которому принадлежит пул
    /// </summary>
    /// <remarks>0 - пул перевозчика</remarks>
    public int AgentId { get; set; }

    /// <summary>
    /// Префикс накладной
    /// </summary>
    public int Prefix { get; set; }

    /// <summary>
    /// Стартовый номер без проверочного числа
    /// </summary>
    public int BeginNum { get; set; }

    /// <summary>
    /// Стартовый номер с проверочным числом
    /// </summary>
    public int FullBeginNum { get; set; }

    /// <summary>
    /// Конечный номер без проверочного числа
    /// </summary>
    public int EndNum { get; set; }

    /// <summary>
    /// Конечный номер с проверочным числом
    /// </summary>
    public int FullEndNum { get; set; }

    /// <summary>
    /// Длина пула
    /// </summary>
    public int PoolLen { get; set; }

    /// <summary>
    /// Кол-во занятых в пуле номеров
    /// </summary>
    public int Occupied { get; set; }
}