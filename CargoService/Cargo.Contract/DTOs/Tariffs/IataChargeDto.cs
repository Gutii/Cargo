namespace Cargo.Contract.DTOs.Tariffs
{
    public class IataChargeDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public string DescriptionEng { get; set; }
        public string DescriptionRus { get; set; }
    }
}