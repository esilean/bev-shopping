using MediatR;

namespace BevShopping.Shared.Core.Events
{
    public interface IEventHandler<T> : INotificationHandler<T> where T : IEvent
    { }
}