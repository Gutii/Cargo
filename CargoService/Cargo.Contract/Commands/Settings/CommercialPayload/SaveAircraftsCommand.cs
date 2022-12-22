using Cargo.Contract.Commands;
using Cargo.Contract.DTOs.Settings;

namespace Cargo.Application.CommandHandlers.Settings
{
    public class SaveAircraftsCommand : ICommand<AircraftsDto>
    {
        public string IataCode { get; set; }
        public AircraftsDto AircraftsDto { get; set; }
    }
}
