using AutoMapper;
using Cargo.Application.Services;
using Cargo.Contract.DTOs.Bookings;
using Cargo.Contract.Queries.Bookings;
using IdealResults;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.QueryHandlers.Bookings
{
    public class AwbQueryHandler : IQueryHandler<AwbQuery, Result<AwbDto>>
    {
        IMapper mapper;
        AwbService awbService;

        public AwbQueryHandler(IMapper mapper, AwbService awbService)
        {
            this.mapper = mapper;
            this.awbService = awbService;
        }
        public async Task<Result<AwbDto>> Handle(AwbQuery request, CancellationToken cancellationToken)
        {
            var awbResult = await this.awbService.Awb(request.Id, request.AwbIdentifier);

            awbResult.LogIfFailedOrInvalid();
            if (awbResult.IsSuccess)
            {
                return this.mapper.Map<Result<AwbDto>>(awbResult);
            }

            return awbResult.ToResult<AwbDto>();
        }
    }
}