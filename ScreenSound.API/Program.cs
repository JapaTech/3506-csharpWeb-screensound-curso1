using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ScreenSound.API.Endpoints;
using ScreenSound.BD;
using ScreenSound.Modelos;
using System.Security.Cryptography.Xml;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ScreenSoundContext>();
builder.Services.AddTransient<DAL<Artista>>();
builder.Services.AddTransient<DAL<Musica>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(option =>
{
    option.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    option.SerializerOptions.WriteIndented = true;
    option.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

var app = builder.Build();

app.AddEndPointsArtistas();
app.AddEndPointsMusica();

app.UseSwagger();
app.UseSwaggerUI();
app.Run();
