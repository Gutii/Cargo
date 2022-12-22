using Cargo.Infrastructure.Data.Model.Dictionary.Settings;
using System;

namespace Cargo.Infrastructure.Data.Model.Quotas
{
    public class QuotasDirectory
    {
        public Guid Id { get; set; }
        public int CarrierId { get; set; }
        public int AgentId { get; set; }
        public Agent Agent { get; set; }
        public string Name { get; set; }
    }
}
