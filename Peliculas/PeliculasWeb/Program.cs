using Peliculas.Abstractions.Interfaces;
using Peliculas.Servicios;
using PeliculasWeb.Controllers;
using PeliculasWeb.Data;
using PeliculasWeb.Services;
using Reglas;
using Microsoft.EntityFrameworkCore;
using PeliculasWeb.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurar el contexto de base de datos con SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar servicios MVC con vistas
builder.Services.AddControllersWithViews();

// Registrar HttpClient básico
builder.Services.AddHttpClient();

// Registrar HttpClient con base URL para la API
builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("https://localhost:7244/api/");
});

// Registrar HttpClient para servicios específicos
builder.Services.AddHttpClient<PeliculasService>();
builder.Services.AddHttpClient<HomeController>(); // ✅ Esta línea permite inyectar HttpClient en HomeController
builder.Services.AddHttpClient<MovieDBServicio>(); // Servicio que usa HttpClient vía inyección

// Registrar servicios de aplicación y dependencias
builder.Services.AddScoped<IConfiguracion, Configuracion>(); // Implementación de configuración
builder.Services.AddHttpClient<FavoritosService>();
builder.Services.AddHttpClient<WatchlistService>();

// Habilitar sesión para la aplicación
builder.Services.AddSession();

// Servicio para login con HttpClient
builder.Services.AddHttpClient<LoginService>();

// Servicio para series (sin HttpClient explícito aquí, probablemente hereda ApiService)
builder.Services.AddScoped<SeriesService>();

// Permite acceder a HttpContext en controladores y servicios
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configuración del pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Página de error personalizada en producción
    app.UseHsts(); // Seguridad HTTP Strict Transport Security
}

app.UseSession(); // Habilitar sesión
app.UseHttpsRedirection(); // Redirigir HTTP a HTTPS
app.UseStaticFiles(); // Habilitar archivos estáticos (css, js, imágenes, etc.)

app.UseRouting(); // Habilitar routing

app.UseAuthorization(); // Habilitar autorización

// Rutas de controladores
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cuenta}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Peliculas}/{action=Index}/{id?}");

// Ejecutar la aplicación
app.Run();
