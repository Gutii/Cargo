using Cargo.Contract.Queries;
using Directories.Contract.Commands;
using Directories.Contract.DTOs.Settings.BookingRules;
using IDeal.Common.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directories.Contract.Queries.Settings.BookingRules
{
    public class CarrierSettinParamQuery : IQuery<ICollection<CarrierParametersSettingsDto>>, IAuthenticatedMessage
    {
        public int? AgentId { get; set; }
        public int? GhaId { get; set; }
        public int CustomerId { get; set; }
        public int? CarrierId { get; set; }
        public string SelectedRoleNameEn { get; set; }
        public string Language { get; set; }
    }
}
