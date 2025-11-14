using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.BD;
using ScreenSound.Modelos;
using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class GeneroExtensions
    {
        public static void AddEndPoitsGeneros (this WebApplication app)
        {
            app.MapGet("/Generos", ([FromServices] DAL<Genero> dal) =>
            {
                return Results.Ok(dal.Listar());
            });

            app.MapGet("/Generos/{nome}", ([FromServices] DAL<Genero> dal, string nome) => {
                var genero = dal.BuscarObjetoExato(g => g.Nome.ToUpper().Equals(nome.ToUpper()));
                if(genero == null)
                {
                    return Results.NotFound();
                }
                else
                {
                    return Results.Ok(genero);
                }
            });

            app.MapPost("/Generos", ([FromServices] DAL<Genero> dal, [FromBody] GeneroGetRequest generoRequest) =>
            {
                var genero = new Genero(
                    generoRequest.nome,
                    generoRequest.descricao
                    );
                dal.Adicionar(genero);
                return Results.Created($"/Generos/{genero.Id}", genero);
            });

            app.MapDelete("/Generos/{id}", ([FromServices] DAL<Genero> dal, int id) =>
            {
                var genero = dal.BuscarObjetoExato(g => g.Id == id);
                if(genero == null)
                {
                    return Results.NotFound();
                }
                else
                {
                    dal.Deletar(genero);
                    return Results.NoContent();
                }
            });

            app.MapPut("/Generos", ([FromServices] DAL<Genero> dal, [FromBody] GeneroGetRequest generoRequest) =>
            {
                var generoParaAtualizar = dal.BuscarObjetoExato(g => g.Id == generoRequest.id);
                if(generoParaAtualizar != null)
                {
                    generoParaAtualizar.Nome = generoRequest.nome;
                    generoParaAtualizar.Descricao = generoRequest.descricao;
                    dal.Atualizar(generoParaAtualizar);
                    return Results.Ok();
                }
                else
                {
                    return Results.NotFound();
                }
            });
        }
    }
}
