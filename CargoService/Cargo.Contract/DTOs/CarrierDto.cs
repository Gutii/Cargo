using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cargo.Contract.DTOs
{
    public class CarrierDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("airlineId")]
        public int AirlineId { get; set; }
        [JsonPropertyName("iataCode")]
        public string IataCode { get; set; }
        [JsonPropertyName("icaoCode")]
        public string IcaoCode { get; set; }
        [JsonPropertyName("acPrefix")]
        public string AcPrefix { get; set; }
        [JsonPropertyName("acMailPrefix")]
        public string AcMailPrefix { get; set; }
    }
}
