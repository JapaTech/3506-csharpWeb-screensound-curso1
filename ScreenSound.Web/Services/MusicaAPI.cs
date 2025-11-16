namespace ScreenSound.Web.Services
{
    public class MusicaAPI
    {
        private readonly HttpClient _httpClient;
        public MusicaAPI(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API");
        }
    }
}
