using Cargo.Application.CommandHandlers;
using Directories.Application.Services.Settings;
using Directories.Contract.Commands.Settings.BookingRules;
using Directories.Contract.DTOs.Settings.BookingRules;
using System.Threading;
using System.Threading.Tasks;

namespace Directories.Application.CommandHandlers.Settings.BookingRules
{
    internal class ResetCarrierSettinParamCommandHandler : ICommandHandler<ResetCarrierSettinParamCommand, CarrierParametersSettingsDto>
    {
        CarrierSettingsParamService CarrierSettingsParamService;

        public ResetCarrierSettinParamCommandHandler(CarrierSettingsParamService carrierSettingsParamService)
        {
            CarrierSettingsParamService = carrierSettingsParamService;
        }

        public Task<CarrierParametersSettingsDto> Handle(ResetCarrierSettinParamCommand request, CancellationToken cancellationToken)
        {
            var task = CarrierSettingsParamService.ResetCarrierSettinParam(request);

            return task;
        }
    }
}
