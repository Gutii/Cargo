using AutoMapper;
using Cargo.Application.Services.CommercialPayload;
using Cargo.Contract.DTOs.Settings;
using Cargo.Contract.Queries.Settings;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Application.QueryHandlers.Settings
{
    public class FindPkzQueryHandler : IQueryHandler<FindPkzQuery, PkzDto>
    {
        ChainSearchPkz ChainSearchPkz;
        IMapper mapper;

        public FindPkzQueryHandler(ChainSearchPkz chainSearchPkz, IMapper mapper)
        {
            this.ChainSearchPkz = chainSearchPkz;
            this.mapper = mapper;
        }

        public async Task<PkzDto> Handle(FindPkzQuery request, CancellationToken cancellationToken)
        {
            return await ChainSearchPkz.FindPkz(request);
        }
    }
}
