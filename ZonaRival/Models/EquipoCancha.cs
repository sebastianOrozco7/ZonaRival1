namespace ZonaRival.Models
{
    public class EquipoCancha
    {
        public int EquipoId { get; set; }
        public int CanchaId { get; set; }

        // Relaciones
        public Equipo Equipo { get; set; }
        public Cancha Cancha { get; set; }
    }
}
