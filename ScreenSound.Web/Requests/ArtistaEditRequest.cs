namespace ScreenSound.Web.Requests
{
    public record ArtistaEditRequest (int id, string nome, string bio, string? fotoPerfil) 
        : ArtistaGetRequest(nome, bio, fotoPerfil);
}
