using System.ComponentModel.DataAnnotations;

namespace ScreenSound.Web.Requests
{
    public record MusicaGetRequest ([Required]string nome, int anoLancamento, int artistaId,
        ICollection<GeneroGetRequest> Generos = null);
}
