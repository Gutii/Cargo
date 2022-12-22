namespace Cargo.Contract.DTOs.Bookings
{

    public class TaxChargeDto
    {
        public ulong Id { get; set; }
        public int AwbId { get; set; }
        /// <summary>
        /// Сборы за перевозку
        /// </summary>
        public decimal Charge { get; set; }

        /// <summary>
        /// Сбор за объявленную ценность
        /// </summary>
        public decimal ValuationCharge { get; set; }

        /// <summary>
        /// TAX
        /// </summary>
        public decimal Tax { get; set; }

        /// <summary>
        /// Итого других агентов
        /// </summary>
        public decimal TotalOtherChargesDueAgent { get; set; }

        /// <summary>
        /// Итого доп. сборов
        /// </summary>
        public decimal TotalOtherChargesDueCarrier { get; set; }

        /// <summary>
        /// Итого
        /// </summary>
        public decimal Total { get; set; }
    }

}
