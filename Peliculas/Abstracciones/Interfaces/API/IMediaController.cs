using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Peliculas.Abstractions.Interfaces
{
    public interface IMediaController
    {
        Task<IActionResult> ObtenerPeliculasPorGenero([FromRoute] int generoId);
        Task<IActionResult> ObtenerSeriesPorGenero([FromRoute] int generoId);
    }
}
