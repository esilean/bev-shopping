using System;

namespace BevShopping.Shared.Core.Events
{
    public abstract class Event : IEvent
    {
        public virtual Guid Id { get; } = Guid.NewGuid();
        public virtual DateTime TimestampUtc { get; } = DateTime.UtcNow;
    }
}