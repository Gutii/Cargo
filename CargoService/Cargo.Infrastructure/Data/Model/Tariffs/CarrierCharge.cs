using Cargo.Infrastructure.Data.Model.Dictionary;
using System.Collections.Generic;

namespace Cargo.Infrastructure.Data.Model.Tariffs
{
    public class CarrierCharge
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
        public string[] SalesChannels { get; set; }

        public ICollection<Shr> IncludedShrCodes { get; set; }
        public ICollection<Shr> ExcludedShrCodes { get; set; }
        public ICollection<TransportProduct> IncludedProducts { get; set; }
        public ICollection<TransportProduct> ExcludedProducts { get; set; }

        public ICollection<CarrierChargeBinding> CarrierChargeBindings { get; set; }
    }
}