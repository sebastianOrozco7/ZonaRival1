using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ZonaRival.Data;
using ZonaRival.Models;
using ZonaRival.Models.ViewModels;


namespace ZonaRival.Services
{
    public class EquipoService
    {
        private readonly ZonaRivalContext _context;

        public EquipoService(ZonaRivalContext context)
        {
            _context = context;
        }

        public async Task<Equipo?> ObtenerInfoEquipo(string Gmail) // se esta obteniendo a traves del gmail del usuario
        {
            //este consulta trae el equipo al cual esta ligado el usuario y equipo trae las canchas a las cuales esta ligada
            var usuario = await _context.Usuarios
                .Include(u => u.Equipo)
                    .ThenInclude(e => e.EquiposCanchas)
                    .ThenInclude(ec => ec.Cancha)
                .FirstOrDefaultAsync(u => u.Gmail == Gmail); //filtra la lista de todos los usuarios y el usuario que coincida con el Gmail escrito en el Login lo trae 
            return usuario?.Equipo;
        }

        public async Task<bool> DisponibilidadEquipo(int EquipoId, bool NuevaDisponibilidad)
        {
            var equipo = await BuscarEquipo(EquipoId); 

            if(equipo == null)
                return false;// en caso de que no encuentre ningun equipo con ese Id entrara aca
            

            //si encuentra un equipo con ese ID continura aca y cambiara la disponibilidad
            equipo.Disponibilidad = NuevaDisponibilidad; 
            await _context.SaveChangesAsync();//aca estoy guardando los cambios en la base de datos 
            return true;
        }

        public async Task<List<Equipo>> ListaEquiposDisponibles()
        {
            return await _context.Equipos
                .Where(e => e.Disponibilidad == true) //filtra la consulta de acuerdo a su disponibilidad y solo trae los equipos con la disponibilidad en TRUE
                .Include(e => e.EquiposCanchas)
                    .ThenInclude(ec => ec.Cancha)
                .ToListAsync();
        }

        public async Task<List<Partido>> ListaDePartidosPendientes(int IdEquipo)
        {
            return await _context.Partidos
                   .Include(p => p.EquipoRetador)
                   .Include(p => p.EquipoDesafiado)
                   .Include(p => p.Cancha)
                   .Where(p => p.Estado == "Pendiente" &&
                              (p.EquipoDesafiadoId == IdEquipo || p.EquipoRetadorId == IdEquipo))
                   .ToListAsync();
        }
        public async Task<List<Partido>> ListaDePartidosConfirmados(int IdEquipo)
        {
            return await _context.Partidos
                .Include(p => p.EquipoRetador)
                    .ThenInclude(e => e.Usuarios)
                .Include(p => p.EquipoDesafiado)
                    .ThenInclude(e => e.Usuarios)
                .Include(p => p.Cancha)
                .Where(p => p.Estado == "Confirmado" &&
                              (p.EquipoDesafiadoId == IdEquipo || p.EquipoRetadorId == IdEquipo))
                   .ToListAsync();
        }

        public bool VerificarFechaPartido(Partido partido)
        {
            DateTime fechaHoraPartido = partido.Fecha.Date.Add(partido.Hora);
            return fechaHoraPartido < DateTime.Now;
        }

        public async Task CambioDeEstadoPartido()
        {
            //lleno la lista partidos con los partidos que tienen el estado en CONFIRMADO
            var partidos = await _context.Partidos
                .Where(p => p.Estado == "Confirmado")
                .ToListAsync();

            //verifico con el metodo y con un bucle cada uno de los partidos de la lista
            foreach(var P in partidos)
            {
                if (VerificarFechaPartido(P))
                    P.Estado = "Finalizado";
            }

            await _context.SaveChangesAsync();

        }

        public async Task<List<Partido>> Historial(int IdEquipo)
        {

            return await _context.Partidos
               .Include(p => p.EquipoRetador)
                   .ThenInclude(e => e.Usuarios)
               .Include(p => p.EquipoDesafiado)
                   .ThenInclude(e => e.Usuarios)
               .Include(p => p.Cancha)
               .Where(p => p.Estado == "Finalizado" &&
                             (p.EquipoDesafiadoId == IdEquipo || p.EquipoRetadorId == IdEquipo))
                  .ToListAsync();
        }

        //este metodo me permite buscar el equipo por el id para hacer consultas a la DB
        public  async Task<Equipo> BuscarEquipo(int EquipoId)
        {
            return await _context.Equipos
                .Include(e => e.EquiposCanchas)
                    .ThenInclude(ec => ec.Cancha)
                .FirstOrDefaultAsync(e => e.EquipoId == EquipoId); //busca el equipo por si ID y trae los datos tambien de la relacion con canchas
        }

        public async Task<bool> EditarEquipo(int equipoId, string Nombre, int CantidadJugadores, string RangoEdad, string ColorUniforme)
        {
            var equipo = await _context.Equipos.FindAsync(equipoId);

            if (equipo == null)
                return false;

            //asignamos los nuevos valores actualizados
            equipo.NombreEquipo = Nombre;
            equipo.CantidadJugadores = CantidadJugadores;
            equipo.RangoEdad = RangoEdad;
            equipo.ColorUniforme = ColorUniforme;

            //guardamos los cambios en la base de datos
            await _context.SaveChangesAsync();
            return true;
        }       

        public async Task<Partido> DesafiarRival(Partido partido)
        {
            _context.Partidos.Add(partido);
            await _context.SaveChangesAsync();

            return partido;
        } 

        public async Task<Partido> AceptarDesafio(int IdPartido)
        {
            var Partido = await _context.Partidos.FindAsync(IdPartido);

            if (Partido == null)
                return null;

            //cambio el estado del partido a Confirmado
            Partido.Estado = "Confirmado";
            await _context.SaveChangesAsync();

            return Partido;
        }

        public async Task RechazarDesafio(int IdPartido)
        {
            var partido = await _context.Partidos.FindAsync(IdPartido); //consulta para buscar el partido por medio del id

            _context.Partidos.Remove(partido); //elimina el partido
            await _context.SaveChangesAsync();

        }
    }
}
