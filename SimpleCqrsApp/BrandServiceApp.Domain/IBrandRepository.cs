using System.Threading.Tasks;

namespace BrandServiceApp.Domain
{
    public interface IBrandRepository
    {
        Task<dynamic> GetBrandAsync(int id);
    }
}
