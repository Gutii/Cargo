using Cargo.Contract.DTOs.Tariffs;

namespace Cargo.Contract.Commands.Tariffs
{
    public class CreateOrUpdateTariffSolutionCommand : ICommand<TariffSolutionDto>
    {
        public TariffSolutionDto TariffSolution { get; set; }
    }
}