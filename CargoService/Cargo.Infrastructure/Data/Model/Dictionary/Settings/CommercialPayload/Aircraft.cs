using Cargo.Infrastructure.Data.Model.Dictionary;


namespace Cargo.Infrastructure.Data.Model.Settings
{
    //Борты с ПКЗ
    public class Aircraft
    {
        public int Id { get; set; }

        public string OnboardNumber { get; set; }
        public int MaxGrossPayload { get; set; }
        public int MaxTakeOffWeight { get; set; }
        public string AccseptedShr { get; set; }
        public int AircraftTypeId { get; set; }
        public AircraftType AircraftType { get; set; }
        public string IataCode { get; set; }
    }
}
