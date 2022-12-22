using AutoMapper;
using Cargo.Infrastructure.Data.Model.Settings.MyFlights;

namespace Cargo.Application.Automapper;

public class MyFlightProfile : Profile
{
    public MyFlightProfile()
    {
        this.CreateMap<MyFlight, MyFlight>();
        this.CreateMap<MyFlightRoute, MyFlightRoute>();
        this.CreateMap<MyFlightNumbers, MyFlightNumbers>();
    }
}