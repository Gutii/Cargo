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
using Microsoft.EntityFrameworkCore;

namespace Cargo.Application.CommandHandlers.Quotas
{
    public class SaveQuotasDirectoryCommandHandler : ICommandHandler<SaveQuotasDirectoryCommand, Result<QuotasDirectoryDto>>
    {
        IMapper mapper;
        CargoContext dbContext;

        public SaveQuotasDirectoryCommandHandler(IMapper mapper, CargoContext dbContext)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public async Task<Result<QuotasDirectoryDto>> Handle(SaveQuotasDirectoryCommand request, CancellationToken cancellationToken)
        {
            var task = Task.Run(() =>
            {
                var quotas = request.QuotasDirectory;
                var quotasCorrect = dbContext.QuotasDirectory
                .AsNoTracking()
                .Where(c => c.Id == quotas.Id)
                .FirstOrDefault();
                if (quotasCorrect == null)
                {
                    dbContext.QuotasDirectory.Add(mapper.Map<QuotasDirectory>(quotas));
                }
                else
                {
                    dbContext.QuotasDirectory.Update(mapper.Map<QuotasDirectory>(quotas));
                }

                dbContext.SaveChanges();

                return Result.Ok(quotas);
            });
            return await task;
        }
    }
}
