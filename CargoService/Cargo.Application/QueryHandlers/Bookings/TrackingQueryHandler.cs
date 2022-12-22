using AutoMapper;
using Cargo.Contract.DTOs;
using Cargo.Contract.Queries.Bookings;
using Cargo.Infrastructure.Data;
using IDeal.Common.Components.Paginator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.QueryHandlers.Bookings
{
    public class TrackingQueryHandler : IQueryHandler<TrackingQuery, PagedResult<ConsignmentStatusDto>>
    {
        CargoContext CargoContext;
        IMapper mapper;
        ILogger<TrackingQueryHandler> logger;

        public TrackingQueryHandler(CargoContext CargoContext, IMapper mapper, ILogger<TrackingQueryHandler> logger)
        {
            this.CargoContext = CargoContext;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<PagedResult<ConsignmentStatusDto>> Handle(TrackingQuery request, CancellationToken cancellationToken)
        {
            Task<PagedResult<ConsignmentStatusDto>> task = Task.Run(() =>
            {
                var trackings = this.CargoContext
                .ConsignmentStatuses
                .Include(cs => cs.Awb)
                .AsNoTracking()
                .Where(cs => request.Id == cs.AwbId)
                .Select(cs => new { Id = cs.Id, AcPrefix = cs.Awb.AcPrefix, SerialNumber = cs.Awb.SerialNumber, DateChange = cs.DateChange, AirportCode = cs.AirportCode, FlightDate = cs.FlightDate, FlightNumber = cs.FlightNumber, Source = cs.Source, StatusCode = cs.StatusCode, NumberOfPiece = cs.NumberOfPiece, cs.Weight, cs.VolumeAmount, cs.TitleRu, cs.TitleEn }).OrderByDescending(s => s.DateChange).ToList();

                var trackingsDto = trackings
                    .Select(t =>
                    {
                        ConsignmentStatusDto csd = new ConsignmentStatusDto
                        {
                            Id = t.Id,
                            Source = t.Source,
                            DateChange = t.DateChange,
                            StatusCode = t.StatusCode,
                            AirportCode = t.AirportCode,
                            AwbIdentifier = string.Concat(t.AcPrefix, "-", t.SerialNumber),
                            TitleRu = t.TitleRu,
                            TitleEn = t.TitleEn
                        };

                        if (t.FlightDate.HasValue && !string.IsNullOrEmpty(t.FlightNumber))
                        {
                            csd.MovementDetail = new MovementDetailDto { FlightDate = t.FlightDate.Value, FlightNumber = t.FlightNumber };
                        }

                        if (t.NumberOfPiece > 0 && t.Weight.HasValue && t.VolumeAmount.HasValue)
                        {
                            csd.QuantityDetail = new QuantityDetailDto { NumberOfPiece = t.NumberOfPiece, Weight = t.Weight.Value, VolumeAmount = t.VolumeAmount.Value };
                        }

                        return csd;
                    }).Page(new PageInfo { PageIndex = request.PageIndex ?? 0, PageSize = request.PageSize ?? 20 });

                return trackingsDto;
            }, cancellationToken);

            return await task;
        }
    }
}