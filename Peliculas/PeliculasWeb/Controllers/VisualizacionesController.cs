using Microsoft.AspNetCore.Mvc;
using PeliculasWeb.Models;
using PeliculasWeb.Services;

namespace PeliculasWeb.Controllers
{
    public class VisualizacionController : Controller
    {
        private readonly VisualizacionService _visualizacionService;

        // Constructor: inicializa el servicio de visualización
        public VisualizacionController()
        {
            _visualizacionService = new VisualizacionService();
        }

        // Acción para mostrar la lista de visualizaciones
        public async Task<IActionResult> Index()
        {
            var lista = await _visualizacionService.ObtenerListaVisualizacion();
            return View(lista);
        }

        // Acción para mostrar formulario de creación
        public IActionResult Crear()
        {
            return View();
        }

        // Acción POST para crear una nueva visualización
        [HttpPost]
        public async Task<IActionResult> Crear(VisualizacionViewModel visualizacion)
        {
            // Validar modelo y agregar a la lista si es válido
            if (ModelState.IsValid)
            {
                await _visualizacionService.AgregarALista(visualizacion);
                return RedirectToAction("Index");
            }
            // Si hay errores, volver a mostrar el formulario con datos
            return View(visualizacion);
        }

        // Acción para eliminar una visualización por id
        public async Task<IActionResult> Eliminar(int id)
        {
            await _visualizacionService.EliminarDeLista(id);
            return RedirectToAction("Index");
        }
    }
}
