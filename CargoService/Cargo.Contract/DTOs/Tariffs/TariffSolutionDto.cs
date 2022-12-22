using Cargo.Contract.Common;
using System;
using System.Collections.Generic;

namespace Cargo.Contract.DTOs.Tariffs
{
    /// <summary>
    /// Справочник тарифных групп
    /// </summary>
    public class TariffSolutionDto : IContragentSpecific
    {
        public int Id { get; set; }

        public int ContragentId { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// дата начала действия тарифа
        /// </summary>
        public DateTime ValidationDate { get; set; }

        /// <summary>
        /// Код (номер)
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Зона действия
        /// </summary>
        public string CoverageArea { get; set; }

        /// <summary>
        /// Валюта тарифа
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// дата начала действия тарифа
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// дата окончания действия тарифа
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Вид тарифного решения
        /// - public (публичный)
        /// - special(специальный)
        /// </summary>
        public bool IsSpecial { get; set; }

        /// <summary>
        /// Канал продаж
        /// </summary>
        public string SalesChannel { get; set; }

        /// <summary>
        /// IATA код агента
        /// </summary>
        public string IataAgentCode { get; set; }

        /// <summary>
        /// Номер клиента
        /// </summary>
        public string ClientNumber { get; set; }

        /// <summary>
        /// Наименование клиента
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Продукт
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// ORD/SALE/PREM (ORDINAR/SALE/PREMIUM) - выбор вида периода применения тарифного решения
        /// </summary>
        public string PeriodType { get; set; }

        /// <summary>
        /// Аэропорт отправления авианакладной
        /// </summary>
        public IataLocationDto AwbOriginAirport { get; set; }

        /// <summary>
        /// Аэропорт назначения авианакладной
        /// </summary>
        public IataLocationDto AwbDestinationAirport { get; set; }

        /// <summary>
        /// Тарифная группа отправления авианакладной
        /// </summary>
        public TariffGroupDto AwbOriginTariffGroup { get; set; }

        /// <summary>
        /// Тарифная группа назначения авианакладной
        /// </summary>
        public TariffGroupDto AwbDestinationTariffGroup { get; set; }

        /// <summary>
        /// Аэропорт транзита
        /// </summary>
        public IataLocationDto TransitAirport { get; set; }

        /*
        /// <summary>
        /// Рейсы
        /// </summary>
        public string Flights { get; set; }

        /// <summary>
        /// Маршруты
        /// </summary>
        public string Routes { get; set; }

        /// <summary>
        /// Дни недели
        /// </summary>
        public string WeekDays { get; set; }
        */

        /// <summary>
        /// Порядок оплаты – Prepaid или Сollect
        /// </summary>
        public string PaymentTerms { get; set; }

        /// <summary>
        /// Вес к оплате – Chargeable или Actual
        /// </summary>
        public string WeightCharge { get; set; }

        /// <summary>
        /// All-in
        /// </summary>
        public bool IsAllIn { get; set; }

        /// <summary>
        /// Вид тарифа:
        /// - плоский
        /// - весовой
        /// </summary>
        public string TariffType { get; set; }

        public int? RateGridHeaderId { get; set; }
        /// <summary>
        /// Вид тарифной сетки
        /// </summary>
        public RateGridDto RateGrid { get; set; }

        /// <summary>
        /// Мин тариф
        /// </summary>
        public decimal MinTariff { get; set; }

        /// <summary>
        /// Значения тарифной сетки
        /// </summary>
        public List<RateGridRankValueDto> RateGridRankValues { get; set; }

        /// <summary>
        /// Надбавка к тарифу на перевозку согласно тарифному решению
        /// </summary>
        public List<TariffAddonDto> Addons { get; set; }
    }
}