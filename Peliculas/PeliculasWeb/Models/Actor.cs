using System;
using System.ComponentModel.DataAnnotations;

namespace PeliculasWeb.Models
{
    public class Actor
    {
        // Identificador único del actor
        public int ActorId { get; set; }

        // Nombre del actor, campo obligatorio, máximo 100 caracteres
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; }

        // Apellido del actor, campo obligatorio, máximo 100 caracteres
        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(100)]
        public string Apellido { get; set; }

        // Fecha de nacimiento, tipo Date, puede ser nulo
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime? FechaNacimiento { get; set; }

        // Nacionalidad del actor, máximo 50 caracteres, campo opcional
        [StringLength(50)]
        public string Nacionalidad { get; set; }

        // Biografía del actor, texto multilínea, campo opcional
        [DataType(DataType.MultilineText)]
        public string Biografia { get; set; }
    }
}
