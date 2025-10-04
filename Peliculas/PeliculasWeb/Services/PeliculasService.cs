using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PeliculasWeb.Models;
using System;
using System.Text.Json.Serialization;

namespace PeliculasWeb.Services
{
    public class PeliculasService
    {
        private readonly HttpClient _httpClient;
        // Clave de API para TMDb (reemplazar por tu propia clave)
        private const string ApiKey = "d49e442f7fd045de13717ddfd651331d";

        // Constructor que recibe HttpClient para hacer peticiones HTTP
        public PeliculasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Método para obtener películas por género usando la API de TMDb
        public async Task<List<MediaViewModel>> ObtenerPeliculasPorGenero(int idGenero)
        {
            // Construye la URL con el id del género y la clave de API
            string url = $"https://api.themoviedb.org/3/discover/movie?api_key={ApiKey}&with_genres={idGenero}&language=es-ES";

            // Hace una petición GET y deserializa la respuesta JSON en TMDbResponse
            var response = await _httpClient.GetFromJsonAsync<TMDbResponse>(url);

            // Si la respuesta o resultados es nulo, retorna lista vacía
            if (response?.Results == null)
                return new List<MediaViewModel>();

            // Mapea los resultados de TMDb al modelo MediaViewModel
            return response.Results.Select(p => new MediaViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Overview,
                ImageURL = !string.IsNullOrEmpty(p.PosterPath)
                    ? $"https://image.tmdb.org/t/p/w500{p.PosterPath}"  // URL completa de la imagen
                    : "/images/no-image.png",  // Imagen por defecto si no hay poster
                ReleaseDate = !string.IsNullOrEmpty(p.ReleaseDate)
                    ? DateTime.Parse(p.ReleaseDate)  // Convierte string a DateTime
                    : (DateTime?)null,
                Rating = p.VoteAverage
            }).ToList();
        }
    }

    // Clase para recibir la respuesta principal de TMDb con la lista de películas
    public class TMDbResponse
    {
        public List<TMDbMovieResult> Results { get; set; }
    }

    // Modelo que representa cada película en la respuesta TMDb
    public class TMDbMovieResult
    {
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }
    }
}