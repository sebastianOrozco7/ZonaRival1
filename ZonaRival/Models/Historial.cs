namespace ZonaRival.Models
{
    public class Historial
    {
        public int HistorialId { get; set; }

        //Relacion con equipo
        public int EquipoId {  get; set; }
        public Equipo equipo { get; set; }

        //Relacion con partido
        public int PartidoId { get; set; }
        public Partido partido { get; set; }
        public DateTime FechaDeRegistro { get; set; }
    }
}
