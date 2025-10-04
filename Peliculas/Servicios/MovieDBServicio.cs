using System.Text.Json;
using Peliculas.Abstractions.Interfaces;
using Peliculas.Abstractions.Modelos;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace Peliculas.Servicios
{
    // Servicio para consumir la API de TheMovieDB
    public class MovieDBServicio : IMovieDBServicio
    {
        private readonly HttpClient _httpClient; // Cliente HTTP para hacer las peticiones
        private readonly string _baseUrl;         // URL base de la API
        private readonly string _apiKey;          // Clave API para autenticación

        // Constructor que recibe HttpClient e instancia configuración para obtener URL y clave
        public MovieDBServicio(HttpClient httpClient, IConfiguracion configuracion)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            // Obtiene URL base desde configuración
            _baseUrl = configuracion.ObtenerValor("ExternalAPIs:TheMovieDB:BaseUrl")
                ?? throw new ArgumentNullException("BaseUrl no está configurado");

            // Obtiene clave API desde configuración
            _apiKey = configuracion.ObtenerValor("ExternalAPIs:TheMovieDB:ApiKey")
                ?? throw new ArgumentNullException("ApiKey no está configurado");

            // Establece la URL base en el HttpClient
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        // Obtiene lista de géneros de películas desde la API
        public async Task<IEnumerable<Genero>> ObtenerGenerosDesdeAPI()
        {
            var url = $"genre/movie/list?api_key={_apiKey}&language=es-ES";

            var respuesta = await _httpClient.GetAsync(url);
            respuesta.EnsureSuccessStatusCode(); // Lanza excepción si hay error HTTP

            var contenido = await respuesta.Content.ReadAsStringAsync();

            // Deserializa respuesta JSON en objeto interno
            var resultado = JsonSerializer.Deserialize<TheMovieDBGenerosResponse>(contenido, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Ignora mayúsculas/minúsculas
            });

            // Convierte DTO a modelo de dominio y devuelve
            return resultado?.Genres.Select(g => new Genero
            {
                Id = g.Id,
                Nombre = g.Name
            });
        }

        // Método reutiliza ObtenerGenerosDesdeAPI para obtener géneros de películas
        public async Task<IEnumerable<Genero>> ObtenerGenerosPeliculasDesdeAPI()
        {
            return await ObtenerGenerosDesdeAPI();
        }

        // Obtiene lista de géneros de series desde la API
        public async Task<IEnumerable<Genero>> ObtenerGenerosSeriesDesdeAPI()
        {
            var url = $"genre/tv/list?api_key={_apiKey}&language=es-ES";

            var respuesta = await _httpClient.GetAsync(url);
            respuesta.EnsureSuccessStatusCode();

            var contenido = await respuesta.Content.ReadAsStringAsync();

            var resultado = JsonSerializer.Deserialize<TheMovieDBSeriesGenerosResponse>(contenido, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return resultado?.Genres.Select(g => new Genero
            {
                Id = g.Id,
                Nombre = g.Name
            });
        }

        // Obtiene películas filtradas por género
        public async Task<IEnumerable<Media>> ObtenerPeliculasPorGenero(int generoId)
        {
            var url = $"discover/movie?api_key={_apiKey}&language=es-ES&with_genres={generoId}";
            var respuesta = await _httpClient.GetAsync(url);

            if (!respuesta.IsSuccessStatusCode)
                throw new Exception($"Error al consultar películas: {respuesta.StatusCode}");

            var contenido = await respuesta.Content.ReadAsStringAsync();
            var resultado = JsonSerializer.Deserialize<TheMovieDBResult>(contenido);

            // Mapea cada resultado a un objeto Media
            return resultado.Results.Select(p =>
            {
                Console.WriteLine($"📷 PosterPath: {p.PosterPath}");

                return new Media
                {
                    Id = p.Id,
                    Title = p.Title,
                    ImageURL = !string.IsNullOrWhiteSpace(p.PosterPath)
                        ? $"https://image.tmdb.org/t/p/w500{p.PosterPath}"
                        : null,
                    Description = p.Overview,
                    ReleaseDate = !string.IsNullOrWhiteSpace(p.ReleaseDate) && DateTime.TryParse(p.ReleaseDate, out var fecha) ? fecha : (DateTime?)null,
                    Rating = p.VoteAverage,
                    MediaType = "Movie"
                };
            });
        }

        // Obtiene detalles de una película por su ID
        public async Task<Media> ObtenerPeliculaPorId(int id)
        {
            var url = $"movie/{id}?api_key={_apiKey}&language=es-ES";
            var respuesta = await _httpClient.GetAsync(url);

            if (!respuesta.IsSuccessStatusCode)
                return null;

            var contenido = await respuesta.Content.ReadAsStringAsync();
            var resultado = JsonSerializer.Deserialize<TheMovieDBItem>(contenido);

            return new Media
            {
                Title = resultado.Title,
                Description = resultado.Overview,
                ImageURL = $"https://image.tmdb.org/t/p/w500{resultado.PosterPath}",
                ReleaseDate = DateTime.TryParse(resultado.ReleaseDate, out var fecha) ? fecha : (DateTime?)null,
                Rating = resultado.VoteAverage,
                MediaType = "Movie"
            };
        }

        // Obtiene detalles de una serie por su ID
        public async Task<Media> ObtenerSeriePorId(int id)
        {
            var url = $"tv/{id}?api_key={_apiKey}&language=es-ES";
            var respuesta = await _httpClient.GetAsync(url);

            if (!respuesta.IsSuccessStatusCode)
                return null;

            var contenido = await respuesta.Content.ReadAsStringAsync();
            var resultado = JsonSerializer.Deserialize<TheMovieDBItem>(contenido);

            return new Media
            {
                Title = resultado.Name,
                Description = resultado.Overview,
                ImageURL = $"https://image.tmdb.org/t/p/w500{resultado.PosterPath}",
                ReleaseDate = DateTime.TryParse(resultado.FirstAirDate, out var fecha) ? fecha : (DateTime?)null,
                Rating = resultado.VoteAverage,
                MediaType = "Series"
            };
        }

        // Obtiene series filtradas por género
        public async Task<IEnumerable<Media>> ObtenerSeriesPorGenero(int generoId)
        {
            var url = $"discover/tv?api_key={_apiKey}&language=es-ES&with_genres={generoId}";
            var respuesta = await _httpClient.GetAsync(url);

            if (!respuesta.IsSuccessStatusCode)
                throw new Exception($"Error al consultar series: {respuesta.StatusCode}");

            var contenido = await respuesta.Content.ReadAsStringAsync();
            var resultado = JsonSerializer.Deserialize<TheMovieDBResult>(contenido);

            return resultado.Results.Select(s => new Media
            {
                Id = s.Id,
                Title = s.Name,
                ImageURL = $"https://image.tmdb.org/t/p/w500{s.PosterPath}",
                Description = s.Overview,
                ReleaseDate = !string.IsNullOrWhiteSpace(s.ReleaseDate) && DateTime.TryParse(s.ReleaseDate, out var fecha) ? fecha : (DateTime?)null,
                Rating = s.VoteAverage,
                MediaType = "Series"
            });
        }

        // Clases internas para mapear respuestas JSON de la API

        private class TheMovieDBGenerosResponse
        {
            [JsonPropertyName("genres")]
            public List<GeneroTMDBDto> Genres { get; set; }
        }
        private class GeneroTMDBDto
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }
        }
        private class TheMovieDBResult
        {
            [JsonPropertyName("results")]
            public List<TheMovieDBItem> Results { get; set; }
        }
        private class TheMovieDBSeriesGenerosResponse
        {
            [JsonPropertyName("genres")]
            public List<GeneroTMDBDto> Genres { get; set; }
        }
        private class TheMovieDBItem
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("title")]
            public string Title { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("overview")]
            public string Overview { get; set; }

            [JsonPropertyName("poster_path")]
            public string PosterPath { get; set; }

            [JsonPropertyName("release_date")]
            public string ReleaseDate { get; set; }

            [JsonPropertyName("first_air_date")]
            public string FirstAirDate { get; set; }

            [JsonPropertyName("vote_average")]
            public double VoteAverage { get; set; }
        }
    }
}
