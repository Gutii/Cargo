using AutoMapper;
using Cargo.Application.CsvMapper;
using Cargo.Contract.DTOs;
using Cargo.Infrastructure.Data.Model.Dictionary;
using Cargo.Infrastructure.Data.Model.Quotas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Application.Automapper
{
    public class CsvProfile : Profile
    {
        public CsvProfile()
        {
            CreateMap<QuotasOperativeCsvMapper, QuotasOperative>().ReverseMap();
        }
    }
}
