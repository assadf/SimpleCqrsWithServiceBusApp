﻿using System.Threading.Tasks;
using CustomerServiceApp.Domain.Commands;
using CustomerServiceApp.Domain.Entities.BrandAggregate;
using CustomerServiceApp.Domain.Entities.CustomerAggregate;
using Framework.Shared.Messaging;

namespace CustomerServiceApp.Domain.CommandHandlers
{
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IEventBus _eventBus;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IBrandRepository brandRepository, IEventBus eventBus)
        {
            _customerRepository = customerRepository;
            _brandRepository = brandRepository;
            _eventBus = eventBus;
        }

        public async Task HandleAsync(CreateCustomerCommand command)
        {
            var customer = Customer.Create(command);
            await _customerRepository.CreateAsync(customer).ConfigureAwait(false);

            if (customer.ValidateForEligibility)
            {
                var brand = await _brandRepository.GetBrandAsync(command.BrandId).ConfigureAwait(false);
                customer.IsEligible(brand);
            }

            await customer.SendEventsAsync(_eventBus).ConfigureAwait(false);
        }
    }
}
