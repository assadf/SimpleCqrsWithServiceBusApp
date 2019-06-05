using System;
using System.Threading.Tasks;
using Framework.Shared.Messaging;
using PolicyAdminServiceApp.Domain.Events;

namespace PolicyAdminServiceApp.Domain.EventHandlers
{
    public class CustomerVerifiedAsEligibleEventHandler : IEventHandler<CustomerVerifiedAsEligibleEvent>
    {
        public Task HandleAsync(CustomerVerifiedAsEligibleEvent @event)
        {
            Console.WriteLine("Verify Customer record exists, if not delay message otherwise update Expiration Date or something");

            return Task.FromResult(0);
        }
    }
}
