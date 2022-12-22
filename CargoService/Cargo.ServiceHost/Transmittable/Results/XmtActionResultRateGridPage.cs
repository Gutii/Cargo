using Cargo.ServiceHost.Transmittable.Outgoing;
using Cargo.ServiceHost.Transmittable.Payloads;
using System;

namespace Cargo.ServiceHost.Transmittable.Results
{
    public class XmtActionResultRateGridPage : XmtActionResultBase<XmtPayloadRateGridPage>
    {
        public XmtActionResultRateGridPage() : base()
        {
        }

        public XmtActionResultRateGridPage(bool bRes) : base(bRes)
        {
        }

        public XmtActionResultRateGridPage(bool bRes, XmtPayloadRateGridPage p) : base(bRes, p)
        {
        }

        public XmtActionResultRateGridPage(Exception x) : base(x)
        {
        }
    }
}