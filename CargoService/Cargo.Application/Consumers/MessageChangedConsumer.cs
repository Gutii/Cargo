using AutoMapper;
using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Data.Model;
using IDeal.Common.Messaging.Messages;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cargo.Application.CommandHandlers
{
    public class MessageChangedConsumer : IConsumer<MessageChanged>
    {
        ILogger<MessageChangedConsumer> logger;
        CargoContext dbContext;
        IMapper mapper;

        public MessageChangedConsumer(ILogger<MessageChangedConsumer> logger, CargoContext dbContext, IMapper mapper)
        {
            this.logger = logger;
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task Consume(ConsumeContext<MessageChanged> context)
        {
            if (context.CorrelationId == null)
            {
                throw new ArgumentNullException("Пропущен параметр для определения сообщения");
            }
            Message message = dbContext.Messages.Find(context.CorrelationId);

            bool isNew = message == null;
            message = mapper.Map(context.Message, message);
            message.Id = context.CorrelationId.Value;

            var carrier = dbContext.Carriers.AsNoTracking().FirstOrDefault(c => c.Id == (context.Message.CustomerId == 0 ? 55 : context.Message.CustomerId));
            message.Carrier = carrier?.IataCode ?? "SU";

            if (isNew)
            {
                dbContext.Messages.Add(message);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
