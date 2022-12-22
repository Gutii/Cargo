using AutoMapper;
using Cargo.Contract.Commands.Tariffs;
using Cargo.Contract.DTOs.Tariffs;
using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Data.Model.Dictionary;
using Cargo.Infrastructure.Data.Model.Tariffs;
using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.CommandHandlers.Tariffs
{
    public class CreateOrUpdateTariffGroupCommandHandler : ICommandHandler<CreateOrUpdateTariffGroupCommand, TariffGroupDto>
    {
        CargoContext dbContext;
        IMapper mapper;

        public CreateOrUpdateTariffGroupCommandHandler(CargoContext dbContext, IMapper mapper, IPublishEndpoint endpoint)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<TariffGroupDto> Handle(CreateOrUpdateTariffGroupCommand request, CancellationToken cancellationToken)
        {
            TariffGroupDto tariffGroupDto = mapper.Map<TariffGroupDto>(request);
            TariffGroup tariffGroup = mapper.Map<TariffGroup>(tariffGroupDto);

            int[] airportsId = tariffGroup.Airports.Select(s => s.Id).ToArray();
            List<IataLocation> airports = dbContext.IataLocations.Where(w => airportsId.Contains(w.Id)).ToList();
            tariffGroup.Airports = airports;


            if (request.Id != 0)
            {
                TariffGroup dbTariffGroup = dbContext.TariffGroups.Include(i => i.Airports).FirstOrDefault(w => w.Id == request.Id);
                mapper.Map(tariffGroup, dbTariffGroup);
                dbContext.SaveChanges();
                return mapper.Map<TariffGroupDto>(dbTariffGroup);
            }
            else
            {
                dbContext.TariffGroups.Add(tariffGroup);
                dbContext.SaveChanges();
                return mapper.Map<TariffGroupDto>(tariffGroup);

            }
        }
    }
}