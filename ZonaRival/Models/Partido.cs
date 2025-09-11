namespace ZonaRival.Models
{
    public class Partido
    {
        public int PartidoId { get; set; }
        public string Modalidad {  get; set; }
        public string Estado {  get; set; }
        public DateTime Fecha {  get; set; }

        //relacion con cancha
        public int CanchaId {  get; set; }
        public Cancha Cancha { get; set; }

        //relacion con Equipo
        public int EquipoId1 { get; set; }
        public Equipo equipo1 { get; set; }
        public int EquipoId2 { get; set; }
        public Equipo equipo2 { get; set; }

        //Relacion con historial
        public List<Historial> historiales { get; set; }






    }
}
