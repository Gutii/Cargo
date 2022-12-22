using AutoMapper;
using Cargo.Contract.Commands.Tariffs;
using Cargo.Contract.DTOs.Tariffs;
using Cargo.Infrastructure.Data.Model.Tariffs;

namespace Cargo.Application.Automapper
{
    public class TariffsProfile : Profile
    {
        public TariffsProfile()
        {
            CreateMap<TariffGroup, TariffGroup>();
            CreateMap<TariffGroupDto, TariffGroup>().ReverseMap();

            CreateMap<CreateOrUpdateTariffGroupCommand, TariffGroupDto>();

            CreateMap<TariffSolution, TariffSolution>()
                .ForMember(x => x.AwbOriginTariffGroup, opt => opt.Ignore())
                .ForMember(x => x.AwbDestinationTariffGroup, opt => opt.Ignore())
                .ForMember(x => x.RateGrid, opt => opt.Ignore())
                .ForMember(x => x.Addons, opt => opt.Ignore());

            CreateMap<CarrierCharge, CarrierCharge>()
                 .ForMember(x => x.CarrierChargeBindings, opt => opt.Ignore());

            CreateMap<CarrierCharge, CarrierChargeDto>().ReverseMap();
            CreateMap<CarrierChargeBinding, CarrierChargeBindingDto>().ReverseMap();

            CreateMap<TransportProduct, TransportProductDto>().ReverseMap();

            CreateMap<IataCharge, IataChargeDto>();

            CreateMap<TariffSolution, TariffSolutionDto>().ReverseMap();
            CreateMap<RateGridRankValue, RateGridRankValueDto>().ReverseMap();
            CreateMap<RateGridHeader, RateGridDto>();
            CreateMap<RateGridDto, RateGridHeader>()
                .ForMember(x => x.Ranks, opt => opt.Ignore());
            CreateMap<RateGridRank, uint>().ConstructUsing(x => x.Rank);

            CreateMap<TariffAddon, TariffAddonDto>().ReverseMap();
        }
    }
}