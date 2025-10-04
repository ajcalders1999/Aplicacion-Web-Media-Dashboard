using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasWeb.Data;
using PeliculasWeb.Models;
using System.Threading.Tasks;

namespace PeliculasWeb.Controllers
{
    public class ActoresController : Controller // Controlador para manejar CRUD de actores
    {
        private readonly ApplicationDbContext _context; // Contexto de base de datos

        // Constructor que recibe el contexto mediante inyección de dependencias
        public ActoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Actores - Muestra la lista de actores
        public async Task<IActionResult> Index()
        {
            var actores = await _context.Actores.ToListAsync(); // Obtiene todos los actores
            return View(actores); // Envía la lista a la vista
        }

        // GET: Actores/Details/5 - Muestra detalles de un actor por su ID
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound(); // Si no se envía ID, devuelve 404

            var actor = await _context.Actores.FirstOrDefaultAsync(a => a.ActorId == id); // Busca el actor
            if (actor == null) return NotFound(); // Si no existe, devuelve 404

            return View(actor); // Muestra la vista con los detalles
        }

        // GET: Actores/Create - Muestra el formulario para crear un actor
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actores/Create - Guarda un nuevo actor en la base de datos
        [HttpPost]
        [ValidateAntiForgeryToken] // Previene ataques CSRF
        public async Task<IActionResult> Create(Actor actor)
        {
            if (ModelState.IsValid) // Valida que los datos sean correctos
            {
                _context.Add(actor); // Agrega el actor al contexto
                await _context.SaveChangesAsync(); // Guarda cambios en la base de datos
                return RedirectToAction(nameof(Index)); // Redirige a la lista
            }
            return View(actor); // Si hay errores, vuelve al formulario
        }

        // GET: Actores/Edit/5 - Muestra el formulario para editar un actor
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound(); // Si no hay ID, devuelve 404

            var actor = await _context.Actores.FindAsync(id); // Busca el actor por ID
            if (actor == null) return NotFound(); // Si no existe, devuelve 404

            return View(actor); // Muestra el formulario con los datos
        }

        // POST: Actores/Edit/5 - Guarda los cambios de un actor editado
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Actor actor)
        {
            if (id != actor.ActorId) return NotFound(); // Verifica que el ID coincida

            if (ModelState.IsValid) // Si los datos son válidos
            {
                try
                {
                    _context.Update(actor); // Marca el actor como modificado
                    await _context.SaveChangesAsync(); // Guarda cambios
                }
                catch (DbUpdateConcurrencyException) // Maneja conflictos de concurrencia
                {
                    if (!ActorExists(actor.ActorId)) // Si el actor ya no existe
                        return NotFound();
                    else
                        throw; // Lanza el error si es otro problema
                }
                return RedirectToAction(nameof(Index)); // Redirige a la lista
            }
            return View(actor); // Si hay errores, vuelve al formulario
        }

        // GET: Actores/Delete/5 - Muestra la confirmación para eliminar un actor
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound(); // Si no hay ID, devuelve 404

            var actor = await _context.Actores.FirstOrDefaultAsync(a => a.ActorId == id); // Busca el actor
            if (actor == null) return NotFound(); // Si no existe, devuelve 404

            return View(actor); // Muestra la vista de confirmación
        }

        // POST: Actores/Delete/5 - Elimina el actor después de la confirmación
        [HttpPost, ActionName("Delete")] // Cambia el nombre para diferenciar del GET
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actor = await _context.Actores.FindAsync(id); // Busca el actor
            _context.Actores.Remove(actor); // Lo elimina del contexto
            await _context.SaveChangesAsync(); // Guarda cambios
            return RedirectToAction(nameof(Index)); // Redirige a la lista
        }

        // Verifica si un actor existe en la base de datos
        private bool ActorExists(int id)
        {
            return _context.Actores.Any(e => e.ActorId == id);
        }
    }
}
