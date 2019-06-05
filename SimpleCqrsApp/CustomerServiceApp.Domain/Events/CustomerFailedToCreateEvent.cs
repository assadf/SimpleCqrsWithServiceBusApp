using System;
using Framework.Shared.Messaging;

namespace CustomerServiceApp.Domain.Events
{
    public class CustomerFailedToCreateEvent : IEvent
    {
        public Guid EventId { get; }

        public CustomerFailedToCreateEvent()
        {
            EventId = Guid.NewGuid();
        }
    }
}
