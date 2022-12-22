namespace Cargo.Contract.Commands
{
    public interface ICommand<out TResponse> : MediatR.IRequest<TResponse>
    { }
}
