using System.Threading.Tasks;

namespace BevShopping.Shared.Core.Events
{
    public interface IEventBus
    {
        Task PublishLocal(params IEvent[] events);

    }
}