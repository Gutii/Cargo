namespace Cargo.Infrastructure.Data.Model
{
    public class OtherCharge
    {
        public int Id { get; set; }
        public int AwbId { get; set; }

        /// <summary>
        /// Навигационное свойства
        /// </summary>
        public Awb Awb { get; set; }
        /// <summary>
        /// Вид сбора
        /// </summary>
        public string TypeCharge { get; set; }

        /// <summary>
        /// C/A
        /// </summary>
        public string CA { get; set; }

        /// <summary>
        /// Prepaid
        /// </summary>
        public decimal Prepaid { get; set; }

        /// <summary>
        /// Collect
        /// </summary>
        public decimal Collect { get; set; }
    }
}
