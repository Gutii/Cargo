using Cargo.Contract.Commands;
using Cargo.Infrastructure.Data;
using IdealResults;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.CommandHandlers
{
    public class SaleStateCommandHandler : ICommandHandler<SaleStateCommand, Result>
    {
        CargoContext CargoContext;

        public SaleStateCommandHandler(CargoContext cargoContext)
        {
            CargoContext = cargoContext;
        }

        public async Task<Result> Handle(SaleStateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var flight = CargoContext.FlightShedules.Where(i => i.Id == request.FlightId)?.FirstOrDefault();
                if (flight != null)
                {
                    flight.SaleState = request.State;
                    await CargoContext.SaveChangesAsync(cancellationToken);
                    return await Task.FromResult(Result.Ok());
                }
                return await Task.FromResult(Result.Fail("Рейс отсутствует."));
            }
            catch (Exception e)
            {
                return await Task.FromResult(Result.Fail("Все плохо - " + e.Message));
            }


        }
    }
}