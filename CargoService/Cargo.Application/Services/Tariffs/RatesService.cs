using Cargo.Contract.Queries.Tariffs;
using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Data.Model.Tariffs;
using IDeal.Common.Components.Paginator;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Cargo.Application.Services.Tariffs
{
    public class RatesService
    {
        private CargoContext mContext = null;

        public RatesService(CargoContext ctx)
        {
            context = ctx;
        }

        private CargoContext context
        {
            get => mContext;
            set => mContext = value ?? throw new NullReferenceException("Internal error. Null reference assignment.");
        }

        public IdcRateGrid addRate(IdcRateGrid grd)
        {
            if (grd == null) return null;

            CargoContext ctx = context;
            grd.validateOnAdd(ctx);

            RateGridHeader hd = grd.cloneEntityForAdd();
            ctx.Add(hd);
            ctx.SaveChanges();

            return new IdcRateGrid(hd);
        }

        public IdcRateGrid updateRate(IdcRateGrid grd)
        {
            if (grd == null) return null;

            CargoContext ctx = context;
            grd.validateOnUpdate(ctx);

            RateGridHeader hd = ctx.RateGrids.Include("Ranks").SingleOrDefault(g => g.Id == grd.id);
            if (hd == null)
                throw new ApplicationException("The specified rate grid not found.");

            grd.update(hd);
            ctx.SaveChanges();

            return new IdcRateGrid(hd);
        }

        public bool deleteRate(ulong iGridId)
        {
            CargoContext ctx = context;

            RateGridHeader hd = ctx.RateGrids.SingleOrDefault(grd => grd.Id == iGridId);
            if (hd == null) return false;

            ctx.RateGrids.Remove(hd);
            ctx.SaveChanges();

            return true;
        }

        public bool deleteAllRates()
        {
            CargoContext ctx = context;

            List<RateGridHeader> all = ctx.RateGrids.ToList();
            ctx.RateGrids.RemoveRange(all);
            ctx.SaveChanges();

            return true;
        }

        public IdcRateGrid[] getRates(RateGridQuery query)
        {
            CargoContext ctx = context;
            List<RateGridHeader> lst = ctx.RateGrids
                .Where(x => x.ContragentId == query.ContragentId)
                .Include("Ranks").ToList();

            IdcRateGrid[] ret = new IdcRateGrid[lst.Count];
            int i = 0;
            foreach (RateGridHeader hd in lst) ret[i++] = new IdcRateGrid(hd);

            return ret;
        }

        public PagedResult<IdcRateGrid> getRatesPage(RateGridPagedQuery query)
        {
            CargoContext ctx = context;

            PagedResult<RateGridHeader> pageDbEnrities = ctx.RateGrids
                .Where(x => x.ContragentId == query.ContragentId)
                .Include("Ranks").Page(new PageInfo { PageIndex = query.PageIndex, PageSize = query.PageSize });

            PagedResult<IdcRateGrid> ret = new PagedResult<IdcRateGrid>();
            ret.Paging = pageDbEnrities.Paging;
            ret.Count = pageDbEnrities.Count;

            int n = pageDbEnrities.Items?.Length ?? 0;
            IdcRateGrid[] a = ret.Items = new IdcRateGrid[n];
            if (pageDbEnrities.Items != null)
            {
                int i = 0;
                foreach (RateGridHeader hd in pageDbEnrities.Items)
                    a[i++] = new IdcRateGrid(hd);
            }

            return ret;
        }

        public IdcRateGrid getRate(ulong iGridId)
        {
            CargoContext ctx = context;

            RateGridHeader hd = ctx.RateGrids.Include("Ranks").SingleOrDefault(grd => grd.Id == iGridId);
            if (hd == null)
                throw new ApplicationException("The specified rate grid not found.");

            return new IdcRateGrid(hd);
        }

        public List<uint> deserializeRanks(string sText)
        {
            string s = (sText ?? "").Replace(" ", "");
            MatchCollection mc = Regex.Matches(sText, @"[\-\+]\d+");

            List<(char, uint)> buf = new List<(char, uint)>(mc.Count);
            (char dir, uint val) eFirst = ('$', uint.MaxValue);
            (char dir, uint val) ePrev = ('$', uint.MaxValue);

            foreach (Match match in mc)
            {
                string sRank = match.Value;
                if (sRank.Length < 2) continue; //invalid, protective

                char dir = sRank[0];
                string sVal = sRank.Substring(1);
                uint val = 0;
                if (!uint.TryParse(sVal, out val)) continue; //invalid

                if (buf.Count < 1)
                {
                    if (val != 0)
                        buf.Add(eFirst = ePrev = (dir, val));
                    else
                        buf.Add(eFirst = ePrev = ('+', val));
                    continue; //done for this iteration
                }

                //true for all after 1st
                if (val < ePrev.val) continue; //invalid
                if (dir != '+') continue; //invalid

                if (buf.Count < 2)
                {
                    if (val == ePrev.val)
                    {
                        if (val == 0) continue; //invalid

                        if (ePrev.dir == '-') buf.Add(ePrev = ('+', val)); //otherwise duplication
                        continue; //done for this iteration
                    }

                    if (ePrev.dir == '-') continue; //invalid, gap

                    buf.Add(ePrev = ('+', val));
                    continue; //done for this iteration
                }

                if (val == ePrev.val) continue; //invalid, duplication
                buf.Add(ePrev = ('+', val));
            }

            int j = 0;
            List<uint> ret = new List<uint>(buf.Count);
            foreach ((char dir, uint val) rnk in buf)
            {
                if (j++ == 1 && rnk.val == eFirst.val) continue;
                ret.Add(rnk.val);
            }

            return ret;
        }

        public string serializeRanks(uint[] ranks)
        {
            int cnt;
            if ((cnt = ranks?.Length ?? 0) < 1) return "";

            uint[] ranks2 = new uint[cnt];
            ranks.CopyTo(ranks2, 0);
            Array.Sort(ranks2, delegate (uint x, uint y)
             {
                 if (x == y) return 0;
                 if (x > y) return 1;
                 return -1;
             });

            StringBuilder sb = new StringBuilder(cnt * 3);

            uint rnk = ranks2[0];
            if (rnk > 0)
                sb.Append($"-{rnk}+{rnk}");
            else
                sb.Append($"+{rnk}");

            for (int i = 1; i < cnt; i++) sb.Append($"+{ranks2[i]}");

            return sb.ToString();
        }
    }
}