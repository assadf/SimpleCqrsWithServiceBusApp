using System.Threading.Tasks;
using CustomerServiceApp.Domain.Commands;
using CustomerServiceApp.Domain.Entities.BrandAggregate;
using CustomerServiceApp.Domain.Entities.CustomerAggregate;
using CustomerServiceApp.Domain.Events;
using Framework.Shared.Messaging;

namespace CustomerServiceApp.Domain.CommandHandlers
{
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IBrandRepository _brandRepository;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IBrandRepository brandRepository)
        {
            _customerRepository = customerRepository;
            _brandRepository = brandRepository;
        }

        public async Task HandleAsync(CreateCustomerCommand command)
        {
            var customer = Customer.Create(command);
            await _customerRepository.CreateAsync(customer).ConfigureAwait(false);

            if (customer.Id == 0)
            {
                customer.AddEvent(new CustomerFailedToCreateEvent());
            }
            else
            {
                var brand = await _brandRepository.GetBrandAsync(command.BrandId).ConfigureAwait(false);
                customer.IsEligible(brand);
            }
        }
    }
}
