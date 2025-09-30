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
        
    }
}
