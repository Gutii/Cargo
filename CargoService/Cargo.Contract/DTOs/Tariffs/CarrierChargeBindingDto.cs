using System.Collections.Generic;

namespace Cargo.Contract.DTOs.Tariffs
{
    public class CarrierChargeBindingDto
    {
        public long Id { get; set; }

        public CurrencyDto Currency { get; set; }

        public string Parameter { get; set; }

        public decimal Value { get; set; }

        /// <summary>
        /// Страна
        /// </summary>
        public CountryDto Country { get; set; }

        /// <summary>
        /// Аэропорты
        /// </summary>
        public List<IataLocationDto> Airports { get; set; }
    }
}