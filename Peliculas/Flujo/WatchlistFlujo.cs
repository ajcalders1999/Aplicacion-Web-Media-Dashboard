
using Abstracciones.Modelos;
using Newtonsoft.Json;
using Peliculas.Abstractions.Interfaces;
using Peliculas.Abstractions.Interfaces.Flujo;
using Peliculas.Abstractions.Modelos;
using Peliculas.Abstractions.Modelos.Peliculas.Abstractions.Modelos;
using System.Net.Http;

namespace Peliculas.Flujo
{
    public class WatchlistFlujo : IWatchlistFlujo
    {
        private readonly IWatchlistRepositorio _repositorio;
        private readonly HttpClient _httpClient;
        public WatchlistFlujo(IWatchlistRepositorio repositorio, HttpClient httpClient)
        {
            _repositorio = repositorio;
            _httpClient = httpClient;
        }
        public async Task<Watchlist> ObtenerWatchlistPorId(int id)
        {
            return await _repositorio.ObtenerPorId(id);
        }

        public async Task AgregarWatchlist(Watchlist watchlist)
        {
            await _repositorio.AgregarWatchlist(watchlist);
        }

        public async Task EliminarWatchlist(int id)
        {
            await _repositorio.EliminarWatchlist(id);
        }

        public async Task<List<Watchlist>> ObtenerWatchlistPorUsuario(int usuarioId)
        {
            var watchlist = await _repositorio.ObtenerWatchlist(usuarioId);
            return watchlist.ToList(); // 🔥 Conversión explícita a List<Watchlist>
        }
        public async Task<IEnumerable<Watchlist>> ObtenerWatchlist(int usuarioId)
        {
            return await _repositorio.ObtenerWatchlist(usuarioId);
        }

        public async Task<bool> ActualizarWatchlist(Watchlist watchlist)
        {
            return await _repositorio.ActualizarWatchlist(watchlist);
        }
        public async Task<List<WatchlistConDetallesDto>> ObtenerWatchlistConDetalles()
        {
            var elementos = await _repositorio.ObtenerTodosAsync();
            var resultado = new List<WatchlistConDetallesDto>();

            foreach (var item in elementos)
            {
                string url = item.Tipo.ToLower() == "movie"
                    ? $"https://api.themoviedb.org/3/movie/{item.Id}?api_key=d49e442f7fd045de13717ddfd651331d&language=es-ES"
                    : $"https://api.themoviedb.org/3/tv/{item.Id}?api_key=d49e442f7fd045de13717ddfd651331d&language=es-ES";

                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(json);

                    resultado.Add(new WatchlistConDetallesDto
                    {
                        Id = item.Id,
                        Title = item.Tipo.ToLower() == "movie" ? data.title : data.name,
                        ImageURL = "https://image.tmdb.org/t/p/w500" + (string)data.poster_path,
                        Tipo = item.Tipo,
                        Prioridad = item.Prioridad
                    });
                }
            }

            return resultado;
        }
    }
}
