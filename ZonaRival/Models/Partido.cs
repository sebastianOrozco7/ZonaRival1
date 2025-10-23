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
        public TimeSpan Hora { get; set; }

        //Equipo retador
        public int EquipoRetadorId {  get; set; }
        public Equipo EquipoRetador {  get; set; }

        //Equipo Desafiado
        public int EquipoDesafiadoId {  get; set; }
        public Equipo EquipoDesafiado { get; set; }

        //relacion con cancha
        public int CanchaId {  get; set; }
        public Cancha Cancha { get; set; }  

    }
}
