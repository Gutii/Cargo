using AutoMapper;
using Cargo.Application.Services;
using Cargo.Contract.DTOs.Bookings;
using Cargo.Contract.DTOs.Quotas;
using Cargo.Contract.Queries.Bookings;
using Cargo.Contract.Queries.Quotas;
using Cargo.Infrastructure.Data;
using IDeal.Common.Components.Paginator;
using IdealResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.QueryHandlers.Quotas
{
    public class QuotasOperativeQueryHandler : IQueryHandler<QuotasOperativeQuery, PagedResult<QuotasOperativeDto>>
    {
        IMapper mapper;
        CargoContext CargoContext;

        public QuotasOperativeQueryHandler(IMapper mapper, CargoContext cargoContext)
        {
            this.mapper = mapper;
            this.CargoContext = cargoContext;
        }
        public async Task<PagedResult<QuotasOperativeDto>> Handle(QuotasOperativeQuery request, CancellationToken cancellationToken)
        {
            var res = Task.Run(() =>
            {
                var QuotasOperative = CargoContext.QuotasOperative
                .Where(c => c.CarrierId == request.CarrierId)
                .Page(new PageInfo { PageIndex = request.PageIndex ?? 0, PageSize = request.PageSize ?? 20 });
                return mapper.Map<PagedResult<QuotasOperativeDto>>(QuotasOperative);
            });

            return await res;
        }
    }
}
