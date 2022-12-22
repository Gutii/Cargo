using Cargo.Application.Services.Reports;
using Cargo.Contract.Queries.Report;
using IDeal.Common.Components;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Cargo.ServiceHost.Controllers;

[Authorize]
[Route("Api/[controller]/V1")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly ReportService reportService;

    public ReportController(IMediator mediator, ReportService reportService)
    {
        this.reportService = reportService;
        this.mediator = mediator;
    }

    /// <summary>
    /// Отчет «Общее бронирование на рейсах»
    /// </summary>
    /// <param name="query"></param>
    [MessageInjector]
    [HttpPost(nameof(ReportBookingOnFlights))]
    public async Task<byte[]> ReportBookingOnFlights([FromQuery] AuthenticatedMessage q) =>
        await reportService.BookingOnFlights(q.beginDate, q.endinDate, q.AgentId, q.CarrierId, q.Language);


    /// <summary>
    /// Отчет «Бронирование на рейсах по авианакладным»
    /// </summary>
    /// <param name="query"></param>
    [MessageInjector]
    [HttpPost(nameof(GetReportBookingsPerPeriod))]
    public async Task<byte[]> GetReportBookingsPerPeriod([FromQuery] AuthenticatedMessage q) =>
        await reportService.BookingsPerPeriod(q.beginDate, q.endinDate, q.AgentId, q.CarrierId, q.Language);

    /// <summary>
    /// Получить бланк FWB (в байтах) по ID накладной
    /// </summary>
    /// <param name="query"></param>
    [HttpPost(nameof(FwbBlankByAwbId))]
    public async Task<byte[]> FwbBlankByAwbId([FromBody] ReportFwbBlankByAwbIdQuery query) =>
        await mediator.Send(query);
}

public class AuthenticatedMessage : IAuthenticatedMessage
{
    public DateTime beginDate { get; set; }
    public DateTime endinDate { get; set; }
    public int? AgentId { get; set; }
    public int? GhaId { get; set; }
    public int? CarrierId { get; set; }
    public string SelectedRoleNameEn { get; set; }
    public string Language { get; set; }
    public int CustomerId { get; set; }
}