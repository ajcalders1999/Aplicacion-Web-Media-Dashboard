namespace PeliculasWeb.Models
{
    public class ResultadoLogin
    {
        // Indica si el login fue exitoso o no
        public bool Exitoso { get; set; }

        // Mensaje relacionado al resultado del login (error, éxito, etc.)
        public string Mensaje { get; set; }

        // Información del usuario autenticado (si login fue exitoso)
        public UsuarioViewModel Usuario { get; set; }
    }
}
