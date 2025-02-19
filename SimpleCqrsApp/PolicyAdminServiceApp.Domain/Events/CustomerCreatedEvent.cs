﻿using System;
using Framework.Shared.Messaging;

namespace PolicyAdminServiceApp.Domain.Events
{
    public class CustomerCreatedEvent : IEvent
    {
        public Guid EventId { get; set; }

        public string CategoryName { get; set; }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
