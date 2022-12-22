using Cargo.Contract.DTOs.Tariffs;
using Cargo.ServiceHost.Transmittable.Outgoing;

namespace Cargo.ServiceHost.Transmittable.Payloads
{
    public class XmtPayloadRateGrids : XmtPayloadBase
    {
        private RateGridDto[] mRateGrids = null;

        public XmtPayloadRateGrids() : base()
        {
        }

        public XmtPayloadRateGrids(RateGridDto[] dtos) : this()
        {
            rateGrids = dtos;
        }

        public RateGridDto[] rateGrids
        {
            get => mRateGrids;
            set => mRateGrids = value;
        }
    }
}