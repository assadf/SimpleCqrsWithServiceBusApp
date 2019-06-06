using System;
using Framework.Shared.Messaging;

namespace PolicyAdminServiceApp.Domain.Events
{
    public class CustomerVerifiedAsEligibleEvent : IEvent
    {
        public Guid EventId { get; set; }

        public string CategoryName { get; set; }

        public int Id { get; set; }

        public DateTime ExpirationDate { get; set; }

    }
}
