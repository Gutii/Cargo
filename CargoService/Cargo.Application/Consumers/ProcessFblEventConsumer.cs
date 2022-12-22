using Cargo.Application.Services;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TimerServiceContract;

namespace Cargo.Application.CommandHandlers;

public class ProcessFblEventConsumer : IConsumer<TimerEvent>
{
    private readonly ILogger<ProcessFblEventConsumer> logger;
    private readonly FblService fblService;

    public ProcessFblEventConsumer(ILogger<ProcessFblEventConsumer> logger, FblService fblService)
    {
        this.logger = logger;
        this.fblService = fblService;
    }

    public async Task Consume(ConsumeContext<TimerEvent> context)
    {
        if (ulong.TryParse(context.Message.Body, out var flightId))
            await fblService.SendFbl(flightId);
        else
            logger.LogError("Неизвестный тип тела сообщения");
    }
}