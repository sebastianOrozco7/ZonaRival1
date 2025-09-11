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
        public IActionResult Registrar(RegistroViewModel model)
        {
            if(ModelState.IsValid)
            {
                _inicioService.RegistrarEquipo(model.equipo);

                model.equipo.EquipoId = model.usuario.IdEquipo; // para que primero registre equipo y le asigne un id de equipo a el usuario
                _inicioService.RegistrarUsuario(model.usuario);

                _inicioService.RegistrarCanchasPrefencia(model.cancha);
                return RedirectToAction("index", "Home");
            }
            return View(model);
        }

        


    }
}
