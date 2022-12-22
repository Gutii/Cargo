using System;

namespace Cargo.ServiceHost.Transmittable.Outgoing
{
    public class XmtActionResultUintArray : XmtActionResultBase<XmtPayloadUintArray>
    {
        public XmtActionResultUintArray() : base()
        {
        }

        public XmtActionResultUintArray(bool bRes) : base(bRes)
        {
        }

        public XmtActionResultUintArray(bool bRes, XmtPayloadUintArray p) : base(bRes, p)
        {
        }

        public XmtActionResultUintArray(Exception x) : base(x)
        {
        }
    }
}