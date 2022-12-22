using Cargo.ServiceHost.Transmittable.Outgoing;
using Cargo.ServiceHost.Transmittable.Payloads;
using System;

namespace Cargo.ServiceHost.Transmittable.Results
{
    public class XmtActionResultRateGrid : XmtActionResultBase<XmtPayloadRateGrid>
    {
        public XmtActionResultRateGrid() : base()
        {
        }

        public XmtActionResultRateGrid(bool bRes) : base(bRes)
        {
        }

        public XmtActionResultRateGrid(bool bRes, XmtPayloadRateGrid p) : base(bRes, p)
        {
        }

        public XmtActionResultRateGrid(Exception x) : base(x)
        {
        }
    }
}
