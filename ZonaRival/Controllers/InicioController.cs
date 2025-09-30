using Microsoft.AspNetCore.Http;
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

        [HttpGet]
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
                return RedirectToAction("login", "Inicio");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuario usuario)
        {
            var UsuarioDB = _inicioService.VerificacionInicioSesion(usuario.Gmail, usuario.Contraseña);

            if (UsuarioDB != null)
            {
                HttpContext.Session.SetString("Gmail", UsuarioDB.Gmail); //guarda el Gmail del usuario autenticado en la sesión, que es como una "memoria temporal" del servidor que identifica al usuario mientras navega.
                return RedirectToAction("Index", "Equipo");

            }
            
            ViewBag.Error = "Correo o Contraseña incorrectos";

            return View();
        }

    }
    
}
