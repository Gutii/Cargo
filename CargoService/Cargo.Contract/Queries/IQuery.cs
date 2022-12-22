using MediatR;

namespace Cargo.Contract.Queries
{
    /// <summary>Запрос. Маркер интерфейс</summary>
    /// <typeparam name="TResponse"></typeparam>
    public interface IQuery<TResponse> : IRequest<TResponse>
    {
    }
}
