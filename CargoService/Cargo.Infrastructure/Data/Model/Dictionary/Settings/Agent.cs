using Cargo.Infrastructure.Data.Model.Quotas;
using Cargo.Infrastructure.Data.Model.Settings;
using Cargo.Infrastructure.Data.Model.Settings.PoolAwbs;
using System.Collections.Generic;

namespace Cargo.Infrastructure.Data.Model.Dictionary.Settings
{
    public class Agent
    {
        public int Id { get; set; }

        public int CarrierId { get; set; }
        public Customer Carrier { get; set; }
        public string IataCargoAgentNumericCode { get; set; }
        public string IataCargoAgentCassAddress { get; set; }
        public string ParticipantIdentifier { get; set; }

        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }

        public string ImpCode { get; set; }
        public string KosudCode { get; set; }

        public string Remarks { get; set; }

        public int ContragentId { get; set; }
        public Contragent Contragent { get; set; }

        public ICollection<ContactInformation> AdditionalContactInfo { get; set; }

        public ICollection<AgentContract> AgentContracts { get; set; }
        public ICollection<QuotasDirectory> QuotasDirectory { get; set; }
    }
}
