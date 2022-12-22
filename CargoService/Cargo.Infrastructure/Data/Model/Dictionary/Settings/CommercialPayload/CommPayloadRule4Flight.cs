
using System;

namespace Cargo.Infrastructure.Data.Model.Settings.CommPayloads
{
    public class CommPayloadRule4Flight
    {
        public int CommPayloadRuleId { get; set; }
        public CommPayload CommercialPayload { get; set; }
        public string FlightCarrier { get; set; }
        public string FlightNumber { get; set; }
        public DateTime? DateAt { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
