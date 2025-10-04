
using Dapper;
using Peliculas.Abstractions.Interfaces;
using Peliculas.Abstractions.Modelos;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;
using Peliculas.Abstractions.Modelos.Peliculas.Abstractions.Modelos;
using Abstracciones.Modelos;

namespace Peliculas.DA
{
    public class FavoritoRepositorio : IFavoritoRepositorio
    {
        private readonly string _connectionString;

        public FavoritoRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> ActualizarFavorito(ActualizarFavoritoDto favorito)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "sp_ActualizarFavorito";
            var result = await connection.ExecuteAsync("sp_ActualizarFavorito", new
            {
                Id = favorito.Id,
                Comentario = favorito.Comentario,
                Calificacion = favorito.Calificacion
            }, commandType: CommandType.StoredProcedure);

            return result > 0;
        }

        public async Task<IEnumerable<Favorito>> ObtenerFavoritos(int usuarioId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Favorito>(
                "sp_ObtenerFavoritos",
                new { UsuarioId = usuarioId },
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<IEnumerable<Favorito>> ObtenerFavoritosPorUsuario(int usuarioId)
        {
            using var connection = new SqlConnection(_connectionString);
            var favoritos = await connection.QueryAsync<Favorito>(
                "sp_ObtenerFavoritos",
                new { UsuarioId = usuarioId },
                commandType: CommandType.StoredProcedure
            );
            return favoritos;
        }
        public async Task AgregarFavorito(Favorito favorito)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                "sp_AgregarFavorito",
                new
                {
                    UsuarioId = favorito.UsuarioId,
                    ItemId = favorito.ItemId,
                    Tipo = favorito.Tipo,
                    Comentario = favorito.Comentario,
                    Calificacion = favorito.Calificacion// <== Este nombre debe coincidir con el SP
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task EliminarFavorito(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                "sp_EliminarFavorito",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
