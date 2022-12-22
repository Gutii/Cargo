namespace Cargo.Infrastructure.Data.Model.Settings.CommPayloads
{
    public class CommPayloadRule4Carrier
    {
        public int CommPayloadRuleId { get; set; }

        public CommPayload CommercialPayload { get; set; }

        public string Carrier { get; set; }
    }
}
