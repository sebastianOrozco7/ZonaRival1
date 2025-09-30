namespace ZonaRival.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Gmail { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Contraseña { get; set; } = string.Empty;

        // Relación con Equipo
        public int IdEquipo { get; set; }//FK
        public Equipo Equipo { get; set; }
    }
}
