using System;
using Framework.Shared.Messaging;

namespace PolicyAdminServiceApp.Domain.Events
{
    public class CustomerCreatedEvent : IEvent
    {
        public Guid EventId { get; }

        public string CategoryName => EventCategories.NewCustomerSuccessEvent;

        public string FirstName { get; }

        public string LastName { get; }

        public DateTime DateOfBirth { get; }

        public CustomerCreatedEvent(string firstName, string lastName, DateTime dateOfBirth)
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

            EventId = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }
    }
}
