namespace ScreenSound.Web.Requests
{
    public record GeneroEditRequest (int id, string nome, string descricao) : GeneroGetRequest(nome, descricao);
}
