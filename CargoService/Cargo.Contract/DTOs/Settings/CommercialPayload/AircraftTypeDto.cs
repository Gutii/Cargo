namespace Cargo.Contract.DTOs
{
    public class AircraftTypeDto
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
        public int MaxGrossPayload { get; set; }
        public int MaxTakeOffWeight { get; set; }
        public int Oew { get; set; }

        public bool? IsCargo { get; set; }

        public string BaseType { get; set; }

    }
}
