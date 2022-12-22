using Cargo.Contract.DTOs.Quotas;
using IDeal.Common.Components;
using IdealResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Contract.Commands.Quotas
{
    public class SaveQuotasDirectoryCommand : ICommand<Result<QuotasDirectoryDto>>, IAuthenticatedMessage
    {
        public QuotasDirectoryDto QuotasDirectory { get; set; }
        public int? AgentId { get; set; }
        public int? GhaId { get; set; }
        public int? CarrierId { get; set; }
        public string SelectedRoleNameEn { get; set; }
        public string Language { get; set; }
        public int CustomerId { get; set; }
    }
}
