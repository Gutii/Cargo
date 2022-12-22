using Cargo.Infrastructure.Data.Model.Dictionary;
using System.Collections.Generic;

namespace Cargo.Infrastructure.Data.Model.Tariffs
{
    /// <summary>
    /// Справочник тарифных групп
    /// </summary>
    public class TariffGroup
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
        public List<IataLocation> Airports { get; set; }

        public ICollection<TariffSolution> TariffSolutionsOrigins { get; set; }
        public ICollection<TariffSolution> TariffSolutionsDesinations { get; set; }
    }
}