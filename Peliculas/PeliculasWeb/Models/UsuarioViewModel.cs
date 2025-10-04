using System.Text.Json.Serialization;

namespace PeliculasWeb.Models
{
    public class UsuarioViewModel
    {
        // Identificador único del usuario
        public int Id { get; set; }

        // Nombre de usuario, mapeado al JSON con la propiedad "userName"
        [JsonPropertyName("userName")] // 👈 Esto es clave para serialización/deserialización
        public string UserName { get; set; }

        // Contraseña del usuario
        public string Password { get; set; }
    }
}
