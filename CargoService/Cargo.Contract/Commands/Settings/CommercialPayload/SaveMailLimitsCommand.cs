using Cargo.Contract.DTOs.Settings.CommercialPayload;

namespace Cargo.Contract.Commands.Settings.CommPayload
{
    public class SaveMailLimitsCommand : ICommand<MailLimitsDto>
    {
        public string IataCode { get; set; }
        public MailLimitsDto MailLimits { get; set; }
    }
}
