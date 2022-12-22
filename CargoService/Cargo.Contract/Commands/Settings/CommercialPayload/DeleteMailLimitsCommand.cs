using IdealResults;

namespace Cargo.Contract.Commands.Settings.CommercialPayload
{
    public class DeleteMailLimitsCommand : ICommand<Result>
    {
        public int MailLimitsId { get; set; }
    }
}
