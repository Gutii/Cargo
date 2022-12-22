using AutoMapper;
using Cargo.Contract.Commands.Tariffs;
using Cargo.Contract.DTOs.Tariffs;
using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Data.Model.Dictionary;
using Cargo.Infrastructure.Data.Model.Tariffs;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.CommandHandlers.Tariffs
{
    public class CreateOrUpdateTariffSolutionCommandHandler : ICommandHandler<CreateOrUpdateTariffSolutionCommand, TariffSolutionDto>
    {
        CargoContext dbContext;
        IMapper mapper;

        public CreateOrUpdateTariffSolutionCommandHandler(CargoContext dbContext,
            IMapper mapper,
            IPublishEndpoint endpoint)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<TariffSolutionDto> Handle(CreateOrUpdateTariffSolutionCommand request, CancellationToken cancellationToken)
        {
            var tariffSolution = mapper.Map<TariffSolution>(request.TariffSolution);

            if (tariffSolution.AwbOriginAirport != null && tariffSolution.AwbOriginAirport.Id > 0)
            {
                var airport = dbContext.IataLocations
                    .FirstOrDefault(x => x.Id == tariffSolution.AwbOriginAirport.Id);
                if (airport != null)
                    tariffSolution.AwbOriginAirport = airport;
            }

            if (tariffSolution.AwbDestinationAirport != null && tariffSolution.AwbDestinationAirport.Id > 0)
            {
                var airport = dbContext.IataLocations
                    .FirstOrDefault(x => x.Id == tariffSolution.AwbDestinationAirport.Id);
                if (airport != null)
                    tariffSolution.AwbDestinationAirport = airport;
            }

            if (tariffSolution.TransitAirport != null && tariffSolution.TransitAirport.Id > 0)
            {
                var airport = dbContext.IataLocations
                    .FirstOrDefault(x => x.Id == tariffSolution.TransitAirport.Id);
                if (airport != null)
                    tariffSolution.TransitAirport = airport;
            }

            if (tariffSolution.AwbOriginTariffGroup != null && tariffSolution.AwbOriginTariffGroup.Id > 0)
            {
                var tariffGroup = dbContext.TariffGroups
                    .FirstOrDefault(x => x.Id == tariffSolution.AwbOriginTariffGroup.Id);
                if (tariffGroup != null)
                    tariffSolution.AwbOriginTariffGroup = tariffGroup;
            }

            if (tariffSolution.AwbDestinationTariffGroup != null && tariffSolution.AwbDestinationTariffGroup.Id > 0)
            {
                var tariffGroup = dbContext.TariffGroups
                    .FirstOrDefault(x => x.Id == tariffSolution.AwbDestinationTariffGroup.Id);
                if (tariffGroup != null)
                    tariffSolution.AwbDestinationTariffGroup = tariffGroup;
            }

            if (tariffSolution.RateGrid != null && tariffSolution.RateGrid.Id > 0)
            {
                var rateGrid = dbContext.RateGrids
                    .FirstOrDefault(x => x.Id == tariffSolution.RateGrid.Id);
                if (rateGrid != null)
                    tariffSolution.RateGrid = rateGrid;
            }

            if (tariffSolution.RateGridRankValues != null && tariffSolution.RateGridRankValues.Count > 0)
            {
                // ToDo
            }

            TariffSolution dbTariffSolution = dbContext.TariffSolutions
                //.Find(tariffSolution.Id);
                .Include(x => x.AwbOriginAirport)
                .Include(x => x.AwbDestinationAirport)
                .Include(x => x.AwbOriginTariffGroup)
                .Include(x => x.AwbDestinationTariffGroup)
                .Include(x => x.TransitAirport)
                .Include(x => x.RateGrid)
                .Include(x => x.RateGridRankValues)
                .Include(x => x.Addons)
                .ThenInclude(x => x.ShrCodes)
                .FirstOrDefault(x => x.Id == tariffSolution.Id);

            if (dbTariffSolution != null)
            {
                mapper.Map(tariffSolution, dbTariffSolution);

                dbTariffSolution.AwbOriginTariffGroup = tariffSolution.AwbOriginTariffGroup;
                dbTariffSolution.AwbDestinationTariffGroup = tariffSolution.AwbDestinationTariffGroup;
                dbTariffSolution.RateGrid = tariffSolution.RateGrid;

                UpdateAddons(dbTariffSolution, tariffSolution);

                dbContext.SaveChanges();
                return mapper.Map<TariffSolutionDto>(dbTariffSolution);
            }
            else
            {
                dbContext.TariffSolutions.Add(tariffSolution);
                dbContext.SaveChanges();
                return mapper.Map<TariffSolutionDto>(tariffSolution);
            }
        }

        private void UpdateAddons(TariffSolution dbTariffSolution, TariffSolution tariffSolution)
        {
            if (tariffSolution.Addons == null || tariffSolution.Addons.Count == 0)
            {
                dbTariffSolution.Addons.Clear();
                return;
            }

            var addonsToStore = new List<TariffAddon>();
            foreach (var addon in tariffSolution.Addons)
            {
                var addonInDb = dbContext.TariffAddons
                    .FirstOrDefault(x => x.Id == addon.Id);

                if (addonInDb == null)
                {
                    addonInDb = new TariffAddon();
                }

                addonInDb.MinimumAddon = addon.MinimumAddon;
                addonInDb.WeightAddon = addon.WeightAddon;

                var shrCodes = new List<Shr>();

                foreach (var shrCode in addon.ShrCodes)
                {
                    var shrInCtx = dbContext.Shrs.Local.FirstOrDefault(x => x.Id == shrCode.Id);
                    if (shrInCtx == null)
                    {
                        dbContext.Entry(shrCode).State = EntityState.Unchanged;
                        shrInCtx = shrCode;
                    }
                    shrCodes.Add(shrInCtx);
                }

                addonInDb.ShrCodes = shrCodes;

                addonsToStore.Add(addonInDb);
            }
            dbTariffSolution.Addons = addonsToStore;
        }
    }
}