using Cargo.Application.Services.CommercialPayload;
using Cargo.Contract.Commands.Settings.CommercialPayload;
using IdealResults;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.CommandHandlers.Settings.CommPayload
{
    public class DeleteMailLimitsCommandHandler : ICommandHandler<DeleteMailLimitsCommand, Result>
    {
        MailLimitsService mailLimitsService;

        public DeleteMailLimitsCommandHandler(MailLimitsService mailLimitsService)
        {
            this.mailLimitsService = mailLimitsService;
        }

        public Task<Result> Handle(DeleteMailLimitsCommand request, CancellationToken cancellationToken)
        {
            var task = mailLimitsService.DeleteMailLimits(request.MailLimitsId);

            return task;
        }
    }
}

