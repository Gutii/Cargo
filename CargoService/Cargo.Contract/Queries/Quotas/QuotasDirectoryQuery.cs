using Cargo.Contract.DTOs.Quotas;
using IDeal.Common.Components.Paginator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Contract.Queries.Quotas
{
    public class QuotasDirectoryQuery : IQuery<PagedResult<QuotasDirectoryDto>>
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public int CarrierId { get; set; }
    }
}
