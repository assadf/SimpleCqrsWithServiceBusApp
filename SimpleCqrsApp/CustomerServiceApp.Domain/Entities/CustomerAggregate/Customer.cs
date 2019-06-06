using System;
using CustomerServiceApp.Domain.Commands;
using CustomerServiceApp.Domain.Entities.BrandAggregate;
using CustomerServiceApp.Domain.Events;
using Framework.Shared.Core;

namespace CustomerServiceApp.Domain.Entities.CustomerAggregate
{
    public class Customer : DomainEntity, IAggregateRoot
    {

        #region Factories

        public static Customer Create(CreateCustomerCommand command)
        {
            return new Customer(command.FirstName, command.LastName, command.DateOfBirth);
        }

        #endregion

        public string FirstName { get; }

        public string LastName { get; }

        public DateTime DateOfBirth { get; set; }

        public bool ValidateForEligibility { get; private set; }

        private Customer(string firstName, string lastName, DateTime dateOfBirth) : this(-1, firstName, lastName, dateOfBirth) {}

        private Customer(int id, string firstName, string lastName, DateTime dateOfBirth)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException($"{nameof(firstName)} is required", firstName);
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException($"{nameof(lastName)} is required", lastName);
            }

            if (dateOfBirth == DateTime.MinValue)
            {
                throw new ArgumentException($"{nameof(dateOfBirth)} is required", dateOfBirth.ToLongDateString());
            }

            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }
        
        public bool IsEligible(Brand brand)
        {
            var today = DateTime.Today;
            var age = today.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }
            
            if (age >= brand.MinAge && age < brand.MaxAge)
            {
                var expirationDate = today.AddYears(age);

                AddEvent(new CustomerVerifiedAsEligibleEvent(Id, expirationDate));

                return true;
            }

            return false;
        }

        public override void SetId(int id)
        {
            base.SetId(id);

            if (Id > 0)
            {
                AddEvent(new CustomerCreatedEvent(Id, FirstName, LastName, DateOfBirth));
                ValidateForEligibility = true;
            }
            else
            {
                AddEvent(new CustomerFailedToCreateEvent());
                ValidateForEligibility = false;
            }
        }
    }
}
