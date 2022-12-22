using MassTransit;
using System;
using System.Threading.Tasks;
using TelegramLogInterface;

namespace Cargo.Application.Services;

public class TelegrammService
{
    private readonly IBusControl _busControl;

    public TelegrammService(IBusControl busControl)
    {
        _busControl = busControl;
    }

    /// <summary>
    /// Отправка в лог бота простого сообщения
    /// </summary>
    /// <param name="message">Текст сообщения</param>
    public async Task Send(string message)
    {
        var endPoint = await _busControl.GetSendEndpoint(new Uri("queue:TelegramMessage")).ConfigureAwait(false);
        await endPoint.Send(new ErrorMessage { Message = message, StackTrace = string.Empty, Source = "CargoService" }).ConfigureAwait(false);
    }

    /// <summary>
    /// Отправка в лог бота сообщения об ошибке
    /// </summary>
    /// <param name="e">Объект ошибки</param>
    public async Task SendError(Exception e)
    {
        var endPoint = await _busControl.GetSendEndpoint(new Uri("queue:TelegramMessage")).ConfigureAwait(false);
        await endPoint.Send(new ErrorMessage { Message = e.Message, StackTrace = e.StackTrace ?? string.Empty, Source = "CargoService" }).ConfigureAwait(false);
    }
}