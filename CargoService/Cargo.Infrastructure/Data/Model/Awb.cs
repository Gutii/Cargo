using Cargo.Infrastructure.Data.Model.Dictionary;
using Cargo.Infrastructure.Data.Model.Settings;
using Cargo.Infrastructure.Data.Model.Settings.PoolAwbs;
using System;
using System.Collections.Generic;

namespace Cargo.Infrastructure.Data.Model
{
    public class Awb
    {
        public int Id { get; set; }
        public string AcPrefix { get; set; }
        public string SerialNumber { get; set; }
        public int? PoolAwbId { get; set; }
        public AgentContractPoolAwb PoolAwb { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string QuanDetShipmentDescriptionCode { get; set; }
        public int NumberOfPieces { get; set; }
        public string WeightCode { get; set; }
        public decimal Weight { get; set; }
        public decimal ChargeWeight { get; set; }
        public string VolumeCode { get; set; }
        public decimal VolumeAmount { get; set; }
        public string ManifestDescriptionOfGoods { get; set; }
        public string ManifestDescriptionOfGoodsRu { get; set; }
        public string SpecialHandlingRequirements { get; set; }
        public string SpecialServiceRequest { get; set; }
        public string SpecialServiceRequestRu { get; set; }
        public string Product { get; set; }
        public string Status { get; set; }

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

        /// <summary>
        /// Место выпуска накладной
        /// </summary>
        public string PlaceOfIssue { get; set; }

        public ICollection<Booking> Bookings { get; set; }
        public ICollection<BookingRcs> BookingRcs { get; set; }
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Грузотправитель
        /// </summary>
        public AwbContragent Consignor { get; set; }
        public ulong? ConsignorId { get; set; }


        /// <summary>
        /// Грузополучатель
        /// </summary>
        public AwbContragent Consignee { get; set; }
        public ulong? ConsigneeId { get; set; }

        /// <summary>
        /// Агент
        /// </summary>
        public Contragent Agent { get; set; }
        public int? AgentId { get; set; }

        /// <summary>
        /// Перевозчик
        /// </summary>
        public Customer Carrier { get; set; }
        public int? CarrierId { get; set; }

        public ICollection<SizeGroup> SizeGroups { get; set; }

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
        /// Столбец Prepaid
        /// </summary>
        public TaxCharge Prepaid { get; set; }
        public ulong? PrepaidId { get; set; }

        /// <summary>
        /// Столбец Collect
        /// </summary>
        public TaxCharge Collect { get; set; }
        public ulong? CollectId { get; set; }

        public ICollection<OtherCharge> OtherCharges { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<ConsignmentStatus> Tracking { get; set; }
    }
}
