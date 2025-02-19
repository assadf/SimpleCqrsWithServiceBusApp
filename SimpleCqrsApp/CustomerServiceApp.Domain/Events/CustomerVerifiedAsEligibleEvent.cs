﻿using System;
using Framework.Shared.Messaging;

namespace CustomerServiceApp.Domain.Events
{
    public class CustomerVerifiedAsEligibleEvent : IEvent
    {
        public Guid EventId { get; }

        public string CategoryName => EventCategories.NewCustomerSuccessEvent;

        public int Id { get;  }

        public DateTime ExpirationDate { get; }

        public CustomerVerifiedAsEligibleEvent(int id, DateTime expirationDate)
        {
            EventId = Guid.NewGuid();
            Id = id;
            ExpirationDate = expirationDate;
        }
    }
}
