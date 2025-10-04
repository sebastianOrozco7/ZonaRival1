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
                //estoy migrando los valores a objetos EquipoViewModel que son los que la vista admite 
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
                var equipo = await _EquipoService.BuscarEquipo(EquipoId);               //ERROR, Solo me trae los elementos de equipo mas no me incluye las canchas
                var ListaModelView = new EquipoViewModel
                {
                    //estoy migrando los valores a objetos EquipoViewModel que son los que la vista admite 
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

        [HttpPost]
        public async Task<IActionResult> EditarEquipo(int equipoId, string nombreEquipo, int cantidadJugadores, string RangoEdad, string ColorUniforme)
        {
            var exito = await _EquipoService.EditarEquipo(equipoId, nombreEquipo, cantidadJugadores, RangoEdad, ColorUniforme);

            if (exito)
            {
                var equipos = await _EquipoService.ListaEquiposDisponibles();
                var equipo = await _EquipoService.BuscarEquipo(equipoId); 
                var model = new EquipoViewModel
                {
                    //estoy migrando los valores a objetos EquipoViewModel que son los que la vista admite 
                    ListaEquipos = equipos,
                    equipoViewModel = equipo
                };
                return View("~/Views/Home/Panel.cshtml", model);
            }
            else
            {
                ViewBag.Error = "No se pudo editar el equipo.";
                return View("~/Views/Home/Panel.cshtml");
            }
        }

    }
}
