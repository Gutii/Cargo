using Cargo.Application.Services.CommPayload;
using Cargo.Contract.Commands.Settings;
using IdealResults;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.CommandHandlers.Settings
{
    public class DeleteAircraftsCommandHandlercs : ICommandHandler<DeleteAircraftCommand, Result>
    {
        AircraftsService AircraftsService;

        public DeleteAircraftsCommandHandlercs(AircraftsService aircraftsService)
        {
            this.AircraftsService = aircraftsService;
        }

        public Task<Result> Handle(DeleteAircraftCommand request, CancellationToken cancellationToken)
        {
            var task = AircraftsService.DeleteAircrafts(request.AircraftId);

            return task;
        }
    }
}
