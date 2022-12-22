using System;

namespace Cargo.Contract.DTOs.Settings.CommercialPayload
{
    public class MailLimitsDto
    {
        /// <summary>
        /// Идентификатор типа ПКЗ
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Идентификатор перевозчика
        /// </summary>
        public string IataCode { get; set; }

        public int AircraftTypeId { get; set; }
        /// <summary>
        /// Тип воздушного судна
        /// </summary>
        public string AircraftType { get; set; }

        public int? AirlineId { get; set; }
        /// <summary>
        /// Авиакомпания
        /// </summary>
        public string Airline { get; set; }

        public int? FromIataLocationId { get; set; }
        /// <summary>
        /// Пункт отправления
        /// </summary>
        public string FromIataLocation { get; set; }


        public int? InIataLocationId { get; set; }
        /// <summary>
        /// Пункт назначения
        /// </summary>
        public string InIataLocation { get; set; }

        /// <summary>
        /// Номер рейса
        /// </summary>
        public string FlightNumber { get; set; }

        /// <summary>
        /// Начало действия
        /// </summary>
        public DateTime? DateStart { get; set; }

        /// <summary>
        /// Окончание действия
        /// </summary>
        public DateTime? DateEnd { get; set; }

        public string AccseptedShr { get; set; }

        /// <summary>
        /// Weight
        /// </summary>
        public int? Weight { get; set; }

        /// <summary>
        /// Volume
        /// </summary>
        public int? Volume { get; set; }
    }
}
