using Cargo.Contract.DTOs.Settings;

namespace Cargo.Contract.Commands.Settings
{
    public class SaveCommPayloadCommand : ICommand<CommPayloadDto>
    {
        public string IataCode { get; set; }
        public CommPayloadDto CommPayload { get; set; }
    }
}
