using System;
using Framework.Shared.Core;

namespace CustomerServiceApp.Domain.Entities.BrandAggregate
{
    public class Brand : DomainEntity, IAggregateRoot
    {
        public int Id { get; }

        public int MinAge { get; }

        public int MaxAge { get; }

        public Brand(int id, int minAge, int maxAge)
        {
            if (id == default(int))
            {
                throw new ArgumentException("Brand Id is required", nameof(id));
            }

            if (minAge == default(int))
            {
                throw new ArgumentException("Min Age is required", nameof(minAge));
            }

            if (maxAge == default(int))
            {
                throw new ArgumentException("Max Age is required", nameof(maxAge));
            }

            Id = id;
            MinAge = minAge;
            MaxAge = maxAge;
        }
    }
}
