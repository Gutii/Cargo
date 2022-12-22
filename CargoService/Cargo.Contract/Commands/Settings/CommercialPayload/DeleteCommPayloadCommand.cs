using IdealResults;

namespace Cargo.Contract.Commands.Settings
{
    public class DeleteCommPayloadCommand : ICommand<Result>
    {
        public int CommPayloadId { get; set; }
    }
}
