using IdealResults;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Cargo.ServiceHost
{
    public class FluentResultLogger : IResultLogger
    {
        ILogger<IResultLogger> logger;
        public FluentResultLogger(ILogger<IResultLogger> logger)
        {
            this.logger = logger;
        }

        public void Log(string context, string content, ResultBase result, LogLevel logLevel)
        {
            this.logger.Log(logLevel, "Result: {0} {1} <{2}>", result.Reasons.Select(reason => reason.Message), content, context);
        }

        public void Log<TContext>(string content, ResultBase result, LogLevel logLevel)
        {
            this.logger.Log(logLevel, "Result: {0} {1} <{2}>", result.Reasons.Select(reason => reason.Message), content, typeof(TContext).FullName);
        }
    }
}
