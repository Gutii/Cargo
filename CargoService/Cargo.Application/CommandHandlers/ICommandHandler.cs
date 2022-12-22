using Cargo.Contract.Commands;
using MediatR;

namespace Cargo.Application.CommandHandlers
{
    public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : ICommand<TResponse>
    {

    }
}
