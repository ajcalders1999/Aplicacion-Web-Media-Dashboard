using Peliculas.Abstractions.Interfaces;
using Peliculas.Abstractions.Interfaces.Flujo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reglas
{
    public class GeneroReglas : IGeneroReglas
    {
        private readonly IGeneroFlujo _generoFlujo;

        // Constructor que recibe la implementación del flujo de géneros
        public GeneroReglas(IGeneroFlujo generoFlujo)
        {
            _generoFlujo = generoFlujo;
        }

        // Método para validar que un género con el id dado existe
        public async Task ValidarGeneroExistente(int generoId)
        {
            // Obtener la lista de géneros
            var generos = await _generoFlujo.ObtenerGeneros();

            // Verificar si el género existe; si no, lanzar excepción
            if (!generos.Any(g => g.Id == generoId))
            {
                throw new KeyNotFoundException("El género especificado no existe.");
            }
        }
    }
}
