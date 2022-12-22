namespace Cargo.Infrastructure.Data.Model.Dictionary
{
    public class AircraftType
    {
        public int Id { get; set; }
        public string IataCode { get; set; }
        public string IcaoCode { get; set; }

        /// <summary>
        /// Международное название
        /// </summary>
        public string InternationalName { get; set; }

        /// <summary>
        /// Русский вариант
        /// </summary>
        public string RussianName { get; set; }

        public int MaxGrossCapacity { get; set; }
        public int MaxPayloadWeight { get; set; }
        public int MaxPayloadVolume { get; set; }
        public int Oew { get; set; }

        public bool? IsCargo { get; set; }

        public string BaseType { get; set; }

    }
}
