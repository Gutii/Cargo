namespace Cargo.Infrastructure.Data.Model.Settings.CommPayloads
{
    public class CommPayloadRule4Route
    {
        public int CommPayloadRuleId { get; set; }
        public CommPayload CommercialPayload { get; set; }

        public string Origin { get; set; }
        public string Destination { get; set; }
    }
}
