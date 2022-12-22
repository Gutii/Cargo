using System;
using System.Collections.Generic;
using System.Text;

namespace Cargo.Application.Services.Reports
{
    public class ReportHeaders
    {
        public enum Reports
        {
            /// <summary>
            /// Отчет «Бронирование на рейсах по авианакладным».
            /// </summary>
            BookingOnFlights,

            /// <summary>
            /// Отчет «Общее бронирование на рейсах».
            /// </summary>
            BookingsPerPeriod,

            /// <summary>
            /// Отчет «Стоки авианакладных»
            /// </summary>
            AwbStocks
        }

        public ReportHeaders(Reports report, string lang)
        {
            lang = lang.ToUpper();
            Names = report switch
            {
                Reports.AwbStocks => _bookingsPerPeriod[lang],
                Reports.BookingOnFlights => _bookingOnFlights[lang],
                Reports.BookingsPerPeriod => _awbStocks[lang],
                _ => throw new ArgumentOutOfRangeException(nameof(report), report, null)
            };
        }

        public string[] Names { get; }

        private Dictionary<string, string[]> _bookingOnFlights = new()
        {
            ["RU"] = new[]
            {
                new StringBuilder("Дата рейса").ToString(),
                new StringBuilder("STD").ToString(),
                new StringBuilder("№рейса").ToString(),
                new StringBuilder("Тип ВС").ToString(),
                new StringBuilder("А/П вылета").ToString(),
                new StringBuilder("А/П прилета").ToString(),
                new StringBuilder("Бронь общая (кг)").ToString(),
                new StringBuilder("Бронь общая (м3)").ToString(),
                new StringBuilder("ПКЗ рейса общее (кг)").ToString(),
                new StringBuilder("ПКЗ рейса общее (м3)").ToString(),
                new StringBuilder("% использ ПКЗ по весу").ToString(),
                new StringBuilder("% использ ПКЗ по объему").ToString()
            },
            ["EN"] = new[]
            {
                new StringBuilder("Flight date").ToString(),
                new StringBuilder("STD").ToString(),
                new StringBuilder("Flight Number").ToString(),
                new StringBuilder("A/C type").ToString(),
                new StringBuilder("Departure Airport").ToString(),
                new StringBuilder("Arrival Airport").ToString(),
                new StringBuilder("Total booking (kg)").ToString(),
                new StringBuilder("Total booking (m3)").ToString(),
                new StringBuilder("Payload Limit (kg)").ToString(),
                new StringBuilder("Payload Limit (m3)").ToString(),
                new StringBuilder("Payload Factor WGHT").ToString(),
                new StringBuilder("Payload Factor VOL").ToString()
            }
        };

        private Dictionary<string, string[]> _bookingsPerPeriod = new()
        {
            ["RU"] = new[]
            {
                new StringBuilder("№ AWB").ToString(),
                new StringBuilder("Аэропорт отправления AWB").ToString(),
                new StringBuilder("Аэропорт назначения AWB").ToString(),
                new StringBuilder("Мест по AWB").ToString(),
                new StringBuilder("Вес (кг) по AWB").ToString(),
                new StringBuilder("Объем (м3) по AWB").ToString(),
                new StringBuilder("№ рейса").ToString(),
                new StringBuilder("Дата рейса").ToString(),
                new StringBuilder("А/П вылета").ToString(),
                new StringBuilder("А/П прилета рейса").ToString(),
                new StringBuilder("Мест по AWB на рейсе").ToString(),
                new StringBuilder("Вес (кг) по AWB на рейсе").ToString(),
                new StringBuilder("Объем (м3) по AWB на рейсе").ToString(),
                new StringBuilder("Статус брони на рейсе").ToString(),
                new StringBuilder("Агент по AWB").ToString()
            },
            ["EN"] = new[]
            {
                new StringBuilder("#AWB").ToString(),
                new StringBuilder("AWB Origin").ToString(),
                new StringBuilder("AWB Destination").ToString(),
                new StringBuilder("AWB pieces").ToString(),
                new StringBuilder("AWB weight (kg)").ToString(),
                new StringBuilder("AWB volume (m3)").ToString(),
                new StringBuilder("Flight Number").ToString(),
                new StringBuilder("Flight date").ToString(),
                new StringBuilder("Departure Airport").ToString(),
                new StringBuilder("Arrival Airport").ToString(),
                new StringBuilder("AWB pcs on flight").ToString(),
                new StringBuilder("AWB wght (kg) on flight").ToString(),
                new StringBuilder("AWB volume (m3) on flight").ToString(),
                new StringBuilder("Booking status on flight").ToString(),
                new StringBuilder("AWB Agent").ToString()
            }
        };

        private Dictionary<string, string[]> _awbStocks = new()
        {
            ["RU"] = new[]
            {
                new StringBuilder("Стартовый номер AWB").ToString(),
                new StringBuilder("Конечный номер AWB").ToString(),
                new StringBuilder("Количество в стоке").ToString(),
                new StringBuilder("Количество использованных").ToString(),
                new StringBuilder("Количество доступных").ToString(),
                new StringBuilder("Агент по продажам").ToString(),
                new StringBuilder("№ договора").ToString(),
                new StringBuilder("Тип договора").ToString(),
                new StringBuilder("Срок истечения договора").ToString(),
            },
            ["EN"] = new[]
            {
                new StringBuilder("AWB Start Number").ToString(),
                new StringBuilder("AWB Last Number").ToString(),
                new StringBuilder("Stock Q-ty").ToString(),
                new StringBuilder("Used Q-ty").ToString(),
                new StringBuilder("Available Q-ty").ToString(),
                new StringBuilder("Sales Agent").ToString(),
                new StringBuilder("Contract Number").ToString(),
                new StringBuilder("SA/BSA").ToString(),
                new StringBuilder("Contract expiration date").ToString()
            }
        };
    }
}
