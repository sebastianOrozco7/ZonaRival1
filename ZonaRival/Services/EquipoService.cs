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

        public async Task<Equipo> ObtenerInfoEquipo(string Gmail) // se esta obteniendo a traves del gmail del usuario
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Equipo)
                    .ThenInclude(e => e.EquiposCanchas)
                    .ThenInclude(ec => ec.Cancha)
                .FirstOrDefaultAsync(u => u.Gmail == Gmail);
            return usuario?.Equipo;
        }
    }
}
