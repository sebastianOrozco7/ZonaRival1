using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ZonaRival.Models
{
    public class Cancha
    {
        public int CanchaId { get; set; }
        public string NombreCancha { get; set; } = string.Empty;

        // Relación muchos a muchos con Equipo
        [ValidateNever]
        public List<EquipoCancha> EquiposCanchas { get; set; } = new List<EquipoCancha>();

        //relacion con partido
        public List<Partido> partidos { get; set; } = new List<Partido>();
    }
}
