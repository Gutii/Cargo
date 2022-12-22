namespace Cargo.Contract.DTOs.Bookings
{

    public class TotalChargeDto
    {
        public ulong Id { get; set; }
        /// <summary>
        /// Столбец Prepaid
        /// </summary>
        public TaxChargeDto Prepaid { get; set; }

        /// <summary>
        /// Столбец Collect
        /// </summary>
        public TaxChargeDto Collect { get; set; }
    }
}
