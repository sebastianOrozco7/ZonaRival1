using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ZonaRival.Models.ViewModels;

namespace ZonaRival.Models
{
    public class Equipo
    {
        public int EquipoId { get; set; }
        public string NombreEquipo { get; set; }
        public int CantidadJugadores { get; set; }
        public string RangoEdad { get; set; }
        public string ColorUniforme { get; set; }
        public bool Disponibilidad { get; set; } = false;

        // Relación con Usuario
        public List<Usuario> Usuarios { get; set; } = new();//lista de usuarios
        // Relación muchos a muchos con Cancha
        public List<EquipoCancha> EquiposCanchas { get; set; } = new();

        //Relacion EquipoPartido
        public List<EquipoPartido> equipoPartidos { get; set; } = new();
    }
}
