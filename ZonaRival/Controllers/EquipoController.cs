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
        private readonly InicioService _InicioService;

        public EquipoController(EquipoService equipoService, InicioService inicioService)
        {
            _EquipoService = equipoService;
            _InicioService = inicioService;
        }

        [HttpGet]
        public IActionResult Panel()
        {
            return View();
        }

        //este metodo simplifica el codigo para que en cada clase no tenga que escribir esto mismo. solo tengo que llamar este metodo en los demas metodos y pasarle los parametros 
        public EquipoViewModel EnviarViewModelCompleto
            (Equipo equipo,
            List<Cancha> listaCanchas,
            List<Equipo> listaEquipos,
            List<Partido> listaPartidosPendientes,
            List<Partido> listaPartidosConfirmados,
            List<Partido> listaHistorial)
        {
            EquipoViewModel Model = new EquipoViewModel
            {
                equipoViewModel = equipo,
                ListaCanchas = listaCanchas,
                ListaEquipos = listaEquipos,
                ListaEncuentrosPendientes = listaPartidosPendientes,
                ListaEncuentrosConfirmados = listaPartidosConfirmados,
                ListaEncuentrosFinalizados = listaHistorial
            };

            return Model;
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

            await _EquipoService.CambioDeEstadoPartido(); // este metodo cabia el estado de los partidos que ya se jugaron, debe ir primero que todo para obtener bien los valores de las listas

            // Obtener el equipo usando el servicio
            var equipo = await _EquipoService.ObtenerInfoEquipo(Gmail);
            var canchas = _InicioService.ObtenerCanchasRegistradas(); //este metodo lo llamo para que en el apartado de desafio me muestre las canchas disponibles
            var equipos = await _EquipoService.ListaEquiposDisponibles();
            var partidosPendientes = await _EquipoService.ListaDePartidosPendientes(equipo.EquipoId);
            var partidosConfirmados = await _EquipoService.ListaDePartidosConfirmados(equipo.EquipoId);
            var Historial = await _EquipoService.Historial(equipo.EquipoId);
            var Model = EnviarViewModelCompleto(equipo, canchas, equipos, partidosPendientes, partidosConfirmados,Historial);

            return View("Panel", Model); // le paso la vista y el objeto que debe utilizar para mostrar los datos
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
                await _EquipoService.CambioDeEstadoPartido(); // este metodo cabia el estado de los partidos que ya se jugaron, debe ir primero que todo para obtener bien los valores de las listas

                var equipos = await _EquipoService.ListaEquiposDisponibles();
                var equipo = await _EquipoService.BuscarEquipo(EquipoId);
                var canchas = _InicioService.ObtenerCanchasRegistradas();
                var PartidosPendientes = await _EquipoService.ListaDePartidosPendientes(EquipoId);
                var partidosConfirmados = await _EquipoService.ListaDePartidosConfirmados(equipo.EquipoId);
                var Historial = await _EquipoService.Historial(equipo.EquipoId);
                var model = EnviarViewModelCompleto(equipo, canchas, equipos, PartidosPendientes, partidosConfirmados, Historial);


                return View("Panel", model);
            }
            else
            {
                ViewBag.Error = "No se pudo actualizar la disponibilidad.";
                return View("Panel");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditarEquipo(int equipoId, string nombreEquipo, int cantidadJugadores, string RangoEdad, string ColorUniforme)
        {
            var exito = await _EquipoService.EditarEquipo(equipoId, nombreEquipo, cantidadJugadores, RangoEdad, ColorUniforme);

            if (exito)
            {
                await _EquipoService.CambioDeEstadoPartido(); // este metodo cabia el estado de los partidos que ya se jugaron, debe ir primero que todo para obtener bien los valores de las listas

                var equipos = await _EquipoService.ListaEquiposDisponibles();
                var equipo = await _EquipoService.BuscarEquipo(equipoId);
                var canchas = _InicioService.ObtenerCanchasRegistradas();
                var PartidosPendientes = await _EquipoService.ListaDePartidosPendientes(equipoId);
                var partidosConfirmados = await _EquipoService.ListaDePartidosConfirmados(equipo.EquipoId);
                var Historial = await _EquipoService.Historial(equipo.EquipoId);
                var model = EnviarViewModelCompleto(equipo, canchas, equipos, PartidosPendientes, partidosConfirmados, Historial);

                return View("Panel", model);
            }
            else
            {
                ViewBag.Error = "No se pudo editar el equipo.";
                return View("Panel");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DesafiarEquipo(string modalidad, DateTime fecha, TimeSpan hora, int equipoRetadorId, int equipoDesafiadoId, int canchaId)
        {

            //llenando los datos del objeto con los datos que envia el cliente
            Partido partido = new Partido
            {
                Modalidad = modalidad,
                Fecha = fecha,
                Hora = hora,
                EquipoRetadorId = equipoRetadorId,
                EquipoDesafiadoId = equipoDesafiadoId,
                CanchaId = canchaId,
                Estado = "Pendiente"
            };

            //se crea el desafio
            await _EquipoService.DesafiarRival(partido);

            await _EquipoService.CambioDeEstadoPartido(); // este metodo cabia el estado de los partidos que ya se jugaron, debe ir primero que todo para obtener bien los valores de las listas

            var equipos = await _EquipoService.ListaEquiposDisponibles();
            var equipo = await _EquipoService.BuscarEquipo(equipoRetadorId);
            var canchas = _InicioService.ObtenerCanchasRegistradas();
            var PartidosPendientes = await _EquipoService.ListaDePartidosPendientes(equipo.EquipoId);
            var partidosConfirmados = await _EquipoService.ListaDePartidosConfirmados(equipo.EquipoId);
            var Historial = await _EquipoService.Historial(equipo.EquipoId);
            var model = EnviarViewModelCompleto(equipo, canchas, equipos, PartidosPendientes, partidosConfirmados, Historial);

           

            return View("Panel", model);
        }

        [HttpPost]
        public async Task<IActionResult> AceptarDesafio(int IdPartido)
        {

            var Partido = await _EquipoService.AceptarDesafio(IdPartido);

            await _EquipoService.CambioDeEstadoPartido(); // este metodo cabia el estado de los partidos que ya se jugaron, debe ir primero que todo para obtener bien los valores de las listas

            var equipos = await _EquipoService.ListaEquiposDisponibles();
            var equipo = await _EquipoService.BuscarEquipo(Partido.EquipoDesafiadoId);
            var canchas = _InicioService.ObtenerCanchasRegistradas();
            var PartidosPendientes = await _EquipoService.ListaDePartidosPendientes(Partido.EquipoDesafiadoId);
            var partidosConfirmados = await _EquipoService.ListaDePartidosConfirmados(equipo.EquipoId);
            var Historial = await _EquipoService.Historial(equipo.EquipoId);
            var model = EnviarViewModelCompleto(equipo, canchas, equipos, PartidosPendientes, partidosConfirmados, Historial);

            return View("Panel", model);
        }

        public async Task<IActionResult> RechazarDesafio(int IdPartido, int IdEquipoDesafiado)
        {
            await _EquipoService.RechazarDesafio(IdPartido);

            await _EquipoService.CambioDeEstadoPartido(); // este metodo cabia el estado de los partidos que ya se jugaron, debe ir primero que todo para obtener bien los valores de las listas

            var equipos = await _EquipoService.ListaEquiposDisponibles();
            var equipo = await _EquipoService.BuscarEquipo(IdEquipoDesafiado);
            var canchas = _InicioService.ObtenerCanchasRegistradas();
            var PartidosPendientes = await _EquipoService.ListaDePartidosPendientes(IdEquipoDesafiado);
            var partidosConfirmados = await _EquipoService.ListaDePartidosConfirmados(IdEquipoDesafiado);
            var Historial = await _EquipoService.Historial(equipo.EquipoId);
            var model = EnviarViewModelCompleto(equipo, canchas, equipos, PartidosPendientes, partidosConfirmados, Historial);

            return View("panel", model);
        }

        

    }
}


