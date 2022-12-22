using Cargo.Contract.DTOs.Tariffs;
using Cargo.ServiceHost.Transmittable.Outgoing;

namespace Cargo.ServiceHost.Transmittable.Payloads
{
    public class XmtPayloadRateGrid : XmtPayloadBase
    {
        private RateGridDto mRateGrid = null;

        public XmtPayloadRateGrid() : base()
        {
        }

        public XmtPayloadRateGrid(RateGridDto dto) : this()
        {
            rateGrid = dto;
        }

        public RateGridDto rateGrid
        {
            get => mRateGrid;
            set => mRateGrid = value;
        }
    }
}