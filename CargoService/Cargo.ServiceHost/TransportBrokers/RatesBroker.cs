using Cargo.Application.Services.Tariffs;
using Cargo.Contract.DTOs.Tariffs;
using Cargo.Contract.Queries.Tariffs;
using Cargo.Infrastructure.Data;
using IDeal.Common.Components.Paginator;
using System;
using System.Collections.Generic;

namespace Cargo.ServiceHost.TransportBrokers
{
    internal class RatesBroker
    {
        private CargoContext mContext = null;

        public RatesBroker(CargoContext ctx)
        {
            context = ctx;
        }

        private CargoContext context
        {
            get => mContext;
            set => mContext = value ?? throw new NullReferenceException("Internal error. Null reference assignment.");
        }

        public RateGridDto addRate(RateGridDto pck)
        {
            if (pck == null) return null;

            RatesService svc = new RatesService(context);
            return svc.addRate(new IdcRateGrid(pck)).cloneDto();
        }

        public RateGridDto updateRate(RateGridDto pck)
        {
            if (pck == null) return null;

            RatesService svc = new RatesService(context);
            return svc.updateRate(new IdcRateGrid(pck)).cloneDto();
        }

        public bool deleteRate(ulong iGridId)
        {
            RatesService svc = new RatesService(context);
            return svc.deleteRate(iGridId);
        }

        public bool deleteRate(RateGridDto pck)
        {
            if (pck == null) return false;
            return deleteRate(pck.id);
        }

        public bool deleteAllRates()
        {
            RatesService svc = new RatesService(context);
            return svc.deleteAllRates();
        }

        public RateGridDto[] getRates(RateGridQuery query)
        {
            RatesService svc = new RatesService(context);
            IdcRateGrid[] rates = svc.getRates(query);

            int n = rates.Length;
            RateGridDto[] ret = new RateGridDto[n];
            for (int i = 0; i < n; i++) ret[i] = rates[i].cloneDto();

            return ret;
        }

        public PagedResult<RateGridDto> getRatesPage(RateGridPagedQuery query)
        {
            RatesService svc = new RatesService(context);
            PagedResult<IdcRateGrid> pageAppEntities = svc.getRatesPage(query);
            PagedResult<RateGridDto> ret = new PagedResult<RateGridDto>();
            ret.Paging = pageAppEntities.Paging;
            ret.Count = pageAppEntities.Count;

            RateGridDto[] a = ret.Items = new RateGridDto[pageAppEntities.Items.Length];
            int i = 0;
            foreach (IdcRateGrid grd in pageAppEntities.Items)
                a[i++] = grd.cloneDto();

            return ret;
        }

        public RateGridDto getRate(ulong iGridId)
        {
            RatesService svc = new RatesService(context);
            return svc.getRate(iGridId).cloneDto();
        }

        public RateGridDto getRate(RateGridDto pck)
        {
            if (pck == null) return null;
            return getRate(pck.id);
        }

        public List<uint> deserializeRanks(string sText)
        {
            RatesService svc = new RatesService(context);
            return svc.deserializeRanks(sText);
        }

        public string serializeRanks(uint[] ranks)
        {
            RatesService svc = new RatesService(context);
            return svc.serializeRanks(ranks);
        }
    }
}