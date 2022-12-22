using Cargo.Infrastructure.Data.Model.Dictionary.Settings;
using Cargo.Infrastructure.Data.Model.Tariffs;
using System;
using System.Collections.Generic;

namespace Cargo.Infrastructure.Data.Model.Dictionary
{
    /// <summary>
    /// Местоположение по справочнику IATA
    /// </summary>
    public class IataLocation
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

        public IataLocation MetropolitanArea { get; set; }

        /// <summary>
        /// Список аэропортов для случая "городской округ"
        /// </summary>
        public virtual ICollection<IataLocation> Airports { get; set; }

        /// <summary>
        /// Часовой пояс
        /// </summary>
        public TimeSpan TimeZone { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public IataLocationExtInfo IataLocationExtInfo { get; set; }

        public ICollection<TariffGroup> TariffGroups { get; set; }

        public ICollection<TariffSolution> TariffSolutionsOrigins { get; set; }
        public ICollection<TariffSolution> TariffSolutionsDestinations { get; set; }
        public ICollection<TariffSolution> TariffSolutionsTransits { get; set; }

        public ICollection<CarrierChargeBinding> CarrierChargeBindings { get; set; }
    }
}