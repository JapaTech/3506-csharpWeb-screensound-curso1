using ScreenSound.Modelos;

namespace ScreenSound.Web.Requests
{
    public record MusicaEditRequest (int id, string nome, int anoLancamento, int artistaId):
        MusicaGetRequest (nome, anoLancamento, artistaId);
}
