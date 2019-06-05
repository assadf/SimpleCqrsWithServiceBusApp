using System.Threading.Tasks;
using CustomerServiceApp.Domain.Entities.CustomerAggregate;

namespace CustomerServiceApp.Infrastructure.Repositories
{
    public class CustomerSqlRepository : ICustomerRepository
    {
        public async Task<int> CreateAsync(Customer customer)
        {
            customer.SetId(1);

            return 1;
        }
    }
}
