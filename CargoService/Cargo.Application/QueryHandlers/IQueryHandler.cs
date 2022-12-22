using Cargo.Contract.Queries;
using MediatR;

namespace Cargo.Application.QueryHandlers
{
    public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IQuery<TResponse>
    {

    }

}
