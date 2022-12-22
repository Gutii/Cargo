using System;

namespace Cargo.Contract.DTOs
{

    /// <summary>
    /// Информация по загрузке рейса
    /// </summary>
    public class FlightCommPayloadInfoDto
    {
        /// <summary>
        /// Идентификатор рейса
        /// </summary>
        public ulong FlightId { get; set; }

        /// <summary>
        /// Рейс
        /// </summary>
        public FlightSheduleDto FlightShedule { get; set; }

        /// <summary>
        /// Номер рейса
        /// </summary>
        public string NumberFlight { get; set; }

        /// <summary>
        /// Дата рейса
        /// </summary>
        public DateTime? DateFlight { get; set; }

        /// <summary>
        /// план полезной загрузки
        /// </summary>
        public decimal WeightPlan { get; set; }

        /// <summary>
        /// объем полезной загрузки
        /// </summary>
        public decimal VolumePlan { get; set; }

        /// <summary>
        /// забранировано почтой вес
        /// </summary>
        public decimal MailWeight { get; set; }

        /// <summary>
        /// забранировано почтой объема
        /// </summary>
        public decimal MailVolume { get; set; }

        /// <summary>
        /// фактически забронировано вес
        /// </summary>
        public decimal WeightFact { get; set; }

        /// <summary>
        /// фактически забронировано объем
        /// </summary>
        public decimal VolumeFact { get; set; }

        /// <summary>
        /// полезный остаток по весу
        /// </summary>
        public decimal WeightRemain { get; set; }

        /// <summary>
        /// полезный остаток по объему
        /// </summary>
        public decimal VolumeRemain { get; set; }

        /// <summary>
        /// Коэффициент заполнения по объему
        /// </summary>
        public decimal VolumeFillFactor { get; set; }

        /// <summary>
        /// Коэффициент заполнения по весу
        /// </summary>
        public decimal WeightFillFactor { get; set; }
    }
}
