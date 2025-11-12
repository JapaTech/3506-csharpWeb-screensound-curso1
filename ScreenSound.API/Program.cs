using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ScreenSound.BD;
using ScreenSound.Modelos;
using System.Security.Cryptography.Xml;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ScreenSoundContext>();
builder.Services.AddTransient<DAL<Artista>>();
builder.Services.AddTransient<DAL<Musica>>();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(option =>
{
    option.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    option.SerializerOptions.WriteIndented = true;
    option.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

var app = builder.Build();

app.MapGet("/Artistas", ([FromServices] DAL<Artista> dal) =>
{
    return Results.Ok(dal.Listar());
});

app.MapGet("/Artistas/{nome}", ([FromServices] DAL <Artista> dal, string nome) =>
{
    var artista = dal.BuscarObjetoExato(a => a.Nome.ToUpper().Equals(nome.ToUpper()));

    if (artista == null) return Results.NotFound();

    // Teste individualmente cada propriedade
    try
    {
        // Teste 1: Apenas ID e Nome
        var teste1 = new { artista.Id, artista.Nome };
        Console.WriteLine("Teste 1 (Id+Nome) funcionou");

        // Teste 2: Adicione uma propriedade por vez
        var teste2 = new { artista.Id, artista.Nome, artista.Bio };
        Console.WriteLine(" Teste 2 (Id+Nome+Bio) funcionou");

        // Teste 2: Adicione uma propriedade por vez
        var teste3 = new { artista.Id, artista.Nome, artista.Bio, artista.FotoPerfil };
        Console.WriteLine(" Teste 3 (Id+Nome+Bio+FotoPerfil) funcionou");
        // Continue testando...

        var teste4 = new { artista.Id, artista.Nome, artista.Bio, artista.FotoPerfil, artista.Musicas };
        Console.WriteLine(" Teste 4 (Id+Nome+Bio+FotoPerfil+Musicas) funcionou");
        return Results.Ok(teste4); // Use o maior teste que funcionou
    }
    catch (Exception ex)
    {
        Console.WriteLine($" Erro em: {ex.Message}");
        return Results.Ok(artista.ToString()); // Fallback
    }
});

app.MapPost("/Artistas", ([FromServices] DAL <Artista> dal, [FromBody]Artista artista) =>
{
    dal.Adicionar(artista);
    return Results.Created($"/Artistas/{artista.Id}", artista);
});

app.MapDelete("/Artistas/{id}", ([FromServices] DAL <Artista> dal, int id) =>
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

app.MapPost("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] Musica musica) =>
{
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

app.MapPut("/Musicas/", ([FromServices] DAL<Musica> dal, Musica musica) =>
{
    var musicaParaAtualizar = dal.BuscarObjetoExato(m => m.Id == musica.Id);
    if (musicaParaAtualizar == null)
        return Results.NotFound();

    musicaParaAtualizar.Nome = musica.Nome;
    musicaParaAtualizar.Artista = musica.Artista;
    dal.Atualizar(musicaParaAtualizar);
    return Results.Ok();
});


app.Run();
