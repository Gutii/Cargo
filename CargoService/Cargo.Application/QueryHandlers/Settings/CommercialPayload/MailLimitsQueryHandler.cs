using AutoMapper;
using Cargo.Application.Services.CommercialPayload;
using Cargo.Contract.DTOs.Settings.CommercialPayload;
using Cargo.Contract.Queries.Settings.CommercialPayload;
using Cargo.Infrastructure.Data.Model.Settings;
using IDeal.Common.Components.Paginator;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.QueryHandlers.Settings.CommercialPayload
{
    public class MailLimitsQueryHandler : IQueryHandler<MailLimitsQuery, PagedResult<MailLimitsDto>>
    {
        IMapper Mapper;
        MailLimitsService mailLimitsService;

        public MailLimitsQueryHandler(IMapper mapper, MailLimitsService mailLimitsService)
        {
            Mapper = mapper;
            this.mailLimitsService = mailLimitsService;
        }

        public async Task<PagedResult<MailLimitsDto>> Handle(MailLimitsQuery request, CancellationToken cancellationToken)
        {
            var task = mailLimitsService.GetMailLimits(request.PageInfo, request.IataCode);
            return await Mapper.Map<Task<PagedResult<MailLimits>>, Task<PagedResult<MailLimitsDto>>>(task);
        }
    }
}
