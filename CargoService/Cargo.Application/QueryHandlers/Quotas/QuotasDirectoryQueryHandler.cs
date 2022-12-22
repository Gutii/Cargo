using AutoMapper;
using Cargo.Contract.DTOs.Quotas;
using Cargo.Contract.Queries.Quotas;
using Cargo.Infrastructure.Data;
using IDeal.Common.Components.Paginator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.QueryHandlers.Quotas
{
    public class QuotasDirectoryQueryHandler : IQueryHandler<QuotasDirectoryQuery, PagedResult<QuotasDirectoryDto>>
    {
        IMapper mapper;
        CargoContext CargoContext;

        public QuotasDirectoryQueryHandler(IMapper mapper, CargoContext cargoContext)
        {
            this.mapper = mapper;
            this.CargoContext = cargoContext;
        }
        public async Task<PagedResult<QuotasDirectoryDto>> Handle(QuotasDirectoryQuery request, CancellationToken cancellationToken)
        {
            var res = Task.Run(() =>
            {
                var QuotasOperative = CargoContext.QuotasDirectory
                .Where(c => c.CarrierId == request.CarrierId)
                .Page(new PageInfo { PageIndex = request.PageIndex ?? 0, PageSize = request.PageSize ?? 20 });
                return mapper.Map<PagedResult<QuotasDirectoryDto>>(QuotasOperative);
            });

            return await res;
        }
    }
}
