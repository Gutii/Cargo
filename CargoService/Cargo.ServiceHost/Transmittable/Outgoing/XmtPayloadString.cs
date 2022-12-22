namespace Cargo.ServiceHost.Transmittable.Outgoing
{
    public class XmtPayloadString : XmtPayloadBase
    {
        private string m_sValue = "";

        public XmtPayloadString() : base()
        {
        }

        public XmtPayloadString(string sVal) : this()
        {
            value = sVal;
        }

        public string value
        {
            get => m_sValue;
            set => m_sValue = value;
        }
    }
}