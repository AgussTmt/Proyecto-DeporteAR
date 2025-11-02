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


        List<CanchaHorario> ListarPorDemanda();

        List<CanchaHorario> ListarPorEstadoReserva(EstadoReserva estado);


        DateTime GetMaximaFechaHorario(Guid idCancha);
        bool ExisteHorario(Guid idCancha, DateTime fechaHora);

        IEnumerable<CanchaHorario> GetHorariosRango(Guid idCancha, DateTime fechaDesde, DateTime fechaHasta);

        void ActualizarReserva(Guid idCanchaHorario, EstadoReserva nuevoEstado, Cliente cliente, bool abonada);

        int GenerarHorariosParaCancha(Guid idCancha, int diasHorizonte);

    }
}
