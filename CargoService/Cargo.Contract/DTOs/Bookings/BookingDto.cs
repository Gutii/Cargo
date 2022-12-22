using System;
using System.Collections.Generic;

namespace Cargo.Contract.DTOs.Bookings
{
    public class BookingDto
    {
        public int? Id { get; set; }

        /// <summary>
        /// Порядковый номер текущей брони в коллекции Awb
        /// </summary>
        public int Seq { get; set; }
        public string QuanDetShipmentDescriptionCode { get; set; }
        public int NumberOfPieces { get; set; }
        public string WeightCode { get; set; }
        public decimal Weight { get; set; }
        public string VolumeCode { get; set; }
        public decimal VolumeAmount { get; set; }
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// KK, LL, NN
        /// </summary>
        public string SpaceAllocationCode { get; set; }
        public FlightSheduleDto FlightSchedule { get; set; }

        public BookingDto PrevRouting { get; set; }
        public ICollection<BookingDto> NextRoutings { get; set; }

        /// <summary>
        /// Пункт отправки
        /// </summary>
        public string AwbOrigin { get; set; }

        /// <summary>
        /// Пункт назначения
        /// </summary>
        public string AwbDestination { get; set; }


    }
}
