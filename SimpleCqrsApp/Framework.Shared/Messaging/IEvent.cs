using System;

namespace Framework.Shared.Messaging
{
    public interface IEvent
    {
        Guid EventId { get; }
    }
}
