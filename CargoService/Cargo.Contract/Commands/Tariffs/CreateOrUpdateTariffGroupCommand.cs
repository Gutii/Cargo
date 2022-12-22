using Cargo.Contract.Common;
using Cargo.Contract.DTOs;
using Cargo.Contract.DTOs.Tariffs;
using System.Collections.Generic;

namespace Cargo.Contract.Commands.Tariffs
{
    public class CreateOrUpdateTariffGroupCommand : ICommand<TariffGroupDto>, IContragentSpecific
    {
        public int Id { get; set; }

        public int ContragentId { get; set; }

        /// <summary>
        /// Код тарифной группы 
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Описание (рус.)
        /// </summary>
        public string DescriptionRus { get; set; }
        /// <summary>
        /// Описание (англ.)
        /// </summary>
        public string DescriptionEng { get; set; }

        public List<IataLocationDto> Airports { get; set; }
    }

    /*
    public class TariffAirportDto
    {
        public int Id { get; set; }

        /// <summary>
        /// IATA код
        /// </summary>
        public string IataCode { get; set; }

        /// <summary>
        /// Наименования (рус.)
        /// </summary>
        public string NameRus { get; set; }

        /// <summary>
        /// Наименование (англ.)
        /// </summary>
        public string NameEng { get; set; }
    }
    */
}
