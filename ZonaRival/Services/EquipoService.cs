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

        public async Task<bool> HabilitarDisponibilidadEquipo(int EquipoId)
        {
            var equipo = await _context.Equipos.FindAsync(EquipoId); // con FindAsync estoy buscando el equipo con la condicion de su Id

            if(equipo == null)
            {
                return false;// en caso de que no encuentre ningun equipo con ese Id entrara aca
            }

            //si no continura aca y cambiara la disponibilidad
            equipo.Disponibilidad = true; 
            await _context.SaveChangesAsync();//aca estoy guardando los cambios en la base de datos 
            return true;
        }

        public async Task<List<Equipo>> ListaEquiposDisponibles()
        {
            return await _context.Equipos
                .Where(e => e.Disponibilidad == true) //filtra la consulta de acuerdo a su disponibilidad y solo trae los equipos con la disponibilidad en TRUE
                .ToListAsync();
        }

        public  async Task<Equipo> BuscarEquipo(int EquipoId)
        {
            return await _context.Equipos.FirstOrDefaultAsync(e => e.EquipoId == EquipoId);

            //este metodo se encarga de buscar un equipo por medio del Id, y me devuelve el equipo si lo encontro y null si no
        }

    }
}
