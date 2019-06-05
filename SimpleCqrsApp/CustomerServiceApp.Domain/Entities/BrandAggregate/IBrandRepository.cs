using System.Threading.Tasks;

namespace CustomerServiceApp.Domain.Entities.BrandAggregate
{
    public interface IBrandRepository
    {
        Task<Brand> GetBrandAsync(int id);
    }
}
