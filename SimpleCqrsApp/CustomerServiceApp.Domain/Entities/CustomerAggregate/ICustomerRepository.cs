using System.Threading.Tasks;

namespace CustomerServiceApp.Domain.Entities.CustomerAggregate
{
    public interface ICustomerRepository
    {
        Task<int> CreateAsync(Customer customer);
    }
}
