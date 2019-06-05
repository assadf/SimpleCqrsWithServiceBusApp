using System.Threading.Tasks;
using BrandServiceApp.Domain;

namespace BrandServiceApp.Infrastructure.Repositories
{
    public class BrandSqlRepository : IBrandRepository
    {
        public async Task<dynamic> GetBrandAsync(int id)
        {
            return new 
            {
                Id = 1,
                Name = "Smart",
                MinAge = 50,
                MaxAge = 75,
                LogoUrl = "https://www.smartinsurance/log.jpg"
            };
        }
    }
}
