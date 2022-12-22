using Cargo.Contract.DTOs;
using Cargo.Contract.DTOs.Quotas;
using IDeal.Common.Components.Paginator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Contract.Queries.Quotas
{
    public class QuotasOperativeQuery : IQuery<PagedResult<QuotasOperativeDto>>
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public int CarrierId { get; set; }
    }
}
