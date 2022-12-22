namespace Cargo.Infrastructure.Data.Model
{
    public class AwbContragent
    {
        public ulong Id { get; set; }

        /// <summary>
        /// Наименование (RU)
        /// </summary>
        public string NameRu { get; set; }

        /// <summary>
        /// Наименование (EN)
        /// </summary>
        public string NameEn { get; set; }

        /// <summary>
        /// Доп Наименование (RU)
        /// </summary>
        public string NameExRu { get; set; }

        /// <summary>
        /// Доп Наименование (EN)
        /// </summary>
        public string NameExEn { get; set; }

        /// <summary>
        /// Город (RU)
        /// </summary>
        public string CityRu { get; set; }

        /// <summary>
        /// Город (EN)
        /// </summary>
        public string CityEn { get; set; }

        /// <summary>
        /// Код страны ISO
        /// </summary>
        public string CountryISO { get; set; }

        /// <summary>
        /// Индекс
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// Паспорт
        /// </summary>
        public string Passport { get; set; }

        /// <summary>
        /// Регион (RU)
        /// </summary>
        public string RegionRu { get; set; }

        /// <summary>
        /// Регион (EN)
        /// </summary>
        public string RegionEn { get; set; }

        /// <summary>
        /// Код (EN)
        /// </summary>
        public string CodeEn { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Факс
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// Адрес (RU)
        /// </summary>
        public string AddressRu { get; set; }

        /// <summary>
        /// Адрес (EN)
        /// </summary>
        public string AddressEn { get; set; }

        /// <summary>
        /// Preview (RU)
        /// </summary>
        public string PreviewRu { get; set; }

        /// <summary>
        /// Preview (EN)
        /// </summary>
        public string PreviewEn { get; set; }

        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email { get; set; }

        public string IataCode { get; set; }
        public string AgentCass { get; set; }
    }
}
