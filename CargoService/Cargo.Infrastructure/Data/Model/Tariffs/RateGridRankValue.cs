namespace Cargo.Infrastructure.Data.Model.Tariffs
{
    public class RateGridRankValue
    {
        public int Id { get; set; }
        public int? TariffSolutionId { get; set; }
        public TariffSolution TariffSolution { get; set; }
        public uint Rank { get; set; }
        public decimal Value { get; set; }
    }
}
