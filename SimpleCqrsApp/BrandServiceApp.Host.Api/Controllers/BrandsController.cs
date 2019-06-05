using System.Threading.Tasks;
using BrandServiceApp.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BrandServiceApp.Host.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;

        public BrandsController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        [HttpGet("{id}")]
        public async Task<dynamic> GetAsync(int id)
        {
            var brand = await _brandRepository.GetBrandAsync(id).ConfigureAwait(false);

            return Ok(brand);
        }
        
    }
}
