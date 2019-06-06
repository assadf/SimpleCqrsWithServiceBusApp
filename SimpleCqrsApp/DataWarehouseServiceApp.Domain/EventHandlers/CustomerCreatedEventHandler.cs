using System;
using System.Threading.Tasks;
using DataWarehouseServiceApp.Domain.Events;
using Framework.Shared.Messaging;
using Newtonsoft.Json;

namespace DataWarehouseServiceApp.Domain.EventHandlers
{
    public class CustomerCreatedEventHandler : IEventHandler<CustomerCreatedEvent>
    {
        public Task HandleAsync(CustomerCreatedEvent @event)
        {
            var json = JsonConvert.SerializeObject(@event);
            Console.WriteLine($"Inserting new Customer into Data Warehouse database: {json}");

            return Task.FromResult(0);
        }
    }
}
