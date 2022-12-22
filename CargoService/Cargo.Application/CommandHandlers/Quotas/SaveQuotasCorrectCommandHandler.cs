using AutoMapper;
using Cargo.Contract.Commands.Quotas;
using Cargo.Contract.DTOs.Quotas;
using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Data.Model.Quotas;
using IdealResults;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.CommandHandlers.Quotas
{
    public class SaveQuotasCorrectCommandHandler : ICommandHandler<SaveQuotasCorrectCommand, Result<QuotasCorrectDto>>
    {
        IMapper mapper;
        CargoContext dbContext;

        public SaveQuotasCorrectCommandHandler(IMapper mapper, CargoContext dbContext)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public async Task<Result<QuotasCorrectDto>> Handle(SaveQuotasCorrectCommand request, CancellationToken cancellationToken)
        {
            var task = Task.Run(() =>
            {
                var quotas = request.QuotasCorrect;
                var quotasCorrect = dbContext.QuotasCorrect.AsNoTracking()
                .Where(c => c.QuotasOperativeId == quotas.QuotasOperativeId)
                .FirstOrDefault();
                if(quotasCorrect == null)
                {
                    dbContext.QuotasCorrect.Add(mapper.Map<QuotasCorrect>(quotas));
                }
                else
                {
                    dbContext.QuotasCorrect.Update(mapper.Map<QuotasCorrect>(quotas));
                }

                dbContext.SaveChanges();

                return Result.Ok(quotas);
            });
            return await task;
        }
    }
}
