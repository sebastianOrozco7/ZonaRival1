using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Macs;
using ZonaRival.Data;
using ZonaRival.Models;
using ZonaRival.Services;



namespace ZonaRival.Controllers
{
    public class EquipoController : Controller
    {
        private readonly EquipoService _EquipoService;

        public EquipoController(EquipoService equipoService)
        {
            _EquipoService = equipoService;
        }

        public async Task<IActionResult> InformacionEquipo()
        {
            var Gmail = HttpContext.Session.GetString("Gmail");

            // verifica si es null, osea si hay un usuario autenticado
            if (string.IsNullOrEmpty(Gmail))
            {
                return RedirectToAction("Login", "Inicio");
            }

            // Obtener el equipo usando el servicio
            var equipo = await _EquipoService.ObtenerInfoEquipo(Gmail);

            return View("index", equipo);
        }
    }
}
