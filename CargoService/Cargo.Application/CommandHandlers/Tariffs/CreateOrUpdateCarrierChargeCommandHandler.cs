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
    public class CreateOrUpdateCarrierChargeCommandHandler : ICommandHandler<CreateOrUpdateCarrierChargeCommand, CarrierChargeDto>
    {
        CargoContext dbContext;
        IMapper mapper;

        public CreateOrUpdateCarrierChargeCommandHandler(CargoContext dbContext,
            IMapper mapper,
            IPublishEndpoint endpoint)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<CarrierChargeDto> Handle(CreateOrUpdateCarrierChargeCommand request, CancellationToken cancellationToken)
        {
            var carrierCharge = mapper.Map<CarrierCharge>(request.CarrierCharge);

            var dbCarrierCharge = dbContext.CarrierCharges
                .Include(x => x.IncludedShrCodes)
                .Include(x => x.ExcludedShrCodes)
                .Include(x => x.IncludedProducts)
                .Include(x => x.ExcludedProducts)
                .Include(x => x.CarrierChargeBindings)
                .ThenInclude(x => x.Currency)
                .Include(x => x.CarrierChargeBindings)
                .ThenInclude(x => x.Country)
                .Include(x => x.CarrierChargeBindings)
                .ThenInclude(x => x.Airports)
                .FirstOrDefault(x => x.Id == carrierCharge.Id);

            UpdateCollections(carrierCharge);

            if (dbCarrierCharge != null)
            {
                mapper.Map(carrierCharge, dbCarrierCharge);

                UpdateBindings(dbCarrierCharge, carrierCharge);

                dbContext.SaveChanges();
                return mapper.Map<CarrierChargeDto>(dbCarrierCharge);
            }
            else
            {
                dbContext.CarrierCharges.Add(carrierCharge);

                dbContext.SaveChanges();
                return mapper.Map<CarrierChargeDto>(carrierCharge);
            }
        }

        private void UpdateCollections(CarrierCharge carrierCharge)
        {
            var shrCodes = new List<Shr>();
            foreach (var shrCode in carrierCharge.IncludedShrCodes)
            {
                var shrInCtx = dbContext.Shrs.Local.FirstOrDefault(x => x.Id == shrCode.Id);
                if (shrInCtx == null)
                {
                    dbContext.Entry(shrCode).State = EntityState.Unchanged;
                    shrInCtx = shrCode;
                }
                shrCodes.Add(shrInCtx);
            }
            carrierCharge.IncludedShrCodes = shrCodes;

            shrCodes = new List<Shr>();
            foreach (var shrCode in carrierCharge.ExcludedShrCodes)
            {
                var shrInCtx = dbContext.Shrs.Local.FirstOrDefault(x => x.Id == shrCode.Id);
                if (shrInCtx == null)
                {
                    dbContext.Entry(shrCode).State = EntityState.Unchanged;
                    shrInCtx = shrCode;
                }
                shrCodes.Add(shrInCtx);
            }
            carrierCharge.ExcludedShrCodes = shrCodes;

            var products = new List<TransportProduct>();
            foreach (var product in carrierCharge.IncludedProducts)
            {
                var productInCtx = dbContext.TransportProducts.Local.FirstOrDefault(x => x.Id == product.Id);
                if (productInCtx == null)
                {
                    dbContext.Entry(product).State = EntityState.Unchanged;
                    productInCtx = product;
                }
                products.Add(productInCtx);
            }
            carrierCharge.IncludedProducts = products;

            products = new List<TransportProduct>();
            foreach (var product in carrierCharge.ExcludedProducts)
            {
                var productInCtx = dbContext.TransportProducts.Local.FirstOrDefault(x => x.Id == product.Id);
                if (productInCtx == null)
                {
                    dbContext.Entry(product).State = EntityState.Unchanged;
                    productInCtx = product;
                }
                products.Add(productInCtx);
            }
            carrierCharge.ExcludedProducts = products;

            if (carrierCharge.CarrierChargeBindings != null && carrierCharge.CarrierChargeBindings.Count > 0)
            {
                foreach (var binding in carrierCharge.CarrierChargeBindings)
                {
                    var airports = new List<IataLocation>();
                    foreach (var airport in binding.Airports)
                    {
                        var airportInCtx = dbContext.IataLocations.Local.FirstOrDefault(x => x.Id == airport.Id);
                        if (airportInCtx == null)
                        {
                            dbContext.Entry(airport).State = EntityState.Unchanged;
                            airportInCtx = airport;
                        }
                        airports.Add(airportInCtx);
                    }
                    binding.Airports = airports;

                    if (binding.Country != null)
                    {
                        var countryInCtx = dbContext.Countries.Local.FirstOrDefault(x => x.Id == binding.Country.Id);
                        if (countryInCtx == null)
                        {
                            dbContext.Entry(binding.Country).State = EntityState.Unchanged;
                            countryInCtx = binding.Country;
                        }
                        binding.Country = countryInCtx;
                    }

                    if (binding.Currency != null)
                    {
                        var currencyInCtx = dbContext.Currencies.Local.FirstOrDefault(x => x.Id == binding.Currency.Id);
                        if (currencyInCtx == null)
                        {
                            dbContext.Entry(binding.Currency).State = EntityState.Unchanged;
                            currencyInCtx = binding.Currency;
                        }
                        binding.Currency = currencyInCtx;
                    }
                }
            }
        }

        private void UpdateBindings(CarrierCharge dbCarrierCharge, CarrierCharge carrierCharge)
        {
            if (carrierCharge.CarrierChargeBindings == null || carrierCharge.CarrierChargeBindings.Count == 0)
            {
                dbCarrierCharge.CarrierChargeBindings.Clear();
                return;
            }

            var bindingsToStore = new List<CarrierChargeBinding>();
            foreach (var binding in carrierCharge.CarrierChargeBindings)
            {
                var bindingInDb = dbContext.CarrierChargeBindings
                    .FirstOrDefault(x => x.Id == binding.Id);

                if (bindingInDb == null)
                {
                    bindingInDb = new CarrierChargeBinding();
                }

                bindingInDb.Currency = binding.Currency;
                bindingInDb.Parameter = binding.Parameter;
                bindingInDb.Value = binding.Value;
                bindingInDb.Country = binding.Country;
                bindingInDb.Airports = binding.Airports;

                bindingsToStore.Add(bindingInDb);
            }
            dbCarrierCharge.CarrierChargeBindings = bindingsToStore;
        }
    }
}