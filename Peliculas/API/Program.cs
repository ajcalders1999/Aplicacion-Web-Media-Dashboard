using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Peliculas.Abstractions.Interfaces;
using Peliculas.Abstractions.Interfaces.Flujo;
using Peliculas.Flujo;
using Peliculas.DA;
using Peliculas.Servicios;
using Peliculas.Abstractions.Interfaces.DA;
using Peliculas.Abstractions.Interfaces.Repositorios;
using Microsoft.Extensions.Configuration;
using Reglas;

var builder = WebApplication.CreateBuilder(args);

// ? Agregar configuración del archivo appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add services to the container.
builder.Services.AddControllers();

// ✅ CONFIGURAR CORS para permitir conexión desde el frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.WithOrigins("https://localhost:7009") // Cambiar a http:// si tu Web no usa HTTPS
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Registro de dependencias
builder.Services.AddScoped<IFavoritoFlujo, FavoritoFlujo>();
builder.Services.AddScoped<IGeneroFlujo, GeneroFlujo>();
builder.Services.AddScoped<IMediaFlujo, MediaFlujo>();
builder.Services.AddScoped<IUsuarioFlujo, UsuarioFlujo>();
builder.Services.AddScoped<IWatchlistFlujo, WatchlistFlujo>();

builder.Services.AddSingleton<IConfiguracion, Configuracion>();
builder.Services.AddScoped<IFavoritoRepositorio, FavoritoRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IWatchlistRepositorio, WatchlistRepositorio>();
builder.Services.AddScoped<IRepositorioDapper, RepositorioDapper>();

builder.Services.AddHttpClient<IMovieDBServicio, MovieDBServicio>();
builder.Services.AddScoped<IConfiguracion, Configuracion>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✅ APLICAR CORS (antes de Authorization, si usás JWT)
app.UseCors("PermitirFrontend");

app.MapControllers();

app.Run();

