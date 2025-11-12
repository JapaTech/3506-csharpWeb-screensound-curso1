using ScreenSound.Modelos;

namespace ScreenSound.API.Requests
{
    public record MusicaEditRequest (int id, string nome, int anoLancamento, int artistaId):
        MusicaGetRequest (nome, anoLancamento, artistaId);
}
