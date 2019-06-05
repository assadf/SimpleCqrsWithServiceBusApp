using System;
using System.Net.Http;
using System.Threading.Tasks;
using CustomerServiceApp.Domain.Entities.BrandAggregate;
using Newtonsoft.Json;

namespace CustomerServiceApp.Infrastructure.Repositories
{
    public class BrandRestApiRepository : IBrandRepository
    {
        private readonly HttpClient _httpClient;

        public BrandRestApiRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5001");
        }

        public async Task<Brand> GetBrandAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/brands/{id}").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Brand>(resultString);
        }
    }
}
