using System;
using Framework.Shared.Messaging;
using PolicyAdminServiceApp.Domain.Events;

namespace DataWarehouseServiceApp.Domain.Events
{
    public class CustomerCreatedEvent : IEvent
    {
        public Guid EventId { get; }

        public string CategoryName { get; set; }

        public int Id { get; set; }

        public DateTime DateOfBirth { get; set; }

    }
}
