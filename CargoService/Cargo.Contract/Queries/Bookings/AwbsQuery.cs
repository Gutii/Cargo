using Cargo.Contract.DTOs.Bookings;
using IDeal.Common.Components;
using IDeal.Common.Components.Paginator;
using System;

namespace Cargo.Contract.Queries.Bookings
{
    public class AwbsQuery : IQuery<PagedResult<AwbLightDto>>, IAuthenticatedMessage
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public int? FlightId { get; set; }

        /// <summary>
        /// Полный номер накладной
        /// </summary>
        public string AwbIdentification { get; set; }

        /// <summary>
        /// Фильтр по продукту
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// Нижняя граница даты создания накладной
        /// </summary>
        public DateTime? AwbCreateAt { get; set; }

        /// <summary>
        /// Верхняя граница даты создания накладной
        /// </summary>
        public DateTime? AwbCreateTo { get; set; }

        /// <summary>
        /// Пункт отправки груза по накладной
        /// </summary>
        public string AwbOrigin { get; set; }

        /// <summary>
        /// Пункт доставки груза по накладной
        /// </summary>
        public string AwbDestination { get; set; }

        /// <summary>
        /// Поле для сортировки
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// Порядок сортировки
        /// </summary>
        public bool? Desc { get; set; }

        public int? AgentId { get; set; }
        public int? GhaId { get; set; }
        public int? CarrierId { get; set; }
        public string Language { get; set; }
        public int CustomerId { get; set; }
        public string SelectedRoleNameEn { get; set; }
    }
}
