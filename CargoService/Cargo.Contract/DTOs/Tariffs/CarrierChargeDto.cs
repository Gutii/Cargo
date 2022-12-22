using Cargo.Contract.Common;
using System.Collections.Generic;

namespace Cargo.Contract.DTOs.Tariffs
{
    public class CarrierChargeDto : IContragentSpecific
    {
        public long Id { get; set; }
        public int ContragentId { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public string DescriptionEng { get; set; }
        public string DescriptionRus { get; set; }

        /// <summary>
        /// Вид сбора по применимости
        ///   M - Обязательные (Mandatory)
        ///   CM - Условно-обязательные (Conditionally mandatory)
        ///   AM - Ручные (Added manaully) 
        /// </summary>
        public string ApplicationType { get; set; }

        /// <summary>
        /// Получатели сбора по AWB:
        ///     C – Carrier – сборы, взимаемые в пользу перевозчика;
        ///     А – Agent – сборы, взимаемые в пользу агента;
        /// </summary>
        public string Recepient { get; set; }

        /// <summary>
        /// All-in
        /// </summary>
        public bool IsAllIn { get; set; }

        /// <summary>
        /// Канал продаж
        /// </summary>
        public List<string> SalesChannels { get; set; }

        public List<ShrDto> IncludedShrCodes { get; set; }
        public List<ShrDto> ExcludedShrCodes { get; set; }
        public List<TransportProductDto> IncludedProducts { get; set; }
        public List<TransportProductDto> ExcludedProducts { get; set; }

        public List<CarrierChargeBindingDto> CarrierChargeBindings { get; set; }
    }
}