using System.Threading;
using System.Threading.Tasks;

namespace BevShopping.Shared.Core.Commands
{
    public interface ICommandBus
    {
        Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default(CancellationToken));

        Task Send(ICommand command, CancellationToken cancellationToken = default(CancellationToken));
    }
}