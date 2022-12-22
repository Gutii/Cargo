using Cargo.Contract.DTOs.Tariffs;

namespace Cargo.Contract.Queries.Tariffs
{
    public class CarrierChargeByIdQuery : IQuery<CarrierChargeDto>
    {
        public int CarrierChargeId { get; set; }
    }
}