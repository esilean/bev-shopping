using MediatR;

namespace BevShopping.Shared.Core.Commands
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    { }

    public interface ICommand : IRequest
    { }
}