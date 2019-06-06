using System;
using Framework.Shared.Messaging;

namespace CustomerServiceApp.Domain.Events
{
    public class CustomerCreatedEvent : IEvent
    {
        public Guid EventId { get; }

        public string CategoryName => EventCategories.NewCustomerSuccessEvent;

        public int Id { get; set; }

        public string FirstName { get; }

        public string LastName { get; }

        public DateTime DateOfBirth { get; }

        public CustomerCreatedEvent(int id, string firstName, string lastName, DateTime dateOfBirth)
        {
            if (id <= 0)
            {
                throw new ArgumentException($"{nameof(id)} must be greater than zero", nameof(id));
            }

            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException($"{nameof(firstName)} is required", nameof(firstName));
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException($"{nameof(lastName)} is required", nameof(lastName));
            }

            if (dateOfBirth == DateTime.MinValue)
            {
                throw new ArgumentException($"{nameof(dateOfBirth)} is required", nameof(dateOfBirth));
            }

            EventId = Guid.NewGuid();
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }
    }
}
