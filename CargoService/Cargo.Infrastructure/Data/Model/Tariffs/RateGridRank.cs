namespace Cargo.Infrastructure.Data.Model.Tariffs
{
    public class RateGridRank
    {
        public ulong GridId { get; set; }
        public RateGridHeader Grid { get; set; }
        public uint Rank { get; set; }
    }
}