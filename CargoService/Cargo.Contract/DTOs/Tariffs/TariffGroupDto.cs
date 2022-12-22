using Cargo.Contract.Common;
using System.Collections.Generic;

namespace Cargo.Contract.DTOs.Tariffs
{
    /// <summary>
    /// Справочник тарифных групп
    /// </summary>
    public class TariffGroupDto : IContragentSpecific
    {
        public int Id { get; set; }

        public int ContragentId { get; set; }

        /// <summary>
        /// Код тарифной группы 
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Описание (рус.)
        /// </summary>
        public string DescriptionRus { get; set; }
        /// <summary>
        /// Описание (англ.)
        /// </summary>
        public string DescriptionEng { get; set; }

        /// <summary>
        /// Аэропорты
        /// </summary>
        public List<IataLocationDto> Airports { get; set; }
    }
}