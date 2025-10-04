using Microsoft.Extensions.Configuration;
using Peliculas.Abstractions.Interfaces;
using System;
using System.Linq;

namespace Reglas
{
    public class Configuracion : IConfiguracion
    {
        private IConfiguration _configuracion;

        // Constructor que recibe la configuración general (appsettings.json, variables, etc.)
        public Configuracion(IConfiguration configuracion)
        {
            _configuracion = configuracion;
        }

        // Obtiene la URL completa para un método de la API, según la sección, nombre y generoId
        public string ObtenerMetodo(string seccion, string nombre, int generoId)
        {
            // Obtiene la configuración de la sección (ejemplo: "TMDb")
            var apiEndpoint = _configuracion.GetSection(seccion).Get<APIEndPoint>();
            if (apiEndpoint == null)
            {
                throw new Exception($"La sección '{seccion}' no existe o no está configurada correctamente.");
            }

            // Busca el método por nombre dentro de la sección
            var metodo = apiEndpoint.Metodos.FirstOrDefault(m => m.Nombre == nombre);
            if (metodo == null)
            {
                throw new Exception($"No se encontró un método con el nombre '{nombre}' en la sección '{seccion}'.");
            }

            // Reemplaza {0} en la cadena Valor con el generoId (ejemplo: "movies?genre={0}" -> "movies?genre=28")
            var finalValor = string.Format(metodo.Valor, generoId);

            // Construye la URL completa con UrlBase + método formateado + api_key
            return $"{apiEndpoint.UrlBase}{finalValor}&api_key={apiEndpoint.ApiKey}";
        }

        // Obtiene un valor de configuración directo por clave (ejemplo: "ConnectionStrings:DefaultConnection")
        public string ObtenerValor(string llave)
        {
            return _configuracion[llave];
        }

        // Obtiene solo la URL base de una sección (no usada públicamente en este código)
        private string ObtenerUrlBase(string seccion)
        {
            return _configuracion.GetSection(seccion).Get<APIEndPoint>().UrlBase;
        }
    }
}
