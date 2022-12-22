using System;
using System.Collections.Generic;

namespace Cargo.Contract.DTOs.Bookings
{
    public class AwbDto
    {
        public int? Id { get; set; }
        public string AcPrefix { get; set; }
        public string SerialNumber { get; set; }
        public int? PoolAwbNumId { get; set; }
        public int? ForwardingAgentId { get; set; }
        public string ForwardingAgent { get; set; }

        public string Origin { get; set; }
        public IataLocationDto OriginInfo { get; set; }
        public string Destination { get; set; }
        public IataLocationDto DestinationInfo { get; set; }

        /// <summary>
        /// Индикатор таможенной перевозки
        /// </summary>
        public string DiIndicator { get; set; }
        public string QuanDetShipmentDescriptionCode { get; set; }
        public int NumberOfPieces { get; set; }
        public string WeightCode { get; set; }
        public decimal Weight { get; set; }
        public decimal? ChargeWeight { get; set; }
        public string VolumeCode { get; set; }
        public decimal VolumeAmount { get; set; }
        public string ManifestDescriptionOfGoods { get; set; }
        public string ManifestDescriptionOfGoodsRu { get; set; }
        public string SpecialHandlingRequirements { get; set; }
        public string Product { get; set; }
        public string Status { get; set; }
        public string SpecialServiceRequest { get; set; }
        public string SpecialServiceRequestRu { get; set; }

        /// <summary>
        /// Место выпуска накладной
        /// </summary>
        public string PlaceOfIssue { get; set; }

        /// <summary>
        /// NCV
        /// </summary>
        public string NCV { get; set; }

        /// <summary>
        /// NDV
        /// </summary>
        public string NDV { get; set; }

        /// <summary>
        /// Валюта
        /// </summary>
        public string Currency { get; set; }

        public ICollection<BookingDto> Bookings { get; set; }

        /// <summary>
        /// Коллекция фиксированных броней (после этапа Груз сдан)
        /// </summary>
        public ICollection<BookingDto> BookingRcs { get; set; }
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Груотправитель
        /// </summary>
        public AwbContragentDto Consignor { get; set; }


        /// <summary>
        /// Грузополучатель
        /// </summary>
        public AwbContragentDto Consignee { get; set; }

        /// <summary>
        /// Агент
        /// </summary>
        public AwbContragentDto Agent { get; set; }

        public ICollection<SizeGroupDto> SizeGroups { get; set; }

        /// <summary>
        /// Код тариф. решения
        /// </summary>
        public string TariffsSolutionCode { get; set; }

        /// <summary>
        /// Канал продаж
        /// </summary>
        public string SalesChannel { get; set; }

        /// <summary>
        /// Порядок оплаты
        /// </summary>
        public string PaymentProcedure { get; set; }

        /// <summary>
        /// Вес к оплате – Chargeable или Actual
        /// </summary>
        public string WeightCharge { get; set; }

        /// <summary>
        /// ALL-in
        /// </summary>
        public bool AllIn { get; set; }

        /// <summary>
        /// Класс тарифа
        /// </summary>
        public string TariffClass { get; set; }

        /// <summary>
        /// Базовый размер тарифа
        /// </summary>
        public decimal BaseTariffRate { get; set; }

        /// <summary>
        /// Надбавка (Add-On)
        /// </summary>
        public string AddOn { get; set; }

        /// <summary>
        /// Итоговый размер тарифа
        /// </summary>
        public decimal TariffRate { get; set; }

        /// <summary>
        /// TOTAL (из раздела Сборы за перевозку)
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// TOTAL (из раздела TOTAL)
        /// </summary>
        public TotalChargeDto Charge { get; set; }

        /// <summary>
        /// Перевозчик
        /// </summary>
        public CustomerDto Carrier { get; set; }
        public int? CarrierId { get; set; }

        /// <summary>
        /// Дополнительные сборы
        /// </summary>
        public List<OtherChargeDto> OtherCharges { get; set; }
        public ICollection<MessageDto> Messages { get; set; }
        public List<HistoryDto> History { get; set; }
        
        /// <summary>
        /// Список тарифных групп
        /// </summary>
        public List<string> TariffGroups { get; set; }
    }
}
