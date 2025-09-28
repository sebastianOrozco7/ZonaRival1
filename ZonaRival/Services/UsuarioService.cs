using ZonaRival.Data;

namespace ZonaRival.Services
{
    public class UsuarioService
    {
        private readonly ZonaRivalContext _context;

        public UsuarioService(ZonaRivalContext context)
        {
            _context = context;
        }
    }
}
