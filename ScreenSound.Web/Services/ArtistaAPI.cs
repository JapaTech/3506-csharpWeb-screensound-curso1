using ScreenSound.Web.Componentes;
using ScreenSound.Web.Pages;
using ScreenSound.Web.Requests;
using ScreenSound.Web.Response;
using System.Net.Http.Json;

namespace ScreenSound.Web.Services
{
    public class ArtistaAPI
    {
        private readonly HttpClient _httpClient;

        public ArtistaAPI(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API");
        }

        public async Task<ICollection<ArtistaResponse>?> GetArtistaResponsesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ICollection<ArtistaResponse>>("Artistas");
            return response;
        }

        public async Task CadastrarArtistaAsync(ArtistaGetRequest request)
        {
            await _httpClient.PostAsJsonAsync("Artistas", request);
        }

        public async Task<ArtistaResponse> GetArtistaPorNomeAsync(string nome)
        {
            return await _httpClient.GetFromJsonAsync<ArtistaResponse>($"Artistas/{nome}");
        }

        public async Task AtualizarArtistaAsync(ArtistaEditRequest request) 
        { 
            await _httpClient.PutAsJsonAsync("Artistas", request);
        }

        public async Task DeletarArtistaAsync(int id)
        {
            await _httpClient.DeleteAsync($"Artistas/{id}");
        }
    }
}
