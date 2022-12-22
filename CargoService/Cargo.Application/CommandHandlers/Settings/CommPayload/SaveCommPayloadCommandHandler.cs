using Cargo.Application.Services.Settings;
using Cargo.Contract.Commands.Settings;
using Cargo.Contract.DTOs.Settings;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.CommandHandlers.Settings
{
    internal class SaveCommPayloadCommandHandler : ICommandHandler<SaveCommPayloadCommand, CommPayloadDto>
    {
        CommPayloadsService commPayloadsService;

        public SaveCommPayloadCommandHandler(CommPayloadsService commPayloadsService)
        {
            this.commPayloadsService = commPayloadsService;
        }

        public Task<CommPayloadDto> Handle(SaveCommPayloadCommand request, CancellationToken cancellationToken)
        {
            var task = commPayloadsService.SavePayloads(request);

            return task;
        }
    }
}
