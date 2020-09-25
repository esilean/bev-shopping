using MediatR;
using System;
using System.Threading.Tasks;

namespace BevShopping.Shared.Core.Events
{
    public class EventBus : IEventBus
    {
        private readonly IMediator _mediator;


        public EventBus(IMediator mediator)
        {
            _mediator = mediator ?? throw new Exception($"Missing dependency '{nameof(IMediator)}'");
        }

        public virtual async Task PublishLocal(params IEvent[] events)
        {
            foreach (var @event in events)
            {
                await _mediator.Publish(@event);
            }
        }
    }
}