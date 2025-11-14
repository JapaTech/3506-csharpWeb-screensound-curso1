using System.ComponentModel.DataAnnotations;

namespace ScreenSound.API.Requests
{
    public record MusicaGetRequest ([Required]string nome, int anoLancamento, int artistaId,
        ICollection<GeneroGetRequest> Generos = null);
}
