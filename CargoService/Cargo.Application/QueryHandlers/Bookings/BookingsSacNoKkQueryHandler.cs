using AutoMapper;
using Cargo.Contract.DTOs.Bookings;
using Cargo.Contract.Queries.Bookings;
using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Data.Model;
using IDeal.Common.Components.Paginator;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.QueryHandlers.Bookings
{
    public class BookingsSacNoKkQueryHandler : IQueryHandler<BookingsSacNoKkQuery, PagedResult<AwbLightDto>>
    {
        private readonly CargoContext cargoContext;
        private readonly IMapper mapper;

        public BookingsSacNoKkQueryHandler(CargoContext cargoContext, IMapper mapper)
        {
            this.cargoContext = cargoContext;
            this.mapper = mapper;
        }
        public async Task<PagedResult<AwbLightDto>> Handle(BookingsSacNoKkQuery request, CancellationToken cancellationToken)
        {
            var task = Task.Run(() =>
                {
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
                    .Where(b => b.Bookings.Any(x => x.SpaceAllocationCode == "NN") || b.Bookings.Any(x => x.SpaceAllocationCode == "--"));
                    bookings = string.IsNullOrEmpty(request.OrderBy) ? bookings.OrderByDescending(b => b.Bookings.FirstOrDefault().FlightSchedule.FlightDate) : bookings.OrderBy(request.OrderBy, request.Desc ?? false);
                    return bookings.Page(new PageInfo { PageIndex = request.PageIndex ?? 0, PageSize = request.PageSize ?? 20 });
                }, cancellationToken);
            return await mapper.Map<Task<PagedResult<AwbLightDto>>>(task);
        }
    }
}
