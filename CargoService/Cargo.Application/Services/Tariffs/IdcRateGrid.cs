using Cargo.Contract.DTOs.Tariffs;
using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Data.Model.Tariffs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cargo.Application.Services.Tariffs
{
    public class IdcRateGrid : ICloneable
    {
        private ulong m_ulId = 0;
        private int m_contragentId = 0;
        private string m_sCode = "";
        private List<uint> mRanks = new List<uint>(10);

        public static void Sort(List<uint> lst)
        {
            if (lst == null) return;

            lst.Sort(delegate (uint x, uint y)
            {
                if (x == y) return 0;
                if (x > y) return 1;
                return -1;
            });
        }

        public static void Sort(List<RateGridRank> lst)
        {
            if (lst == null) return;

            lst.Sort(delegate (RateGridRank x, RateGridRank y)
            {
                uint rx = x?.Rank ?? 0;
                uint ry = y?.Rank ?? 0;

                if (rx == ry) return 0;
                if (rx > ry) return 1;
                return -1;
            });
        }

        public IdcRateGrid(ulong ulId, int iContragentId, string sCode)
        {
            id = ulId;
            contragentId = iContragentId;
            code = sCode;
        }

        public IdcRateGrid(RateGridDto dto)
        {
            if (dto == null)
                throw new ArgumentException("Internal error. Null reference argument.", nameof(dto));

            id = dto.id;
            contragentId = dto.ContragentId;
            code = dto.code;

            ranks = new List<uint>(dto.ranks);
        }

        public IdcRateGrid(RateGridHeader hd)
        {
            if (hd == null)
                throw new ArgumentException("Internal error. Null reference argument.", nameof(hd));

            id = hd.Id;
            contragentId = hd.ContragentId;
            code = hd.Code;

            foreach (RateGridRank rnk in hd.Ranks) ranks.Add(rnk.Rank);
        }

        public ulong id
        {
            get => m_ulId;
            private set => m_ulId = value;
        }

        public string code
        {
            get => m_sCode;
            private set => m_sCode = (value ?? "").Trim();
        }

        public int contragentId
        {
            get => m_contragentId;
            private set => m_contragentId = value;
        }

        public List<uint> ranks
        {
            get => mRanks;
            private set => mRanks = value ?? new List<uint>(10);
        }

        public void sort() => Sort(ranks);

        public bool compare(List<RateGridRank> lst)
        {
            if (lst == null) return false;

            uint[] a1 = ranks.ToArray();
            RateGridRank[] a2 = lst.ToArray();
            int n = a1.Length;
            if (n != a2.Length) return false;

            for (int i = 0; i < n; i++)
            {
                RateGridRank rnk = a2[i];
                if (rnk == null) return false;
                if (a1[i] != rnk.Rank) return false;
            }

            return true;
        }

        public virtual void validate()
        {
            if (code.Length < 1)
                throw new ApplicationException("Rate grid code/name cannot be empty.");

            Dictionary<uint, uint> chk = new Dictionary<uint, uint>(ranks.Count);
            foreach (uint rnk in ranks)
            {
                if (chk.ContainsKey(rnk))
                    throw new ApplicationException("Rank duplication.");
                chk.Add(rnk, rnk);
            }
            //we can actually squash the ranks on input for user's convenience
        }

        public virtual void validateOnAdd(CargoContext ctx)
        {
            if (ctx == null)
                throw new ArgumentNullException("Internal error. Null reference argument.", nameof(ctx));

            validate();

            RateGridHeader dup = ctx.RateGrids
                .SingleOrDefault(grd => grd.ContragentId == contragentId && grd.Code == code);

            if (dup != null)
                throw new ApplicationException("Rate grid with same name already exists.");

            List<RateGridHeader> all = ctx.RateGrids
                .Where(x => x.ContragentId == contragentId)
                .Include("Ranks").ToList();

            sort();
            foreach (RateGridHeader hdr in all)
            {
                Sort(hdr.Ranks);
                if (compare(hdr.Ranks))
                    throw new ApplicationException("Rate grid with the same ranks already exists.");
            }
        }

        public virtual void validateOnUpdate(CargoContext ctx)
        {
            if (ctx == null)
                throw new ArgumentNullException("Internal error. Null reference argument.", nameof(ctx));

            validate();

            RateGridHeader dup = ctx.RateGrids
                .SingleOrDefault(grd => id != grd.Id && code == grd.Code && contragentId == grd.ContragentId);

            if (dup != null)
                throw new ApplicationException("Rate grid with same name already exists.");

            List<RateGridHeader> all = ctx.RateGrids
                .Where(grd => grd.ContragentId == contragentId && id != grd.Id)
                .Include("Ranks").ToList();

            sort();

            foreach (RateGridHeader hdr in all)
            {
                Sort(hdr.Ranks);
                if (compare(hdr.Ranks))
                    throw new ApplicationException("Rate grid with the same ranks already exists.");
            }
        }

        public virtual void update(RateGridHeader hd)
        {
            if (hd == null) return;
            if (id != hd.Id) return;

            hd.Code = code;

            List<RateGridRank> lst = new List<RateGridRank>(ranks.Count);
            foreach (uint rnk in ranks) lst.Add(new RateGridRank { GridId = id, Rank = rnk });
            hd.Ranks = lst;
        }

        public virtual RateGridHeader cloneEntity()
        {
            RateGridHeader ret = new RateGridHeader { Id = id, ContragentId = contragentId, Code = (string)code.Clone() };

            List<RateGridRank> lst = ret.Ranks = new List<RateGridRank>(ranks.Count);
            foreach (uint rnk in ranks) lst.Add(new RateGridRank { GridId = id, Rank = rnk });

            Sort(lst); //courtesy to db
            return ret;
        }

        public virtual RateGridHeader cloneEntityForAdd()
        {
            RateGridHeader ret = new RateGridHeader { ContragentId = contragentId, Code = (string)code.Clone() };

            List<RateGridRank> lst = ret.Ranks = new List<RateGridRank>(ranks.Count);
            foreach (uint rnk in ranks) lst.Add(new RateGridRank { Rank = rnk });

            Sort(lst); //courtesy to db
            return ret;
        }

        public virtual RateGridDto cloneDto()
        {
            RateGridDto ret = new RateGridDto(id, contragentId, (string)code.Clone());
            ret.ranks = new List<uint>(ranks);

            Sort(ret.ranks); //coutesy to receiver
            return ret;
        }

        public IdcRateGrid cloneIdcRateGrid()
        {
            IdcRateGrid ret = new IdcRateGrid(id, contragentId, (string)code.Clone());
            ranks = new List<uint>(ranks);
            return ret;
        }

        public virtual object Clone() => cloneIdcRateGrid();

        public override string ToString() => $"{id}:{contragentId}:{code}";
    }
}