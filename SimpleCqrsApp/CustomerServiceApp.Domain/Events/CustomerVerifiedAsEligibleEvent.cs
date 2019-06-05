using System;
using Framework.Shared.Messaging;

namespace CustomerServiceApp.Domain.Events
{
    public class CustomerVerifiedAsEligibleEvent : IEvent
    {
        public Guid EventId { get; }

        public int CustomerId { get;  }

        public DateTime ExpirationDate { get; }

        public CustomerVerifiedAsEligibleEvent(int customerId, DateTime expirationDate)
        {
            EventId = Guid.NewGuid();
            CustomerId = customerId;
            ExpirationDate = expirationDate;
        }
    }
}
