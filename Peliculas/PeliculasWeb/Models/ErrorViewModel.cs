namespace PeliculasWeb.Models
{
    public class ErrorViewModel
    {
        // Id único de la solicitud donde ocurrió el error (puede ser null)
        public string? RequestId { get; set; }

        // Indica si se debe mostrar el RequestId (si no está vacío o null)
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
