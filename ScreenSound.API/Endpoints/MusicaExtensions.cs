using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.BD;
using ScreenSound.Modelos;
using ScreenSound.Shared.Modelos.Modelos;

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

            app.MapPost("/Musicas", ([FromServices] DAL<Musica> dal,
                [FromServices] DAL <Genero> dalGenero,
                [FromBody] MusicaGetRequest musicaReq) =>
            {      
                var musica = new Musica(
                    musicaReq.nome, 
                    musicaReq.anoLancamento,
                    musicaReq.artistaId,
                    GeneroRequestParaEntidade(musicaReq.Generos, dalGenero) ?? new List<Genero>());
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

        private static ICollection<Genero> GeneroRequestParaEntidade(ICollection<GeneroGetRequest> lista,
            DAL<Genero> dal)
        {
            var listaDeGeneros = new List<Genero>();
            
            foreach (var generoReq in lista)
            {
                var generoExistente = dal.BuscarObjetoExato(g => g.Nome.ToUpper() == generoReq.nome.ToUpper());
                if (generoExistente != null)
                {
                    listaDeGeneros.Add(generoExistente);
                }
                else
                {
                    var novoGenero = new Genero(generoReq.nome, generoReq.descricao);
                    dal.Adicionar(novoGenero);
                    listaDeGeneros.Add(novoGenero);
                }
            }
            return listaDeGeneros;
        }

        private static ICollection<MusicaResponse> EntidadeParaListaResposta(IEnumerable<Musica> lista)
        {
            return lista.Select(a => EntidadeParaResposta(a)).ToList();
        }

        private static MusicaResponse EntidadeParaResposta(Musica musica)
        {
            return new MusicaResponse(musica.Id, 
                musica.Nome, 
                musica.AnoLancamento,
                musica.Artista.Id, 
                musica.Artista.Nome);
        }
    }
}
