namespace ZonaRival.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Gmail { get; set; }
        public string Telefono { get; set; }
        public string Contraseña { get; set; }

        // Relación con Equipo
        public int IdEquipo { get; set; }//FK
        public Equipo Equipo { get; set; }
    }
}
