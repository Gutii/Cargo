using System.Collections.Generic;

namespace Cargo.Contract.DTOs.Tariffs
{
    public class TransportProductDto
    {
        public ulong Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Trigger { get; set; }
        public int PoolStartNumber { get; set; }
        public List<ShrDto> ShrCodes { get; set; }
    }
}