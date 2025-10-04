using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Peliculas.Abstractions.Modelos;
using Peliculas.Abstractions.Interfaces;
using Peliculas.Abstractions.Interfaces.DA;
using Peliculas.Abstractions.Interfaces.Repositorios;

namespace Peliculas.DA
{
    public class MediaDA : IMediaDA
    {
        private readonly IRepositorioDapper _repositorioDapper;

        public MediaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
        }

        /// <summary>
        /// Obtiene todos los registros de Media (películas y series) usando un procedimiento almacenado.
        /// Se asume que existe un SP llamado "sp_ObtenerMedias".
        /// </summary>
        public async Task<IEnumerable<Media>> ObtenerTodos()
        {
            using var conn = _repositorioDapper.ObtenerRepositorio();
            string query = "sp_ObtenerMedias";
            var resultadoConsulta = await conn.QueryAsync<Media>(
                query,
                commandType: CommandType.StoredProcedure
            );
            return resultadoConsulta;
        }

        /// <summary>
        /// Obtiene las películas (MediaType = 'Movie') según el género.
        /// Se asume que existe un SP llamado "sp_ListMoviesByGenre".
        /// </summary>
        public async Task<IEnumerable<Media>> ObtenerPeliculasPorGenero(int generoId)
        {
            using var conn = _repositorioDapper.ObtenerRepositorio();
            string query = "sp_ListMoviesByGenre";
            var parametros = new { GenreID = generoId };
            var resultadoConsulta = await conn.QueryAsync<Media>(
                query,
                parametros,
                commandType: CommandType.StoredProcedure
            );
            return resultadoConsulta;
        }

        /// <summary>
        /// Obtiene las series (MediaType = 'Series') según el género.
        /// Se asume que existe un SP llamado "sp_ListSeriesByGenre".
        /// </summary>
        public async Task<IEnumerable<Media>> ObtenerSeriesPorGenero(int generoId)
        {
            using var conn = _repositorioDapper.ObtenerRepositorio();
            string query = "sp_ListSeriesByGenre";
            var parametros = new { GenreID = generoId };
            var resultadoConsulta = await conn.QueryAsync<Media>(
                query,
                parametros,
                commandType: CommandType.StoredProcedure
            );
            return resultadoConsulta;
        }

        /// <summary>
        /// Obtiene un registro de Media a partir de su ID usando una consulta directa.
        /// </summary>
        public async Task<Media> ObtenerMediaPorId(int mediaId)
        {
            using var conn = _repositorioDapper.ObtenerRepositorio();
            string query = "SELECT * FROM Media WHERE MediaID = @MediaID";
            var resultado = await conn.QueryFirstOrDefaultAsync<Media>(
                query,
                new { MediaID = mediaId }
            );
            return resultado;
        }

        /// <summary>
        /// Crea un nuevo registro de Media y retorna el ID generado.
        /// Se usa una consulta directa; en producción se recomienda un procedimiento almacenado.
        /// </summary>
        public async Task<int> CrearMedia(Media media)
        {
            using var conn = _repositorioDapper.ObtenerRepositorio();
            string query = @"
                INSERT INTO Media (Title, Description, ReleaseDate, ImageURL, Rating, MediaType, GenreID)
                VALUES (@Title, @Description, @ReleaseDate, @ImageURL, @Rating, @MediaType, @GenreID);
                SELECT CAST(SCOPE_IDENTITY() as int);";
            int nuevoId = await conn.ExecuteScalarAsync<int>(query, media);
            return nuevoId;
        }

        /// <summary>
        /// Actualiza un registro existente de Media.
        /// </summary>
        public async Task ActualizarMedia(Media media)
        {
            using var conn = _repositorioDapper.ObtenerRepositorio();
            string query = @"
                UPDATE Media 
                SET Title = @Title,
                    Description = @Description,
                    ReleaseDate = @ReleaseDate,
                    ImageURL = @ImageURL,
                    Rating = @Rating,
                    MediaType = @MediaType,
                    GenreID = @GenreID
                WHERE MediaID = @MediaID";
            await conn.ExecuteAsync(query, media);
        }

        /// <summary>
        /// Elimina un registro de Media a partir de su ID.
        /// </summary>
        public async Task EliminarMedia(int mediaId)
        {
            using var conn = _repositorioDapper.ObtenerRepositorio();
            string query = "DELETE FROM Media WHERE MediaID = @MediaID";
            await conn.ExecuteAsync(query, new { MediaID = mediaId });
        }
    }
}
