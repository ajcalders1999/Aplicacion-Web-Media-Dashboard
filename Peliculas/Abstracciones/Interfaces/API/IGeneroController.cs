using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.Abstractions.Interfaces
{
    public interface IGeneroController
    {
        Task<IActionResult> ObtenerGeneros();
    }
}
