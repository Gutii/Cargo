using Cargo.Infrastructure.Data.Model.Tariffs;
using System.Collections.Generic;

namespace Cargo.Infrastructure.Data.Model.Dictionary
{
    /// <summary>
    /// Справочник валюты
    /// </summary>
    public class Currency
    {
        public int Id { get; set; }
        /// <summary>
        /// Цифровой код
        /// </summary>
        public string DigitalCode { get; set; }
        /// <summary>
        /// Буквенный код
        /// </summary>
        public string AlphabeticCode { get; set; }

        /// <summary>
        /// Русское наименование валюты
        /// </summary>
        public string NameRus { get; set; }
        /// <summary>
        /// Английское наименование валюты
        /// </summary>
        public string NameEng { get; set; }

        public virtual ICollection<CarrierChargeBinding> CarrierChargeBindings { get; set; }
    }
}