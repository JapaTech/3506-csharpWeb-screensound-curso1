using Microsoft.AspNetCore.Mvc;
using ScreenSound.BD;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class ArtistaExtensions
    {
        public static void AddEndPointsArtistas(this WebApplication app)
        {
            app.MapGet("/Artistas", ([FromServices] DAL<Artista> dal) =>
            {
                return Results.Ok(dal.Listar());
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
                return Results.Ok(artista.ToString());
            });

            app.MapPost("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody] Artista artista) =>
            {
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

            app.MapPut("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody] Artista artista) =>
            {
                var artistaParaAtualizar = dal.BuscarObjetoExato(a => a.Id == artista.Id);
                if (artistaParaAtualizar == null)
                    return Results.NotFound();

                artistaParaAtualizar.Nome = artista.Nome;
                artistaParaAtualizar.Bio = artista.Bio;
                artistaParaAtualizar.FotoPerfil = artista.FotoPerfil;

                dal.Atualizar(artistaParaAtualizar);
                return Results.Ok();
            });
        }
    }
}
