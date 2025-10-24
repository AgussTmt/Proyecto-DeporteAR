using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Competicion
    {
        public Guid IdCompeticion { get; set; }

        public Cancha canchaAsignada { get; set; }

        public int Cupos { get; set; }

        public int CuposMinimos { get; set; }

        public string Deporte { get; set; }

        public List<Equipo> ListaEquipos{ get; set; }

        public EstadoCompeticion  Estado{ get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaInicio { get; set; }

        public FormatoEnum Formato { get; set; }

        public string FranjaHoraria { get; set; }

        public int Frecuencia { get; set; }

        public string Nombre { get; set; }

        public decimal Precio { get; set; }
    }

    public enum EstadoCompeticion
    {
        SinFixture,
        ConFixture,
        Activo,
        Deshabilitado
    }

    public enum FormatoEnum
    {
        Liga,
        Torneo
    }
}
