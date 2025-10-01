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


        [HttpGet]
        public async Task<IActionResult> Index() // este metodo maneja el apartado donde se muestra la info del equipo
        {
            var Gmail = HttpContext.Session.GetString("Gmail"); // le estoy asignando a la variable Gmail el Gmail que el usuario digito en el Login

            // verifica si es null, osea si hay un usuario autenticado
            if (string.IsNullOrEmpty(Gmail))
            {
                return RedirectToAction("Login", "Inicio"); //si es null entonces vuelve y redirecciona a el Login
            }

            // Obtener el equipo usando el servicio
            var equipo = await _EquipoService.ObtenerInfoEquipo(Gmail);

            return View("~/Views/Home/Index.cshtml", equipo); // le paso la vista y el objeto que debe utilizar para mostrar los datos
        }

        [HttpPost]
        public async Task<IActionResult> DisponibilidadEquipo(int EquipoId)
        {
            var CambioDisponibiliadad = await _EquipoService.ActualizarDisponibilidadEquipo(EquipoId);

            if (CambioDisponibiliadad)
            {
                return RedirectToAction("~/Views/Home/Index.cshtml");
            }
            else
            {
                ViewBag.Error = "No se pudo actualizar la disponibilidad.";
                return View("~/Views/Home/Index.cshtml");
            }
        }
    }
}
