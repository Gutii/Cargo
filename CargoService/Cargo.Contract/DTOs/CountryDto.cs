namespace Cargo.Contract.DTOs
{
    public class CountryDto
    {
        public int Id { get; set; }

        /// <summary>
        /// ISO 3166-1 alpha-2
        /// </summary>
        public string Alpha2 { get; set; }

        /// <summary>
        /// ISO 3166-1 alpha-3
        /// </summary>
        public string Alpha3 { get; set; }

        /// <summary>
        /// ISO 3166-1 numeric
        /// </summary>
        public string Numeric3 { get; set; }
        /// <summary>
        /// Краткая форма наименования на английском языке
        /// </summary>
        public string EnglishShortName { get; set; }

        /// <summary>
        /// Краткая форма наименования на русском языке
        /// </summary>
        public string RussianName { get; set; }
    }
}
