namespace ZonaRival.Models.ViewModels
{
    public class RegistroViewModel
    {
        public Equipo equipo { get; set; } = new();
        public Usuario usuario { get; set; } = new();
        public Cancha cancha { get; set; } = new();
        public List<Cancha> canchas { get; set; } = new();
    }
}
