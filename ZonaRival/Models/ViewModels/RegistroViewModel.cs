namespace ZonaRival.Models.ViewModels
{
    public class RegistroViewModel
    {
        public Equipo equipo { get; set; } = new();
        public Usuario usuario { get; set; } = new();
  
        public List<Cancha> canchas { get; set; } = new();

        //Esta lista guardará los Ids seleccionados en los <select>
        public List<int> CanchasSeleccionadas { get; set; } = new List<int> { 0, 0, 0 };
    }
}
