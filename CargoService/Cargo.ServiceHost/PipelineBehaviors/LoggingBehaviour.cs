using Cargo.Contract.Commands;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.ServiceHost.PipelineBehaviors
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class, ICommand<TResponse>
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;
        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            Type myType = request.GetType();
            string requestName = myType.Name;
            string unqiueId = Guid.NewGuid().ToString();
            string sProps = string.Join(" / ", myType.GetProperties().Select(p => $"{p.Name} : {p.GetValue(request, null)}"));
            _logger.LogInformation($"Обработка команды {requestName}. Id: {unqiueId}. Параметры: {sProps}");
            _logger.LogInformation(JsonSerializer.Serialize(request));
            var timer = new Stopwatch();
            timer.Start();
            var response = await next();
            timer.Stop();
            _logger.LogInformation($"Обработана команда {requestName}. Id: {unqiueId}. Затрачено {timer.ElapsedMilliseconds}");
            return response;
        }
    }
}
