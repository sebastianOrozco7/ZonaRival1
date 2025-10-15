using Microsoft.EntityFrameworkCore;
using ZonaRival.Data;
using ZonaRival.Models;


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

            if (equipoId == null)
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

    }
}
