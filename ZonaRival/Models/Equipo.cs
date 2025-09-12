using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ZonaRival.Models
{
    public class Equipo
    {
        public int EquipoId { get; set; }
        public string NombreEquipo { get; set; }
        public int CantidadJugadores { get; set; }
        public string RangoEdad { get; set; }
        public string ColorUniforme { get; set; }

        // Relación con Usuario
        public List<Usuario> Usuarios { get; set; } = new();//lista de usuarios
        // Relación muchos a muchos con Cancha
        public List<EquipoCancha> EquiposCanchas { get; set; } = new();

        //Relacion con historial
        public List<Historial> historiales { get; set; } = new();
    }
}
