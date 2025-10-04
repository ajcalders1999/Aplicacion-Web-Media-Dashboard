
using Microsoft.Data.SqlClient;
using System.Data;

namespace Peliculas.Abstractions.Interfaces.Repositorios
{
    public interface IRepositorioDapper
    {
        SqlConnection ObtenerRepositorio();
    }
}
