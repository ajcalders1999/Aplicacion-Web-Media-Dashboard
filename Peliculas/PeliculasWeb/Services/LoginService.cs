using PeliculasWeb.Models;
using System.Text;
using System.Text.Json;

public class LoginService
{
    private readonly HttpClient _httpClient;

    // Constructor que recibe un HttpClient configurado para comunicarse con la API
    public LoginService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        // Establece la URL base de la API (modificar si cambia el puerto)
        _httpClient.BaseAddress = new Uri("https://localhost:7244/api/");
    }

    // Método asíncrono para autenticar un usuario enviando las credenciales
    public async Task<UsuarioViewModel?> AutenticarAsync(LoginViewModel login)
    {
        // Serializa el objeto login a formato JSON
        var json = JsonSerializer.Serialize(login);
        // Crea el contenido HTTP con tipo application/json
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Realiza una petición POST al endpoint "usuarios/login"
        var response = await _httpClient.PostAsync("usuarios/login", content);

        // Si la respuesta no es exitosa, retorna null (usuario no autenticado)
        if (!response.IsSuccessStatusCode)
            return null;

        // Lee la respuesta como cadena JSON
        var body = await response.Content.ReadAsStringAsync();

        // Deserializa el JSON en un objeto ResultadoLogin para obtener los datos
        var resultado = JsonSerializer.Deserialize<ResultadoLogin>(body, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        // Retorna el usuario deserializado o null si no se encontró
        return resultado?.Usuario;
    }
}
