using IDeal.Common.Components;
using IdealResults;

namespace Cargo.Contract.Commands
{
    public class SaleStateCommand : ICommand<Result>
    {
        public ulong FlightId { get; set; }
        public FlightSaleState State { get; set; }
    }
}
