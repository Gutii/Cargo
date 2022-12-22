namespace Cargo.Infrastructure.Data.Model.Dictionary
{
    public class Airline
    {
        public string IataCode { get; set; }
        public string IcaoCode { get; set; }

        /// <summary>
        /// AWB Prefix
        /// </summary>
        public string PrefixAwb { get; set; }

        public int ContragentId { get; set; }
        public Contragent Contragent { get; set; }
    }
}
