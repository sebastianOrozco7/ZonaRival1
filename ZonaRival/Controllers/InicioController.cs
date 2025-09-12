using Microsoft.AspNetCore.Mvc;
using ZonaRival.Data;
using ZonaRival.Models;
using ZonaRival.Models.ViewModels;
using ZonaRival.Services;

namespace ZonaRival.Controllers
{
    public class InicioController : Controller
    {
        private readonly InicioService _inicioService;

        public InicioController(InicioService inicioService)
        {
            _inicioService = inicioService;
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registro(RegistroViewModel model)
        {
          
                _inicioService.RegistrarEquipo(model.equipo);

                model.usuario.IdEquipo = model.equipo.EquipoId; // para que primero registre equipo y le asigne un id de equipo a el usuario
                _inicioService.RegistrarUsuario(model.usuario);

                foreach (var cancha in model.canchas)
                {
                    // 1. Registrar la cancha si es nueva
                    _inicioService.RegistrarCancha(cancha);

                    // 2. Crear la relación Equipo-Cancha
                    var equipoCancha = new EquipoCancha
                    {
                        EquipoId = model.equipo.EquipoId,
                        CanchaId = cancha.CanchaId
                    };
                    _inicioService.RegistrarEquipoCancha(equipoCancha);
                }
                return RedirectToAction("index", "Home");
            
        }

    }
    
}
