using System;

namespace Cargo.ServiceHost.Transmittable.Outgoing
{
    /// <summary>
    /// The base class for all action result classes. Must not be instantiated.
    /// </summary>
    /// <typeparam name="T">Payload type. Must be class and successor of XmtPayloadBase.</typeparam>
    public abstract class XmtActionResultBase<T> where T : XmtPayloadBase
    {
        private bool m_bResult = false;
        private T mPayload = null;
        private XmtFault mFault = null;

        public XmtActionResultBase()
        {
        }

        public XmtActionResultBase(bool bRes)
        {
            result = bRes;
        }

        public XmtActionResultBase(bool bRes, T p)
        {
            result = bRes;
            payload = p;
        }

        public XmtActionResultBase(Exception x) : this(false)
        {
            fault = new XmtFault(x);
        }

        public bool result
        {
            get => m_bResult;
            private set => m_bResult = value;
        }

        public T payload
        {
            get => mPayload;
            private set => mPayload = value;
        }

        public XmtFault fault
        {
            get => mFault;
            private set => mFault = value;
        }

        public bool havePayload => payload != null;

        public bool haveFault => fault != null;
    }
}