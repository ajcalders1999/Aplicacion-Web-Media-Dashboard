namespace PeliculasWeb.Models
{
    public class ErrorViewModel
    {
        // Id �nico de la solicitud donde ocurri� el error (puede ser null)
        public string? RequestId { get; set; }

        // Indica si se debe mostrar el RequestId (si no est� vac�o o null)
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
