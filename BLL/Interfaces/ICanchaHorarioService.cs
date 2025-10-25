using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Interfaces;
using DomainModel;

namespace BLL.Interfaces
{
    public interface ICanchaHorarioService
    {
        void Crear(CanchaHorario canchaHorario);
        

        List<CanchaHorario> ListarPorRangoTiempo(DateTime date);

       

        void AsignarCliente(Cliente cliente, CanchaHorario canchaHorario);

        void AsignarEstado(CanchaHorario canchaHorario, EstadoReserva estado);

        List<CanchaHorario> ListarPorCliente(Cliente cliente);

        List<CanchaHorario> ListarPorDemanda();

        List<CanchaHorario> ListarPorEstadoReserva(EstadoReserva estado);

        public void AbonarReserva(Guid idCanchaHorario);

    }
}
