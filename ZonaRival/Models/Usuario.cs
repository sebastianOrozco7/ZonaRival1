using System.ComponentModel.DataAnnotations;

namespace ZonaRival.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Gmail { get; set; } = string.Empty;
        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        public string Telefono { get; set; } = string.Empty;
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        public string Contraseña { get; set; } = string.Empty;

        // Relación con Equipo
        public int IdEquipo { get; set; }//FK
        public Equipo Equipo { get; set; }
    }
}
