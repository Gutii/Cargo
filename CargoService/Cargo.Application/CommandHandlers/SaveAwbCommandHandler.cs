using AutoMapper;
using Cargo.Application.Services;
using Cargo.Contract.Commands;
using Cargo.Infrastructure.Data.Model;
using IDeal.Common.Components;
using IdealResults;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.CommandHandlers
{
    public class SaveAwbCommandHandler : ICommandHandler<SaveAwbCommand, Result<int>>
    {
        IMapper mapper;
        AwbService awbService;

        public SaveAwbCommandHandler(IMapper mapper, AwbService awbService)
        {
            this.mapper = mapper;
            this.awbService = awbService;
        }

        public async Task<Result<int>> Handle(SaveAwbCommand request, CancellationToken cancellationToken)
        {
            Result<Awb> trackedResult = awbService.TrackedAwb(request.Awb.Id);

            if (!trackedResult.IsSuccess)
            {
                return trackedResult.ToResult<int>();
            }

            mapper.Map(request.Awb, trackedResult.Value);
            Result<int> saveResult = await awbService.SaveAwb(trackedResult.Value, request.Status,
                request.SelectedRoleNameEn == Role.Carrier.Value ? true : false);

            if (!saveResult.IsSuccess)
            {
                return saveResult.ToResult<int>();
            }
            return Result.Ok(saveResult.Value);
        }
    }
}