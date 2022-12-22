using System.Collections.Generic;

namespace Cargo.ServiceHost.Transmittable.Outgoing
{
    public class XmtPayloadUintArray : XmtPayloadBase
    {
        private List<uint> mValue = null;

        public XmtPayloadUintArray() : base()
        {
        }

        public XmtPayloadUintArray(List<uint> val) : this()
        {
            value = val;
        }

        public List<uint> value
        {
            get => mValue;
            set => mValue = value;
        }
    }
}