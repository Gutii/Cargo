using IDeal.Common.Components;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Cargo.Contract.DTOs
{
    public class FlightSheduleDto
    {
        public ulong Id { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public IataLocationDto OriginInfo { get; set; }
        public IataLocationDto DestinationInfo { get; set; }
        public string Number { get; set; }
        public DateTime? FlightDate { get; set; }
        public DateTime? StOrigin { get; set; }
        public DateTime? StDestination { get; set; }

        public virtual DateTime? StOriginLocal { get; set; }
        public virtual DateTime? StDestinationLocal { get; set; }

        /// <summary>
        /// Для STD (LOCAL) – для одноплечевых рейсов=0. Не будет равно нулю для многоплечевых рейсов для второго аэропорта, если произойдет смещение дат;
        /// </summary>
        public virtual int? StdOriginLocalDayChange { get; set; }

        /// <summary>
        /// Для STD (UTC) – отклонение даты вылета UTC от даты рейса по расписанию;
        /// </summary>
        public virtual int? StdOriginUtcDayChange { get; set; }

        /// <summary>
        /// Для STA (LOCAL) – отклонение даты прилета LOCAL от даты рейса по расписанию;
        /// </summary>
        public virtual int? StaDestLocalDayChange { get; set; }

        /// <summary>
        /// Для STA (UTC) – отклонение даты прилета LOCAL от даты рейса по расписанию;
        /// </summary>
        public virtual int? StaDestUtcDayChange { get; set; }

        public int? State { get; set; }
        public string AircraftRegistration { get; set; }
        public string AircraftType { get; set; }
        public string SHR { get; set; }
        public ulong? ProcentId { get; set; }

        public FlightCommPayloadInfoDto CommPayloadInfo { get; set; }

        public ICollection<MessageDto> Messages { get; set; }
        public double PayloadWeight { get; set; }
        public double PayloadVolume { get; set; }

        public FlightSaleState SaleState { get; set; }
        public override string ToString()
        {
            return $"{Number}/{FlightDate?.ToString("ddMMMyy", new CultureInfo("en-US")).ToUpper()}";
        }
    }
}
