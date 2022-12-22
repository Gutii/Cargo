using IDeal.Common.Components;
using IdealResults;
using System;

namespace Cargo.Contract.Queries.Bookings
{
    /// <summary>
    /// Возвращает количество бронирований для статуса запроса
    /// </summary>
    public class BookingsSacNoKkQueryCount : IQuery<Result<int>>, IAuthenticatedMessage
    {

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
        /// Фильтр по нижней границе даты создания накладной
        /// </summary>
        public DateTime? AwbCreateAt { get; set; }

        /// <summary>
        /// Фильтр по верхней границе даты создания накладной
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

        public int? AgentId { get; set; }
        public int? GhaId { get; set; }
        public int? CarrierId { get; set; }
        public string Language { get; set; }
        public int CustomerId { get; set; }
        public string SelectedRoleNameEn { get; set; }
    }
}
