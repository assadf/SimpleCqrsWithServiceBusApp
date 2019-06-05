using System;
using System.Threading.Tasks;
using Framework.Shared.Messaging;
using PolicyAdminServiceApp.Domain.Events;

namespace PolicyAdminServiceApp.Domain.EventHandlers
{
    public class CustomerCreatedEventHandler : IEventHandler<CustomerCreatedEvent>
    {
        public Task HandleAsync(CustomerCreatedEvent @event)
        {
            Console.WriteLine("Inserting new Customer into Phoenix database");

            return Task.FromResult(0);
        }
    }
}
