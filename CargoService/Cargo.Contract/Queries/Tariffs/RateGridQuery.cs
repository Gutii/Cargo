using Cargo.Contract.Common;

namespace Cargo.Contract.Queries.Tariffs
{
    public class RateGridQuery : IContragentSpecific
    {
        public int ContragentId { get; set; }
    }
}