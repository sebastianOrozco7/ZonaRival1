namespace ZonaRival.Models
{
    public class Cancha
    {
        public int CanchaId { get; set; }
        public string NombreCancha { get; set; }

        // Relación muchos a muchos con Equipo
        public List<EquipoCancha> EquiposCanchas { get; set; }

        //relacion muchos a muchos con partido
        public List<Partido> partidos { get; set; }
    }
}
