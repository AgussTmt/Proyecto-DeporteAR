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

        List<CanchaHorario> GetOrderByDemand();

        List<CanchaHorario> GetByEstadoReserva(EstadoReserva estadoReserva);

        CanchaHorario GetByCanchaYHora(Guid idCancha, DateTime hora);

        DateTime GetMaximaFechaHorario(Guid idCancha);
        bool ExisteHorario(Guid idCancha, DateTime fechaHora);

        IEnumerable<CanchaHorario> GetHorariosRango(Guid idCancha, DateTime fechaDesde, DateTime fechaHasta);
    }
}
