using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.BD;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class MusicaExtensions
    {
        public static void AddEndPointsMusica(this WebApplication app)
        {
            app.MapGet("/Musicas", ([FromServices] DAL<Musica> dal) =>
            {
                return Results.Ok(dal.Listar());
            });

            app.MapGet("/Musicas/{nome}", ([FromServices] DAL<Musica> dal, string nome) =>
            {
                var musica = dal.BuscarObjetoExato(m => m.Nome.ToUpper().Equals(nome.ToUpper()));

                if (musica == null)
                    return Results.NotFound();

                return Results.Ok(musica);
            });

            app.MapPost("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] MusicaGetRequest musicaReq) =>
            {
                var musica = new Musica(musicaReq.nome, musicaReq.anoLancamento);
                dal.Adicionar(musica);
                return Results.Created($"/Musicas/{musica.Id}", musica);
            });

            app.MapDelete("/Musicas/{id}", ([FromServices] DAL<Musica> dal, int id) => {
                var musica = dal.BuscarObjetoExato(m => m.Id == id);
                if (musica == null)
                    return Results.NotFound();
                dal.Deletar(musica);
                return Results.NoContent();
            });

            app.MapPut("/Musicas/", ([FromServices] DAL<Musica> dal, MusicaEditRequest musica) =>
            {
                var musicaParaAtualizar = dal.BuscarObjetoExato(m => m.Id == musica.id);
                if (musicaParaAtualizar == null)
                    return Results.NotFound();

                musicaParaAtualizar.Nome = musica.nome;
                musicaParaAtualizar.AnoLancamento = musica.anoLancamento;                
                dal.Atualizar(musicaParaAtualizar);
                return Results.Ok();
            });
        }

        private static ICollection<MusicaResponse> EntidadeParaListaResposta(IEnumerable<Musica> lista)
        {
            return lista.Select(a => EntidadeParaResposta(a)).ToList();
        }

        private static MusicaResponse EntidadeParaResposta(Musica musica)
        {
            return new MusicaResponse(musica.Id, musica.Nome, musica.AnoLancamento, musica.Artista.Id, musica.Artista.Nome);
        }
    }
}
