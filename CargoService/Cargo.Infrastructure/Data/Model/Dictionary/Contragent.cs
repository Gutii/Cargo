using Cargo.Infrastructure.Data.Model.Dictionary.Settings;
using System.Collections.Generic;

namespace Cargo.Infrastructure.Data.Model.Dictionary
{
    public class Contragent
    {
        public int Id { get; set; }

        /// <summary>
        /// Международное название
        /// </summary>
        public string InternationalName { get; set; }
        public string NameEngAdditional { get; set; }

        /// <summary>
        /// Русский вариант
        /// </summary>
        public string RussianName { get; set; }
        public string NameRusAdditional { get; set; }

        /// <summary>
        /// Родное название 
        /// </summary>
        public string DomesticName { get; set; }

        public int? CountryId { get; set; }

        public Country Country { get; set; }
        public string PostCode { get; set; }

        public string StateCode { get; set; }
        public string StateProvince { get; set; }
        public string StreetAddressName { get; set; }
        public string Place { get; set; }

        public string StateProvinceRus { get; set; }
        public string PlaceRus { get; set; }
        public string StreetAddressNameRus { get; set; }

        public ICollection<ContactDetail> ContactDetails { get; set; }

        public ICollection<Agent> SalesAgent { get; set; }
        public Airline Carrier { get; set; }

        public string Login { get; set; }
        public string Pass { get; set; }

        public bool IsPhysic { get; set; }
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string OGRN { get; set; }
        public string AccountNumber { get; set; }
    }
}
