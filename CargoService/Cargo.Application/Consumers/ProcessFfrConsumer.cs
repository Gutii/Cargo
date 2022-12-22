using AutoMapper;
using Cargo.Application.Services;
using Cargo.Application.Validation;
using Cargo.Infrastructure.Data.Model;
using IDeal.Common.Components.Messages.ObjectStructures.Fnas.Ver1;
using IDeal.Common.Messaging.Messages;
using IdealResults;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cargo.Application.CommandHandlers
{
    public class ProcessFfrConsumer : IConsumer<ProcessFfr>
    {
        private IMapper mapper;
        private ILogger<ProcessFfrConsumer> logger;
        private AwbService awbBookingsService;
        private SettingsService settingsService;
        private ProcessFfrValidator processFfrValidator;
        public ProcessFfrConsumer(
            ILogger<ProcessFfrConsumer> logger,
            IMapper mapper, AwbService awbBookingsService,
            ProcessFfrValidator processFfrValidator,
            IMediator mediator,
            SettingsService settingsService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.awbBookingsService = awbBookingsService;
            this.processFfrValidator = processFfrValidator;
            this.settingsService = settingsService;
        }

        public async Task Consume(ConsumeContext<ProcessFfr> context)
        {
            var validationResult = processFfrValidator.Validate(context.Message);
            if (!validationResult.IsValid)
            {
                var fna = new Fna()
                {
                    StandardMessageIdentification = new StandardMessageIdentification() { StandardMessageIdentifier = "FNA", MessageTypeVersionNumber = "1" },
                    ReceivedMessageDetail = context.Message.StandardMessageText,
                    ReasonForRejectionError = validationResult.Errors
                    .Select(x => Translit(x.ErrorMessage ?? string.Empty).ToUpper())
                        .Select(errorMessage => new ReasonForRejectionError() { ReasonForRejectionErrorInner = errorMessage })
                        .ToList()
                };

                logger.LogInformation("Oтправляем FNA: {0}", string.Join("/", validationResult.Errors));
                await context.Publish<BuildMessage>(new { __CorrelationId = Guid.NewGuid(), Fna = fna, CustomerId = 55 });
            }
            else
            {
                int awbId = 0;
                string awbIdentifier = string.Concat(context.Message.Ffr.ConsignmentDetail.AwbIdentification.AirlinePrefix, "-", context.Message.Ffr.ConsignmentDetail.AwbIdentification.AwbSerialNumber);
                Result<Awb> awbOrigin = await this.awbBookingsService.Awb(awbIdentifier: awbIdentifier);

                if (awbOrigin.IsSuccess)
                {
                    awbId = awbOrigin.Value.Id;
                }
                else if (!awbOrigin.IsValid)
                {
                    var reservResult = await this.awbBookingsService.ReserveAsync(context.Message.AgentId, awbIdentifier);
                    this.ThrowIfResultNotSuccess(reservResult);
                    var resultSave = await this.awbBookingsService.SaveAwbAsync(reservResult.Value);
                    this.ThrowIfResultNotSuccess(resultSave);

                    awbId = resultSave.Value;
                }
                else
                {
                    this.ThrowIfResultNotSuccess(awbOrigin);
                }

                Result<Awb> trackedResult = awbBookingsService.TrackedAwb(awbId);
                this.ThrowIfResultNotSuccess(trackedResult);

                Awb awb = this.mapper.Map<Awb>(context.Message.Ffr);
                this.mapper.Map(awb, trackedResult.Value);

                var result = await this.awbBookingsService.SaveAwb(trackedResult.Value, "Booked", false);
                this.ThrowIfResultNotSuccess(result);
            }
        }

        private void ThrowIfResultNotSuccess<T>(Result<T> result)
        {
            if (!result.IsSuccess)
            {
                result.LogIfFailedOrInvalid();
                throw new InvalidOperationException(result.Reasons.FirstOrDefault()?.Message);
            }
        }

        private string Translit(string str)
        {
            string[] lat_up = { "A", "B", "V", "G", "D", "E", "Yo", "Zh", "Z", "I", "Y", "K", "L", "M", "N", "O", "P", "R", "S", "T", "U", "F", "Kh", "Ts", "Ch", "Sh", "Shch", "\"", "Y", "'", "E", "Yu", "Ya" };
            string[] lat_low = { "a", "b", "v", "g", "d", "e", "yo", "zh", "z", "i", "y", "k", "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "kh", "ts", "ch", "sh", "shch", "\"", "y", "'", "e", "yu", "ya" };
            string[] rus_up = { "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я" };
            string[] rus_low = { "а", "б", "в", "г", "д", "е", "ё", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ы", "ь", "э", "ю", "я" };
            for (int i = 0; i <= 32; i++)
            {
                str = str.Replace(rus_up[i], lat_up[i]);
                str = str.Replace(rus_low[i], lat_low[i]);
            }
            return str;
        }

    }


}
