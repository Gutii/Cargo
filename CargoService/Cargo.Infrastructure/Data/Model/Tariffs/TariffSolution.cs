using Cargo.Infrastructure.Data.Model.Dictionary;
using System;
using System.Collections.Generic;

namespace Cargo.Infrastructure.Data.Model.Tariffs
{
    /// <summary>
    /// Тарифные решения
    /// </summary>
    public class TariffSolution
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


        //public int? AwbOriginAirportId { get; set; }
        /// <summary>
        /// Аэропорт отправления авианакладной
        /// </summary>
        public IataLocation AwbOriginAirport { get; set; }

        //public int? AwbDestinationAirportId { get; set; }
        /// <summary>
        /// Аэропорт назначения авианакладной
        /// </summary>
        public IataLocation AwbDestinationAirport { get; set; }

        //public int? AwbOriginTariffGroupId { get; set; }
        /// <summary>
        /// Тарифная группа отправления авианакладной
        /// </summary>
        public TariffGroup AwbOriginTariffGroup { get; set; }

        //public int? AwbDestinationTariffGroupId { get; set; }
        /// <summary>
        /// Тарифная группа назначения авианакладной
        /// </summary>
        public TariffGroup AwbDestinationTariffGroup { get; set; }

        //public int? TransitAirportId { get; set; }
        /// <summary>
        /// Аэропорт транзита
        /// </summary>
        public IataLocation TransitAirport { get; set; }

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

        //public int? RateGridHeaderId { get; set; }
        /// <summary>
        /// Вид тарифной сетки
        /// </summary>
        public RateGridHeader RateGrid { get; set; }

        /// <summary>
        /// Мин тариф
        /// </summary>
        public decimal MinTariff { get; set; }

        /// <summary>
        /// Значения тарифной сетки
        /// </summary>
        public List<RateGridRankValue> RateGridRankValues { get; set; }

        /// <summary>
        /// Надбавка к тарифу на перевозку согласно тарифному решению
        /// </summary>
        public List<TariffAddon> Addons { get; set; }
    }
}