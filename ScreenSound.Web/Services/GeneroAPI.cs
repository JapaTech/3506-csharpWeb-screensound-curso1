using ScreenSound.Web.Response;
using System.Net.Http.Json;

namespace ScreenSound.Web.Services
{
    public class GeneroAPI
    {
        private readonly HttpClient _httpClient;
        public GeneroAPI(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API");
        }

        public async Task<ICollection<GeneroResponse>?> GetGeneroResponsesAsync()
        {
            return await _httpClient.GetFromJsonAsync<ICollection<GeneroResponse>>("Generos");
        }

        public async Task<GeneroResponse>? GeneroResponsePorNomeAsync(string nome)
        {
            return await _httpClient.GetFromJsonAsync<GeneroResponse>($"Generos/{nome}");
        }

    }
}
