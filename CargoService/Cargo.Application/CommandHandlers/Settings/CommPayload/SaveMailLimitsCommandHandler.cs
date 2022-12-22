using Cargo.Application.Services.CommercialPayload;
using Cargo.Contract.Commands.Settings.CommPayload;
using Cargo.Contract.DTOs.Settings.CommercialPayload;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.CommandHandlers.Settings.CommPayload
{
    public class SaveMailLimitsCommandHandler : ICommandHandler<SaveMailLimitsCommand, MailLimitsDto>
    {
        MailLimitsService mailLimitsService;

        public SaveMailLimitsCommandHandler(MailLimitsService mailLimitsService)
        {
            this.mailLimitsService = mailLimitsService;
        }

        public Task<MailLimitsDto> Handle(SaveMailLimitsCommand request, CancellationToken cancellationToken)
        {
            var task = mailLimitsService.SaveMailLimits(request);

            return task;
        }
    }
}
