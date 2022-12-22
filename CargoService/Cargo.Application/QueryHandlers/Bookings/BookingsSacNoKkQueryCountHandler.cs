using Cargo.Contract.Queries.Bookings;
using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Data.Model;
using IdealResults;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.QueryHandlers.Bookings
{
    public class BookingsSacNoKkQueryCountHandler : IQueryHandler<BookingsSacNoKkQueryCount, Result<int>>
    {
        readonly CargoContext cargoContext;
        public BookingsSacNoKkQueryCountHandler(CargoContext cargoContext) => this.cargoContext = cargoContext;
        public async Task<Result<int>> Handle(BookingsSacNoKkQueryCount request, CancellationToken cancellationToken)
        {
            var task = Task.Run(() =>
            {
                /*IQueryable<Booking> query = this.cargoContext.Bookings
                .Include(b => b.FlightSchedule)
                .Include(b => b.Awb);
                var bookings = query
                    .Where(b => request.AgentId == null || b.Awb.AgentId == request.AgentId) // по агенту
                    .Where(b => request.CustomerId == 0 || b.Awb.CarrierId == request.CustomerId) // по перевозчику
                    .Where(b => request.FlightId == null || b.FlightScheduleId == (ulong)request.FlightId)
                    .Where(b => string.IsNullOrEmpty(request.AwbIdentification) || EF.Functions.Like(string.Concat(b.Awb.AcPrefix, "-", b.Awb.SerialNumber), $"{request.AwbIdentification}%"))//TODO: Ограничить длину поля и добавить индекс
                    .Where(b => string.IsNullOrEmpty(request.Product) || EF.Functions.Like(string.Concat(b.Awb.Product), $"{request.Product}%"))//TODO: Ограничить длину поля и добавить индекс
                    .Where(b => string.IsNullOrEmpty(request.AwbOrigin) || request.AwbOrigin == b.Awb.Origin)
                    .Where(b => string.IsNullOrEmpty(request.AwbDestination) || request.AwbDestination == b.Awb.Destination)
                    .Where(b => !request.AwbCreateAt.HasValue || request.AwbCreateAt <= b.Awb.CreatedDate)
                    .Where(b => !request.AwbCreateTo.HasValue || request.AwbCreateTo >= b.Awb.CreatedDate)
                    .Where(b => b.SpaceAllocationCode != "KK" && b.SpaceAllocationCode != "--")*/
                IQueryable<Awb> query = cargoContext.Awbs
                    .Include(b => b.Bookings)
                    .ThenInclude(f => f.FlightSchedule)
                    .Include(b => b.Agent);

                var bookings = query
                    .Where(b => request.AgentId == null || b.AgentId == request.AgentId) // по агенту
                        .Where(b => request.CustomerId == 0 || b.CarrierId == request.CustomerId) // по перевозчику
                        .Where(b => request.FlightId == null || b.Bookings.Any(f => f.FlightScheduleId == (ulong)request.FlightId))
                        .Where(b => string.IsNullOrEmpty(request.AwbIdentification) || EF.Functions.Like(string.Concat(b.AcPrefix, "-", b.SerialNumber), $"{request.AwbIdentification}%"))//TODO: Ограничить длину поля и добавить индекс
                        .Where(b => string.IsNullOrEmpty(request.Product) || EF.Functions.Like(string.Concat(b.Product), $"{request.Product}%"))//TODO: Ограничить длину поля и добавить индекс
                        .Where(b => string.IsNullOrEmpty(request.AwbOrigin) || request.AwbOrigin == b.Origin)
                        .Where(b => string.IsNullOrEmpty(request.AwbDestination) || request.AwbDestination == b.Destination)
                        .Where(b => !request.AwbCreateAt.HasValue || request.AwbCreateAt <= b.CreatedDate)
                        .Where(b => !request.AwbCreateTo.HasValue || request.AwbCreateTo >= b.CreatedDate)
                        .Where(b => b.Bookings.Any(x => x.SpaceAllocationCode == "NN") || b.Bookings.Any(x => x.SpaceAllocationCode == "--"))
                    .AsNoTracking()
                    .Count();
                var result = Result.Ok(bookings);
                return result;
            }, cancellationToken);
            return await task;
        }
    }
}
