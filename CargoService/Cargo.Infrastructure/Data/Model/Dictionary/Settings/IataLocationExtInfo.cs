using System;
using System.Collections.Generic;

namespace Cargo.Infrastructure.Data.Model.Dictionary.Settings
{
    /// <summary>
    /// дополнительные данные по аэропорту
    /// </summary>
    public class IataLocationExtInfo
    {
        public int IataLocationId { get; set; }

        /// <summary>
        /// Наименование местоположения: аэропорт или городской округ 
        /// </summary>
        public string CityRusName { get; set; }

        /// <summary>
        /// Часовой пояс (лето)
        /// </summary>
        public TimeSpan TimeZoneSummer { get; set; }

        /// <summary>
        /// Часовой пояс (зима)
        /// </summary>
        public TimeSpan TimeZoneWinter { get; set; }

        public string Remarks { get; set; }

        public IataLocation IataLocation { get; set; }

        public ICollection<AirportContactInformation> AdditionalContactInfo { get; set; }

        public ICollection<SlaTimeLimitation> SlaTimeLimitations { get; set; }

        public ICollection<SlaProhibition> SlaProhibitions { get; set; }

        public ICollection<TelexSetting> TelexSettings { get; set; }
    }



    public class TelexSetting
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string OffsetBase { get; set; }
        public int? OffsetValue { get; set; }
        public string Emails { get; set; }

        public int IataLocationId { get; set; }
        public IataLocationExtInfo IataLocationExtInfo { get; set; }
    }

    public class SlaProhibition
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Shr { get; set; }
        public string MvlVvl { get; set; }
        public bool Import { get; set; }
        public bool Export { get; set; }
        public bool Transfer { get; set; }
        public bool Transit { get; set; }

        public int IataLocationId { get; set; }
        public IataLocationExtInfo IataLocationExtInfo { get; set; }
    }

    public class SlaTimeLimitation
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public string Shr { get; set; }

        public string MvlVvl { get; set; }

        public int Time { get; set; }

        public int IataLocationId { get; set; }
        public IataLocationExtInfo IataLocationExtInfo { get; set; }
    }

    public class AirportContactInformation
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }

        public string Phone1 { get; set; }
        public string Phone2 { get; set; }

        public string Email1 { get; set; }
        public string Email2 { get; set; }

        public int IataLocationId { get; set; }
        public IataLocationExtInfo IataLocationExtInfo { get; set; }
    }
}
