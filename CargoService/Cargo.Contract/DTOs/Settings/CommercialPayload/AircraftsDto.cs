namespace Cargo.Contract.DTOs.Settings
{
    public class AircraftsDto
    {
        public int Id { get; set; }
        public string IataCode { get; set; }
        public int AircraftTypeId { get; set; }
        public string AircraftType { get; set; }
        public string OnboardNumber { get; set; }
        public int Volume { get; set; }
        public int Weight { get; set; }
        public string AccseptedShr { get; set; }
    }
}
