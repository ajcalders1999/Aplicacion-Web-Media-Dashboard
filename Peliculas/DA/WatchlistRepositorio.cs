
using Dapper;
using Peliculas.Abstractions.Interfaces;
using Peliculas.Abstractions.Modelos;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;
using Peliculas.Abstractions.Modelos.Peliculas.Abstractions.Modelos;

namespace Peliculas.DA
{
    public class WatchlistRepositorio : IWatchlistRepositorio
    {
        private readonly string _connectionString;

        public WatchlistRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<Watchlist> ObtenerPorId(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Watchlist>(
                "sp_ObtenerWatchlistPorId",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );
        }


        public async Task<IEnumerable<Watchlist>> ObtenerWatchlist(int usuarioId)
        {
            using var connection = new SqlConnection(_connectionString);
            var resultado = await connection.QueryAsync<Watchlist>(
                "sp_ObtenerWatchlistPorUsuario",
                new { UsuarioId = usuarioId },
                commandType: CommandType.StoredProcedure
            );
            return resultado;
        }

        public async Task AgregarWatchlist(Watchlist watchlist)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                "sp_AgregarWatchlist",
                new
                {
                    watchlist.UsuarioId,
                    watchlist.ItemId,
                    watchlist.Tipo,
                    watchlist.Prioridad
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task EliminarWatchlist(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                "sp_EliminarWatchlist",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<IEnumerable<Watchlist>> ObtenerTodosAsync()
        {
            return null;//await _context.Watchlist.ToListAsync();
        }

        public async Task<Watchlist> ObtenerWatchlistPorId(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Watchlist>(
                "sp_ObtenerWatchlistPorId",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<bool> ActualizarWatchlist(Watchlist watchlist)
        {
            using var connection = new SqlConnection(_connectionString);
            var filasAfectadas = await connection.ExecuteAsync(
                "sp_ActualizarWatchlist",
                new
                {
                    watchlist.Id,
                    watchlist.Prioridad
                },
                commandType: CommandType.StoredProcedure
            );

            return filasAfectadas > 0;
        }

    }
}
