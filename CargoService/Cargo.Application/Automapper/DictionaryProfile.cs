using AutoMapper;
using Cargo.Contract.DTOs;
using Cargo.Infrastructure.Data.Model.Dictionary;

namespace Cargo.Application.Automapper
{
    public class DictionaryProfile : Profile
    {
        public DictionaryProfile()
        {
            CreateMap<Shr, ShrDto>().ReverseMap();

            CreateMap<Currency, CurrencyDto>().ReverseMap();

            CreateMap<IataLocation, IataLocationDto>().ReverseMap();

            CreateMap<Country, CountryDto>();
        }
    }
}