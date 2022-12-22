using Cargo.Contract.DTOs.Bookings;
using IDeal.Common.Components;
using IdealResults;

namespace Cargo.Contract.Commands
{
    /// <summary>
    /// Создание новой или сохранение существующей накладной
    /// </summary>
    public class SaveAwbCommand : ICommand<Result<int>>, IAuthenticatedMessage
    {
        /// <inheritdoc/>
        public AwbDto Awb { get; set; }
        public string Status { get; set; }

        public int? AgentId { get; set; }
        public int? GhaId { get; set; }
        public int? CarrierId { get; set; }
        public string Language { get; set; }
        public int CustomerId { get; set; }
        public string SelectedRoleNameEn { get; set; }
    }
}