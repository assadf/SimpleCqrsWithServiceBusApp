using System;
using System.Threading.Tasks;
using Framework.Shared.Messaging;
using Newtonsoft.Json;
using PolicyAdminServiceApp.Domain.Events;

namespace DataWarehouseServiceApp.Domain.EventHandlers
{
    public class CustomerVerifiedAsEligibleEventHandler : IEventHandler<CustomerVerifiedAsEligibleEvent>
    {
        public Task HandleAsync(CustomerVerifiedAsEligibleEvent @event)
        {
            var json = JsonConvert.SerializeObject(@event);
            Console.WriteLine($"Verify Customer record exists, if not delay message otherwise update Expiration Date in Data Warehouse database: {json}");

            return Task.FromResult(0);
        }
    }
}
