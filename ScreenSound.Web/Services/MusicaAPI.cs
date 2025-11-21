using ScreenSound.Web.Requests;
using ScreenSound.Web.Response;
using System.Collections;
using System.Net.Http.Json;

namespace ScreenSound.Web.Services
{
    public class MusicaAPI
    {
        private readonly HttpClient _httpClient;
        public MusicaAPI(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API");
        }

        public async Task<ICollection<MusicaResponse>?> GetMusicasResponsesAsync()
        {
            return await _httpClient.GetFromJsonAsync<ICollection<MusicaResponse>>("Musicas");
        }

        public async Task CadastrarMusicaAsync(MusicaGetRequest musica)
        {
            await _httpClient.PostAsJsonAsync("Musicas", musica);
        }
    }
}
