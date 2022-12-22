using IDeal.Common.Components;
using IdealResults;

namespace Cargo.Contract.Commands
{
    public class ReserveAwbNumberCommand : ICommand<Result<int>>, IAuthenticatedMessage
    {
        /// <summary>
        /// Полный номер накладной: AcPrefix + AwbNumber (195-54345663).
        /// </summary>
        public string AwbIdentifier { get; set; }
        public int? AgentId { get; set; }
        public int? GhaId { get; set; }
        public int? CarrierId { get; set; }
        public string Language { get; set; }
        public int CustomerId { get; set; }
        public string SelectedRoleNameEn { get; set; }
    }
}
