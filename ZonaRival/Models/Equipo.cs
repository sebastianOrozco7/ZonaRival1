using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using ZonaRival.Models.ViewModels;

namespace ZonaRival.Models
{
    public class Equipo
    {
        public int EquipoId { get; set; }
        [Required(ErrorMessage = "El nombre del equipo es obligatorio.")]
        public string NombreEquipo { get; set; } = string.Empty;
        [Required(ErrorMessage = "La cantidad de jugadores es obligatoria.")]
        [Range(1, 20, ErrorMessage = "La cantidad de jugadores debe estar entre 1 y 20.")]
        public int CantidadJugadores { get; set; } = 0;
        [Required(ErrorMessage = "El rango de edad es obligatorio.")]
        public string RangoEdad { get; set; } = string.Empty;
        [Required(ErrorMessage = "El color de uniforme es obligatorio.")]
        public string ColorUniforme { get; set; } = string.Empty;
        public bool Disponibilidad { get; set; } = false;

        // Relación con Usuario
        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
        // Relación muchos a muchos con Cancha
        public List<EquipoCancha> EquiposCanchas { get; set; } = new List<EquipoCancha>();
        //Relacion con Partido
        public List<Partido> PartidosComoRetador { get; set; } = new();
        public List<Partido> PartidosComoDesafiado { get; set; } = new();
    }
}
