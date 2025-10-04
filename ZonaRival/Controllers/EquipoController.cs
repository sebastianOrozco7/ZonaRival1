using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Macs;
using ZonaRival.Data;
using ZonaRival.Models;
using ZonaRival.Models.ViewModels;
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
        public IActionResult Panel()
        {
            return View("~/Views/Home/Panel.cshtml");
        }

        [HttpGet]

        public async Task<IActionResult> InformacionEquipo() // este metodo maneja el apartado donde se muestra la info del equipo
        {
            var Gmail = HttpContext.Session.GetString("Gmail"); // le estoy asignando a la variable Gmail el Gmail que el usuario digito en el Login

            // verifica si es null, osea si hay un usuario autenticado
            if (string.IsNullOrEmpty(Gmail))
            {
                return RedirectToAction("Login", "Inicio"); //si es null entonces vuelve y redirecciona a el Login
            }

            // Obtener el equipo usando el servicio
            var equipo = await _EquipoService.ObtenerInfoEquipo(Gmail);
            var EquipoModelView = new EquipoViewModel
            {
                equipoViewModel = equipo,
                ListaEquipos = new List<Equipo>()
            };

            return View("~/Views/Home/Panel.cshtml", EquipoModelView); // le paso la vista y el objeto que debe utilizar para mostrar los datos
        }

        [HttpPost]
        public async Task<IActionResult> ToggleDisponibilidad(int EquipoId)
        {
            var equipoActual = await _EquipoService.BuscarEquipo(EquipoId);
            var nuevaDisponibilidad = equipoActual?.Disponibilidad ?? false; // Si es null, usa false como fallback
            nuevaDisponibilidad = !nuevaDisponibilidad; // Alterna el estado

            var CambioDisponibilidad = await _EquipoService.DisponibilidadEquipo(EquipoId, nuevaDisponibilidad);

            if (CambioDisponibilidad)
            {
                var equipos = await _EquipoService.ListaEquiposDisponibles();
                var equipo = await _EquipoService.BuscarEquipo(EquipoId);
                var ListaModelView = new EquipoViewModel
                {
                    ListaEquipos = equipos,
                    equipoViewModel = equipo
                };
                return View("~/Views/Home/Panel.cshtml", ListaModelView);
            }
            else
            {
                ViewBag.Error = "No se pudo actualizar la disponibilidad.";
                return View("~/Views/Home/Panel.cshtml");
            }
        }

    }
}
