
namespace Cargo.Contract.DTOs
{
    /// <summary>
    /// Местоположение по справочнику IATA
    /// </summary>
    public class IataLocationDto
    {
        public int Id { get; set; }
        public string Code { get; set; }

        /// <summary>
        /// Наименование местоположения: аэропорт или городской округ
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Русский вариант наименования
        /// </summary>
        public string RussianName { get; set; }

        /// <summary>
        /// Наименование местоположения: аэропорт или городской округ
        /// </summary>
        public string CityName { get; set; }
    }
}
