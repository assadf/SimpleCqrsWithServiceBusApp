using System;
using System.Collections.Generic;
using Framework.Shared.Messaging;

namespace Framework.Shared.Core
{
    public abstract class DomainEntity
    {
        public int Id { get; private set; }

        private readonly IDictionary<Type, IEvent> _events = new Dictionary<Type, IEvent>();

        public IEnumerable<IEvent> Events => _events.Values;

        public void AddEvent(IEvent @event)
        {
            _events[@event.GetType()] = @event;
        }

        protected void ClearAllEvents()
        {
            _events.Clear();
        }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}
