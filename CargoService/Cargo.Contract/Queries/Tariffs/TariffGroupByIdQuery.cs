using Cargo.Contract.DTOs.Tariffs;

namespace Cargo.Contract.Queries.Tariffs
{
    public class TariffGroupByIdQuery : IQuery<TariffGroupDto>
    {
        public int TariffGroupId { get; set; }
    }
}