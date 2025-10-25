using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Interfaces;
using DomainModel;

namespace DAL.Interfaces
{
    public interface ICanchaHorarioRepository : IGenericRepository <CanchaHorario>
    {
        List<CanchaHorario> GetByTimeRange(DateTime dateTime);

        List<CanchaHorario> GetByCliente(Cliente cliente);

        void AssignCliente(Cliente cliente, CanchaHorario canchaHorario);

        List<CanchaHorario> GetOrderByDemand();

        List<CanchaHorario> GetByEstadoReserva(EstadoReserva estadoReserva);

        CanchaHorario GetByCanchaYHora(Guid idCancha, DateTime hora);

        /// <summary>
        /// Obtiene la fecha y hora MÁXIMA de un CanchaHorario existente para una cancha específica.
        /// Devuelve DateTime.MinValue si no existen horarios para esa cancha.
        /// </summary>
        DateTime GetMaximaFechaHorario(Guid idCancha);


        /// <summary>
        /// Verifica si ya existe un CanchaHorario para una cancha y fecha/hora exactas.
        /// </summary>
        bool ExisteHorario(Guid idCancha, DateTime fechaHora);
    }
}
