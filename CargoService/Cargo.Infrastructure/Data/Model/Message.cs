using System;

namespace Cargo.Infrastructure.Data.Model
{
    /// <summary>
    /// Заголовки сообщений
    /// </summary>
    public class Message
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Тип Iata-сообщения : FBL, FWB, ...
        /// </summary>
        public string MessageIdentifier { get; set; }

        /// <summary>
        /// Тип Iata-сообщения : FBL, FWB, ...
        /// </summary>
        public ushort? VersionNumber { get; set; }

        /// <summary>
        /// Входящее, исходящее
        /// </summary>
        public string Direction { get; set; }

        public DateTime DateCreated { get; set; }

        public Guid? LinkedNtId { get; set; }

        /// <summary>
        /// Отправитель
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Получатель
        /// </summary>
        public string To { get; set; }
        public string Carrier { get; set; }

        public int? LinkedAwbId { get; set; }
        public Awb LinkedAwb { get; set; }
        public ulong? LinkedFlightId { get; set; }
        public FlightShedule LinkedFlight { get; set; }
    }
}
