using System.Threading;
using System.Threading.Tasks;
using Directories.Application.Services.Settings;
using IdealResults;
using Directories.Contract.DTOs.Settings.BookingRules;
using Directories.Contract.Commands.Settings.BookingRules;
using Cargo.Application.CommandHandlers;

namespace Directories.Application.CommandHandlers.Settings.BookingRules
{
    public class SaveCarrierSettinParamCommandHandler : ICommandHandler<SaveCarrierSettinParamCommand, Result<CarrierParametersSettingsDto>>
    {
        CarrierSettingsParamService CarrierSettingsParamService;

        public SaveCarrierSettinParamCommandHandler(CarrierSettingsParamService carrierSettingsParamService)
        {
            CarrierSettingsParamService = carrierSettingsParamService;
        }
        public async Task<Result<CarrierParametersSettingsDto>> Handle(SaveCarrierSettinParamCommand request, CancellationToken cancellationToken)
        {
            var result = CarrierSettingsParamService.SaveCarrierSettingsParam(request);

            return await Task.FromResult(result);
        }

    }
}
