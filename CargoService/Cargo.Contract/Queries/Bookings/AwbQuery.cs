using Cargo.Contract.DTOs.Bookings;
using IdealResults;

namespace Cargo.Contract.Queries.Bookings
{
    public class AwbQuery : IQuery<Result<AwbDto>>
    {
        public int? Id { get; set; }
        public string AwbIdentifier { get; set; }
    }
}
