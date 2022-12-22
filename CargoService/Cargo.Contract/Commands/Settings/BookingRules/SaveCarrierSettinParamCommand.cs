using IdealResults;
using Directories.Contract.DTOs.Settings.BookingRules;
using IDeal.Common.Components;
using Cargo.Contract.Commands;

namespace Directories.Contract.Commands.Settings.BookingRules
{
    public class SaveCarrierSettinParamCommand : ICommand<Result<CarrierParametersSettingsDto>>, IAuthenticatedMessage
    {
        public CarrierParametersSettingsDto CarrierParametersSettingsCharge { get; set; }
        public int? AgentId { get; set; }
        public int? GhaId { get; set; }
        public int CustomerId { get; set; }
        public int? CarrierId { get; set; }
        public string SelectedRoleNameEn { get; set; }
        public string Language { get; set; }        
    }
}
