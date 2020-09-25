using MediatR;

namespace BevShopping.Shared.Core.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    { }
}