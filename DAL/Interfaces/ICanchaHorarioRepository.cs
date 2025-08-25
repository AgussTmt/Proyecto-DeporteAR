using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Interfaces;
using DomainModel;

namespace DAL.Interfaces
{
    internal interface ICanchaHorarioRepository : IGenericRepository <CanchaHorario>
    {
        List<CanchaHorario> GetByTimeRange(DateTime dateTime);

        List<CanchaHorario> GetByCliente(Cliente cliente);

        void AssignCliente(Cliente cliente, CanchaHorario canchaHorario);

        List<CanchaHorario> GetOrderByDemand();

        List<CanchaHorario> GetByEstadoReserva(EstadoReserva estadoReserva);
    }
}
