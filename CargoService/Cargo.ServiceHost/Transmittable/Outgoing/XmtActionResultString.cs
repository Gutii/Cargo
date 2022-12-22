using System;

namespace Cargo.ServiceHost.Transmittable.Outgoing
{
    public class XmtActionResultString : XmtActionResultBase<XmtPayloadString>
    {
        public XmtActionResultString() : base()
        {
        }

        public XmtActionResultString(bool bRes) : base(bRes)
        {
        }

        public XmtActionResultString(bool bRes, XmtPayloadString p) : base(bRes, p)
        {
        }

        public XmtActionResultString(Exception x) : base(x)
        {
        }
    }
}