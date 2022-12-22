using IdealResults;

namespace Cargo.Contract.Commands.Settings
{
    public class DeleteAircraftCommand : ICommand<Result>
    {
        public int AircraftId { get; set; }
    }
}
