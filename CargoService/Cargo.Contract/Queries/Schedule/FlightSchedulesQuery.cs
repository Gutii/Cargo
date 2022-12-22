using Cargo.Contract.DTOs;
using IDeal.Common.Components;
using IDeal.Common.Components.Paginator;
using System;

namespace Cargo.Contract.Queries.Schedule
{
    public class FlightSchedulesQuery : IQuery<PagedResult<FlightSheduleDto>>, IAuthenticatedMessage
    {
        public int? PageIndex { get; set; }

        public int? PageSize { get; set; }


        /// <summary>
        /// Откуда
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// Куда
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Код перевозчика рейса
        /// </summary>
        public string CarrierCode { get; set; }

        /// <summary>
        /// Номер рейса
        /// </summary>
        public string FlightNumber { get; set; }

        /// <summary>
        /// Дата рейса
        /// </summary>
        public DateTime? FlightDate { get; set; }

        /// <summary>
        /// Дата с
        /// </summary>
        public DateTime? DateFrom { get; set; }

        /// <summary>
        /// Дата по
        /// </summary>
        public DateTime? DateTo { get; set; }


        public int? AgentId { get; set; }
        public int? GhaId { get; set; }
        public int? CarrierId { get; set; }
        public string Language { get; set; }
        public int CustomerId { get; set; }
        public string SelectedRoleNameEn { get; set; }
    }
}
