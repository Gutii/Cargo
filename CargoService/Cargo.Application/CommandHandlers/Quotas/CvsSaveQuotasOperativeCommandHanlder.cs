using AutoMapper;
using Cargo.Contract.Commands.Quotas;
using Cargo.Contract.DTOs.Quotas;
using Cargo.Infrastructure.Data.Model.Quotas;
using Cargo.Infrastructure.Data;
using IdealResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Org.BouncyCastle.Tsp;
using PdfSharpCore.Pdf.Content;
using System.Text.Json;
using System.IO;
using Cargo.Infrastructure.Data.Model;
using Cargo.Application.Parsers;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using Cargo.Application.CsvMapper;
using System.Collections;

namespace Cargo.Application.CommandHandlers.Quotas
{
    public class CvsSaveQuotasOperativeCommandHanlder : ICommandHandler<CvsSaveQuotasOperativeCommand, Result>
    {
        IMapper mapper;
        CargoContext dbContext;

        public CvsSaveQuotasOperativeCommandHanlder(IMapper mapper, CargoContext dbContext)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public async Task<Result> Handle(CvsSaveQuotasOperativeCommand request, CancellationToken cancellationToken)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    //byte[] bytes = File.ReadAllBytes(@"C:\Users\vanta\OneDrive\Документы\BookingService_QuotasOperative.csv");
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HasHeaderRecord = false,
                    };
                    var reader = new StreamReader(new MemoryStream(request.Bytes));
                    var csv = new CsvReader(reader, config);
                    var records = csv.GetRecords<QuotasOperativeCsvMapper>().ToList();
                    var quotasOperative = mapper.Map<List<QuotasOperative>>(records);
                    if (quotasOperative[quotasOperative.Count-1].Currency.Count() > 2)
                    {
                        quotasOperative[quotasOperative.Count - 1].Currency = quotasOperative[quotasOperative.Count - 1].Currency.Substring(0, 2);
                    }
                    foreach(var quotaOperative in quotasOperative)
                    {
                        dbContext.QuotasOperative.Add(quotaOperative);
                    }
                    
                    dbContext.SaveChanges();
                }
                catch(Exception ex)
                {
                    return Result.Fail(new Error($"Не удалось сохранить квоты.").CausedBy(new NotSupportedException()));
                }
                

                return Result.Ok();
            });
            return await task;
        }

        private async Task<byte[]> ReadAllBytesAsync(Stream stream)
        {
            switch (stream)
            {
                case MemoryStream mem:
                    return mem.ToArray();
                default:
                    using (var mem = new MemoryStream())
                    {
                        await stream.CopyToAsync(mem);
                        return mem.ToArray();
                    }
            }
        }
    }
}
