using System.Threading.Tasks;
using CustomerServiceApp.Domain.Entities.BrandAggregate;

namespace CustomerServiceApp.Infrastructure.Repositories
{
    public class BrandRestApiRepository : IBrandRepository
    {
        public async Task<Brand> GetBrandAsync(int id)
        {
            return new Brand(id, 50, 80);
        }
    }
}
