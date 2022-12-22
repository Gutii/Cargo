using AutoMapper;
using IDeal.Common.Components.Paginator;
using IdealResults;
using System.Threading.Tasks;

namespace Cargo.Application.Automapper
{
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            this.CreateMap(typeof(PagedResult<>), typeof(PagedResult<>));
            this.CreateMap(typeof(SingleResult<>), typeof(SingleResult<>));
            this.CreateMap(typeof(Result<>), typeof(Result<>));
            this.CreateMap(typeof(Task<>), typeof(Task<>));
        }
    }
}
