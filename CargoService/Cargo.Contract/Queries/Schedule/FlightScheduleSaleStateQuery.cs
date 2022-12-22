using Cargo.Contract.DTOs;
using IdealResults;

namespace Cargo.Contract.Queries.Schedule
{
    public class FlightScheduleSaleStateQuery : IQuery<Result<FlightSheduleDto>>
    {
        public ulong Id { get; set; }
    }
}
