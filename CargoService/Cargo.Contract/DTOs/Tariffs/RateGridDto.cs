using Cargo.Contract.Common;
using System.Collections.Generic;

namespace Cargo.Contract.DTOs.Tariffs
{
    public class RateGridDto : IContragentSpecific
    {
        public ulong id { get; set; }
        public int ContragentId { get; set; }
        public string code { get; set; }
        public List<uint> ranks { get; set; }

        public RateGridDto()
        {

        }

        public RateGridDto(ulong ulId, int contragentId, string sCode)
        {
            this.id = ulId;
            this.ContragentId = contragentId;
            this.code = sCode;
        }
    }
}