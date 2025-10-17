namespace ZonaRival.Models.ViewModels
{
    public class EquipoViewModel
    {
        public Equipo equipoViewModel { get; set; } = new();
        public Partido partidoViewModel { get; set; } = new();
        public List<Equipo> ListaEquipos { get; set; } = new();
        public List<Cancha> ListaCanchas {  get; set; } = new();
        public List<Partido> ListaPartidos {  get; set; } = new();

    }
}
