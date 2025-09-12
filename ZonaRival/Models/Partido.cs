using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ZonaRival.Models.ViewModels;

namespace ZonaRival.Models
{
    public class Partido
    {
        public int PartidoId { get; set; }
        public string Modalidad {  get; set; }
        public string Estado {  get; set; }
        public DateTime Fecha {  get; set; }

        //relacion con cancha
        public int CanchaId {  get; set; }
        public Cancha Cancha { get; set; }

        //relacion con Equipo
        public List<EquipoPartido> equipoPartidos { get; set; } = new();

        

    }
}
