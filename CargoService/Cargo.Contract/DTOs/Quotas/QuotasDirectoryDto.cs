using IDeal.Common.Components.Messages.ObjectStructures.Fwbs.Ver17;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Contract.DTOs.Quotas
{
    public class QuotasDirectoryDto
    {
        public Guid Id { get; set; }
        public int CarrierId { get; set; }
        public int AgentId { get; set; }
        public Agent Agent { get; set; }
        public string Name { get; set; }
    }
}
