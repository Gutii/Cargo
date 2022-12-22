using Cargo.Application.Services.CommPayload;
using Cargo.Contract.DTOs.Settings;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.CommandHandlers.Settings
{
    internal class SaveAircraftsCommandHandler : ICommandHandler<SaveAircraftsCommand, AircraftsDto>
    {
        AircraftsService AircraftsService;

        public SaveAircraftsCommandHandler(AircraftsService aircraftsService)
        {
            this.AircraftsService = aircraftsService;
        }

        public Task<AircraftsDto> Handle(SaveAircraftsCommand request, CancellationToken cancellationToken)
        {
            var task = AircraftsService.SaveAircrafts(request);

            return task;
        }
    }
}
