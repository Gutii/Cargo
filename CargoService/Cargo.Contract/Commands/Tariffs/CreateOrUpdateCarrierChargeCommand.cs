using Cargo.Contract.DTOs.Tariffs;

namespace Cargo.Contract.Commands.Tariffs
{
    public class CreateOrUpdateCarrierChargeCommand : ICommand<CarrierChargeDto>
    {
        public CarrierChargeDto CarrierCharge { get; set; }
    }
}