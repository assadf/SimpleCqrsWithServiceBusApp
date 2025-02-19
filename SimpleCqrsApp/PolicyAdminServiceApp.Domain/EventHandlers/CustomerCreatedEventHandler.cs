﻿using System;
using System.Threading.Tasks;
using Framework.Shared.Messaging;
using Newtonsoft.Json;
using PolicyAdminServiceApp.Domain.Events;

namespace PolicyAdminServiceApp.Domain.EventHandlers
{
    public class CustomerCreatedEventHandler : IEventHandler<CustomerCreatedEvent>
    {
        public Task HandleAsync(CustomerCreatedEvent @event)
        {
            var json = JsonConvert.SerializeObject(@event);
            Console.WriteLine($"Inserting new Customer into Phoenix database: {json}");

            return Task.FromResult(0);
        }
    }
}
