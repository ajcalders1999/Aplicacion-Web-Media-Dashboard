
using Dapper;
using Peliculas.Abstractions.Interfaces;
using Peliculas.Abstractions.Modelos;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;
using Peliculas.Abstractions.Modelos.Peliculas.Abstractions.Modelos;

namespace Peliculas.DA
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly string _connectionString;

        public UsuarioRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Usuario> ObtenerUsuarioPorCredenciales(string username, string password)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Usuario>(
                "sp_ObtenerUsuarioPorCredenciales",
                new { UserName = username, Password = password },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<ResultadoOperacion> Registrar(Usuario usuario)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                "sp_RegistrarUsuario",
                new { usuario.UserName, usuario.Password },
                commandType: CommandType.StoredProcedure
            );

            return new ResultadoOperacion { Exitoso = true, Mensaje = "Usuario registrado correctamente" };
        }
    }
}
