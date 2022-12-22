using System;

namespace Cargo.ServiceHost.Transmittable.Outgoing
{
    /// <summary>
    /// Action result class with empty payload.
    /// </summary>
    public class XmtActionResult : XmtActionResultBase<XmtPayload>
    {
        public XmtActionResult() : base()
        {
        }

        public XmtActionResult(bool bRes) : base(bRes)
        {
        }

        public XmtActionResult(bool bRes, XmtPayload p) : base(bRes, p)
        {
        }

        public XmtActionResult(Exception x) : base(x)
        {
        }
    }
}