using Cargo.Application.Services.Settings;
using Cargo.Contract.Commands.Settings;
using IdealResults;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.CommandHandlers.Settings
{
    public class DeleteCommPayloadsCommandHandler : ICommandHandler<DeleteCommPayloadCommand, Result>
    {
        CommPayloadsService CommPayloadsService;

        public DeleteCommPayloadsCommandHandler(CommPayloadsService commPayloadsService)
        {
            this.CommPayloadsService = commPayloadsService;
        }

        public Task<Result> Handle(DeleteCommPayloadCommand request, CancellationToken cancellationToken)
        {
            var task = CommPayloadsService.DeleteCommPayload(request.CommPayloadId);

            return task;
        }
    }
}
