using Cargo.Infrastructure.Data.Model.Dictionary;
using System.Collections.Generic;

namespace Cargo.Infrastructure.Data.Model.Tariffs
{
    public class CarrierChargeBinding
    {
        public long Id { get; set; }

        /// <summary>
        /// Сбор
        /// </summary>
        public CarrierCharge CarrierCharge { get; set; }

        public Currency Currency { get; set; }

        public string Parameter { get; set; }

        public decimal Value { get; set; }

        /// <summary>
        /// Страна
        /// </summary>
        public Country Country { get; set; }

        /// <summary>
        /// Аэропорты
        /// </summary>
        public List<IataLocation> Airports { get; set; }
    }
}