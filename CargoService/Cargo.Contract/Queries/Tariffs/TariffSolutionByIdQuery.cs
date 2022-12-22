using Cargo.Contract.DTOs.Tariffs;

namespace Cargo.Contract.Queries.Tariffs
{
    public class TariffSolutionByIdQuery : IQuery<TariffSolutionDto>
    {
        public int TariffSolutionId { get; set; }
    }
}