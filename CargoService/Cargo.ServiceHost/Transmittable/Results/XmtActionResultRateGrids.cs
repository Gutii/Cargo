using Cargo.ServiceHost.Transmittable.Outgoing;
using Cargo.ServiceHost.Transmittable.Payloads;
using System;

namespace Cargo.ServiceHost.Transmittable.Results
{
    public class XmtActionResultRateGrids : XmtActionResultBase<XmtPayloadRateGrids>
    {
        public XmtActionResultRateGrids() : base()
        {
        }

        public XmtActionResultRateGrids(bool bRes) : base(bRes)
        {
        }

        public XmtActionResultRateGrids(bool bRes, XmtPayloadRateGrids p) : base(bRes, p)
        {
        }

        public XmtActionResultRateGrids(Exception x) : base(x)
        {
        }
    }
}
