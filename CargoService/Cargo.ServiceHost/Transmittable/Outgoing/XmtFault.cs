using System;

namespace Cargo.ServiceHost.Transmittable.Outgoing
{
    public class XmtFault
    {
        private string m_sMessage = "";
        private string m_sSource = "";
        private string m_sStackTrace = "";

        public XmtFault(Exception x)
        {
            if (x == null) return;

            message = x.Message;
            source = x.Source;
            stackTrace = x.StackTrace;
        }

        public string message
        {
            get => m_sMessage;
            private set => m_sMessage = (value ?? "").Trim();
        }

        public string source
        {
            get => m_sSource;
            private set => m_sSource = (value ?? "").Trim();
        }

        public string stackTrace
        {
            get => m_sStackTrace;
            private set => m_sStackTrace = (value ?? "").Trim();
        }
    }
}