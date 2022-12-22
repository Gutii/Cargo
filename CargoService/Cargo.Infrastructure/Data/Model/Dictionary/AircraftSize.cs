namespace Cargo.Infrastructure.Data.Model.Dictionary;

public class AircraftSize
{
    /// <summary>
    /// Идентифифкатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Тип ВС
    /// </summary>
    public string AircraftType { get; set; }

    /// <summary>
    /// Погрузка вилочным
    /// </summary>
    public bool HEA { get; set; }

    /// <summary>
    /// Высота
    /// </summary>
    public int H { get; set; }

    /// <summary>
    /// Ширина
    /// </summary>
    public int W { get; set; }

    /// <summary>
    /// Максимальная длина
    /// </summary>
    public int L { get; set; }
}