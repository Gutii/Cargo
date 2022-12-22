namespace Cargo.Infrastructure.Data.Model.Settings.CommPayloads
{
    public class CommPayload
    {
        public int Id { get; set; }
        public decimal Weight { get; set; }
        public decimal Volume { get; set; }
        public CommPayloadRule4AicraftType CommPayloadRule4AicraftType { get; set; }
        public CommPayloadRule4Carrier CommPayloadRule4Carrier { get; set; }
        public CommPayloadRule4Route CommPayloadRule4Route { get; set; }
        public CommPayloadRule4Flight CommPayloadRule4Flight { get; set; }
    }
}
