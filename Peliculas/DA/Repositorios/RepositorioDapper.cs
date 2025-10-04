using Peliculas.Abstractions.Interfaces.Repositorios;
﻿using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Peliculas.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.DA
{
    public class RepositorioDapper : IRepositorioDapper
    {
        private readonly IConfiguration _configuracion;
        private readonly SqlConnection _conexionBaseDatos;

        public RepositorioDapper(IConfiguration configuracion)
        {
            _configuracion = configuracion;
            _conexionBaseDatos = new SqlConnection(_configuracion.GetConnectionString("DefaultConnection"));
        }

        public SqlConnection ObtenerRepositorio()
        {
            return _conexionBaseDatos;
        }
    }
}