using System;
using System.Threading.Tasks;
using BevShopping.Shared.Core.Events;

namespace BevShopping.Shared.Brokers
{
    public interface IEventListener
    {
        void Subscribe(Type type);
        void Subscribe<TEvent>() where TEvent : IEvent;
        Task Publish<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}