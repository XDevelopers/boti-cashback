using Boticario.EuRevendedor.Interfaces.Service;
using Boticario.EuRevendedor.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Boticario.EuRevendedor.Service
{
    public class BoticarioApiService : IBoticarioApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public BoticarioApiService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<decimal?> Cashback(string cpf)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_configuration["ExternalService:host"]}v1/cashback?cpf={cpf}");
            request.Headers.Add("token", _configuration["ExternalService:token"]);

            var client = _httpClientFactory.CreateClient("boticario");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var cashBackResponse = JsonConvert.DeserializeObject<CashbackApiViewModel>(responseString);

                return cashBackResponse.Body.Credit;
            }
            return null;
        }
    }
}
