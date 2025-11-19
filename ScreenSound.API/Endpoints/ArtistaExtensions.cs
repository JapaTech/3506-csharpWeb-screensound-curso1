using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.BD;
using ScreenSound.Modelos;
using System.Collections;

namespace ScreenSound.API.Endpoints
{
    public static class ArtistaExtensions
    {
        public static void AddEndPointsArtistas(this WebApplication app)
        {
            app.MapGet("/Artistas", ([FromServices] DAL<Artista> dal) =>
            {
                return Results.Ok(ArtistaResponseToList(dal.Listar()));
            });

            app.MapGet("/Artistas/{nome}", ([FromServices] DAL<Artista> dal, string nome) =>
            {
                var artista = dal.BuscarObjetoExato(a => a.Nome.ToUpper().Equals(nome.ToUpper()));

                if (artista == null) return Results.NotFound();

                /* Teste individualmente cada propriedade
                //try
                //{
                //    // Teste 1: Apenas ID e Nome
                //    var teste1 = new { artista.Id, artista.Nome };
                //    Console.WriteLine("Teste 1 (Id+Nome) funcionou");

                //    // Teste 2: Adicione uma propriedade por vez
                //    var teste2 = new { artista.Id, artista.Nome, artista.Bio };
                //    Console.WriteLine(" Teste 2 (Id+Nome+Bio) funcionou");

                //    // Teste 2: Adicione uma propriedade por vez
                //    var teste3 = new { artista.Id, artista.Nome, artista.Bio, artista.FotoPerfil };
                //    Console.WriteLine(" Teste 3 (Id+Nome+Bio+FotoPerfil) funcionou");
                //    // Continue testando...

                //    var teste4 = new { artista.Id, artista.Nome, artista.Bio, artista.FotoPerfil, artista.Musicas };
                //    Console.WriteLine(" Teste 4 (Id+Nome+Bio+FotoPerfil+Musicas) funcionou");
                //    return Results.Ok(teste4); // Use o maior teste que funcionou
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine($" Erro em: {ex.Message}");
                //    return Results.Ok(artista.ToString()); // Fallback
                //}
                */
                return Results.Ok(EntidadeParaResposta(artista));
            });

            app.MapPost("/Artistas", async ([FromServices]IHostEnvironment env, [FromServices] DAL<Artista> dal, [FromBody] ArtistaGetRequest artistaRequest) =>
            {
                var nome = artistaRequest.nome.Trim();
                var nomeArquivoImagem = DateTime.Now.ToString("ddMMyyyyhhss") + "_" + nome + ".jpeg";

                var path = Path.Combine(env.ContentRootPath, "wwwroot/FotosPerfil", nomeArquivoImagem);

                using MemoryStream ms = new MemoryStream(Convert.FromBase64String(artistaRequest.fotoPefil));
                using FileStream fs = new FileStream(path, FileMode.Create);
                await ms.CopyToAsync(fs);

                var artista = new Artista
                (artistaRequest.nome, artistaRequest.bio)
                { FotoPerfil = $"/FotosPerfil/{nomeArquivoImagem}"};
                dal.Adicionar(artista);
                return Results.Created($"/Artistas/{artista.Id}", artista);
            });

            app.MapDelete("/Artistas/{id}", ([FromServices] DAL<Artista> dal, int id) =>
            {
                var artista = dal.BuscarObjetoExato(a => a.Id == id);
                if (artista == null)
                    return Results.NotFound();
                dal.Deletar(artista);
                return Results.NoContent();
            });

            app.MapPut("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody] ArtistaEditRequest artista) =>
            {
                var artistaParaAtualizar = dal.BuscarObjetoExato(a => a.Id == artista.id);
                if (artistaParaAtualizar == null)
                    return Results.NotFound();

                artistaParaAtualizar.Nome = artista.nome;
                artistaParaAtualizar.Bio = artista.bio;
                artistaParaAtualizar.FotoPerfil = artista.fotoPerfil;

                dal.Atualizar(artistaParaAtualizar);
                return Results.Ok();
            });
        }

        private static ICollection<ArtistaResponse> ArtistaResponseToList(IEnumerable<Artista> lista)
        {
            return lista.Select(a => EntidadeParaResposta(a)).ToList();
        }

        private static ArtistaResponse EntidadeParaResposta(Artista artista)
        {
            return new ArtistaResponse(artista.Id, artista.Nome, artista.Bio, artista.FotoPerfil);  
        }
    }
}
