using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ZonaRival.Models.ViewModels;

namespace ZonaRival.Models
{
    public class Equipo
    {
        public int EquipoId { get; set; }
        public string NombreEquipo { get; set; } = string.Empty;
        public int CantidadJugadores { get; set; } = 0;
        public string RangoEdad { get; set; } = string.Empty;
        public string ColorUniforme { get; set; } = string.Empty;
        public bool Disponibilidad { get; set; } = false;

        // Relación con Usuario
        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
        // Relación muchos a muchos con Cancha
        public List<EquipoCancha> EquiposCanchas { get; set; } = new List<EquipoCancha>();

        //Relacion EquipoPartido
        public List<EquipoPartido> equipoPartidos { get; set; } = new List<EquipoPartido>();
    }
}
