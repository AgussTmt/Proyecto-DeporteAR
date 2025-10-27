using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Interfaces;
using DomainModel;

namespace BLL.Interfaces
{
    public interface IEquipoService
    {
        void AñadirAusencia(Equipo equipo);

        void Crear(Equipo equipo);

        List<Equipo> ListarPorCompeticion(Competicion competicion);

        Equipo TraerPorId(Equipo equipo);

        void CambiarEstadoAsistencia(EstadoAsistencia estadoAsistencia, Equipo equipo);

        void AñadirMiembro(Jugador jugador);
        IEnumerable<Equipo> GetAll();

        void CambiarHabilitado(Guid idEquipo, bool habilitado);

        IEnumerable<Equipo> GetAllIncludingDisabled();
    }
}
