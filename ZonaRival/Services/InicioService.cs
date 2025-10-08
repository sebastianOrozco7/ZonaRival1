using ZonaRival.Controllers;
using ZonaRival.Data;
using ZonaRival.Models;

namespace ZonaRival.Services
{
    public class InicioService
    {
        private readonly ZonaRivalContext _context;

        public InicioService(ZonaRivalContext context)
        {
            _context = context;
        }

        //registrar Usuario
        public void RegistrarUsuario(Usuario usuario)
        {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
        }

        //metodo para verificar si existe un usuario con un Gmail registrado
        public bool VerificarGmail(string GmailUsuario)
        {
            return  _context.Usuarios // le estoy pasando la lista de usuarios para que pueda verificar
               .Any(u => u.Gmail == GmailUsuario); //con any me verifica si existe y me devuelve true o false
        }

      
        //registrar Equipo
        public void RegistrarEquipo(Equipo equipo)
        {
            _context.Equipos.Add(equipo);
            _context.SaveChanges();
        }

        //registrar canchas de preferencia
        public void RegistrarCancha(Cancha cancha)
        {
            
            _context.Canchas.Add(cancha);
            _context.SaveChanges();
        }

        public void RegistrarEquipoCancha(EquipoCancha equipoCancha)
        {
            _context.EquiposCanchas.Add(equipoCancha);
            _context.SaveChanges();
        }

        public Usuario? VerificacionInicioSesion(string Email, string Contraseña)
        {
           
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Gmail == Email && u.Contraseña == Contraseña);

            return usuario;
        }

        public List<Cancha> ObtenerCanchasRegistradas()
        {
            return _context.Canchas.ToList();
        }
        
    }
}
