namespace Cargo.Infrastructure.Data.Model.Settings.CommPayloads
{
    public class CommPayloadRule4AicraftType
    {
        public int CommPayloadRuleId { get; set; }

        public CommPayload CommercialPayload { get; set; }

        public int AircraftTypeId { get; set; }

        public string AircraftType { get; set; }
    }
}
