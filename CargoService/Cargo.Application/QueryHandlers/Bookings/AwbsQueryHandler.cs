using AutoMapper;
using Cargo.Contract.DTOs.Bookings;
using Cargo.Contract.Queries.Bookings;
using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Data.Model;
using IDeal.Common.Components.Paginator;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.QueryHandlers.Bookings
{
    public class AwbsQueryHandler : IQueryHandler<AwbsQuery, PagedResult<AwbLightDto>>
    {
        CargoContext CargoContext;
        IMapper mapper;

        public AwbsQueryHandler(CargoContext CargoContext, IMapper mapper)
        {
            this.CargoContext = CargoContext;
            this.mapper = mapper;
        }

        public async Task<PagedResult<AwbLightDto>> Handle(AwbsQuery request, CancellationToken cancellationToken)
        {
            var task = Task.Run(() =>
            {
                IQueryable<Awb> query = CargoContext.Awbs;
                var awbs = query.AsNoTracking()
                    .Include(c => c.Agent)
                    .Where(a => request.AgentId == null || a.AgentId == request.AgentId) // по агенту
                    .Where(a => request.CustomerId == 0 || a.CarrierId == request.CustomerId) // по перевозчику
                    .Where(a => request.FlightId == null || a.Bookings.Any(b => b.FlightScheduleId == (ulong)request.FlightId))
                    .Where(a => string.IsNullOrEmpty(request.AwbIdentification) || EF.Functions.Like(string.Concat(a.AcPrefix, "-", a.SerialNumber), $"{request.AwbIdentification}%"))
                    .Where(a => string.IsNullOrEmpty(request.Product) || EF.Functions.Like(string.Concat(a.Product), $"{request.Product}%")).Where(a => string.IsNullOrEmpty(request.AwbOrigin) || request.AwbOrigin == a.Origin)
                    .Where(a => string.IsNullOrEmpty(request.AwbDestination) || request.AwbDestination == a.Destination)
                    .Where(a => !request.AwbCreateAt.HasValue || request.AwbCreateAt <= a.CreatedDate)
                    .Where(a => !request.AwbCreateTo.HasValue || request.AwbCreateTo >= a.CreatedDate)
                    .OrderByDescending(a => a.CreatedDate)
                    .Page(new PageInfo { PageIndex = request.PageIndex ?? 0, PageSize = request.PageSize ?? 20 });

                var awbIds = awbs.Items.Select(item => item.Id).ToArray();
                var bookings = CargoContext.Bookings.Include(b => b.FlightSchedule)
                    .Where(b => awbIds.Contains(b.AwbId));
                awbs.Items.GroupJoin(bookings, a => a.Id, b => b.AwbId, (a, bs) => { return a.Bookings = bs.OrderByDescending(b => b.FlightSchedule?.FlightDate).ToList(); }).ToList();
                return awbs;
            }, cancellationToken);
            return await mapper.Map<Task<PagedResult<AwbLightDto>>>(task);
        }
    }
}
