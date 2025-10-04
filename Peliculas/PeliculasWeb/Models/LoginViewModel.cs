using System.ComponentModel.DataAnnotations;

namespace PeliculasWeb.Models
{
    public class LoginViewModel
    {
        // Nombre de usuario para el login
        public string UserName { get; set; }

        // Contraseña para el login
        public string Password { get; set; }
    }
}
