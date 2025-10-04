using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasWeb.Data;
using PeliculasWeb.Models;
using PeliculasWeb.Services;

public class CuentaController : Controller // Controlador para manejar el inicio de sesión, registro y cierre de sesión
{
    private readonly LoginService _loginService; // Servicio para autenticar usuarios

    private readonly ApplicationDbContext _context; // Contexto de base de datos

    // Constructor que recibe el servicio de login y el contexto de base de datos
    public CuentaController(LoginService loginService, ApplicationDbContext context)
    {
        _loginService = loginService;
        _context = context;
    }

    [HttpGet] // Muestra la vista de login
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost] // Procesa los datos del login
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) // Si los datos no son válidos, regresa la vista
            return View(model);

        var usuario = await _loginService.AutenticarAsync(model); // Llama al servicio para autenticar

        if (usuario == null) // Si no se encuentra el usuario, muestra error
        {
            ModelState.AddModelError(string.Empty, "Credenciales inválidas");
            return View(model);
        }

        // Guarda datos del usuario en la sesión
        HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
        HttpContext.Session.SetString("NombreUsuario", usuario.UserName ?? "");

        // Redirige a la lista de películas
        return RedirectToAction("Index", "Peliculas");
    }

    // GET: Usuarios/Create - Muestra el formulario de registro
    public IActionResult Registrarse()
    {
        return View();
    }

    // POST: Usuarios/Create - Procesa el registro de un nuevo usuario
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Registrarse(UsuarioModel usuario)
    {
        if (ModelState.IsValid) // Si el modelo es válido
        {
            // Verificar si ya existe un usuario con el mismo nombre de usuario
            bool usuarioExiste = await _context.User.AnyAsync(u => u.UserName == usuario.UserName);
            if (usuarioExiste)
            {
                ModelState.AddModelError("UserName", "El nombre de usuario ya está en uso");
                return View(usuario);
            }

            // Agrega el nuevo usuario a la base de datos
            _context.Add(usuario);
            await _context.SaveChangesAsync();

            // Redirige al login
            return RedirectToAction(nameof(Login));
        }
        // Si hay errores, vuelve a mostrar el formulario
        return View(usuario);
    }

    // Cierra la sesión del usuario
    public IActionResult Logout()
    {
        HttpContext.Session.Clear(); // Limpia la sesión
        return RedirectToAction("Login"); // Redirige al login
    }
}
