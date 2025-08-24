using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DomainModel;

namespace BLL.Services
{
    internal class CanchaHorarioService : ICanchaHorarioService
    {
        public void AsignarCliente(Cliente cliente, CanchaHorario canchaHorario)
        {
            throw new NotImplementedException();
        }

        public void AsignarEstado(CanchaHorario canchaHorario, EstadoReserva estado)
        {
            throw new NotImplementedException();
        }

        public void Crear(CanchaHorario canchaHorario)
        {
            throw new NotImplementedException();
        }

        public List<CanchaHorario> ListarPorCliente(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public List<CanchaHorario> ListarPorDemanda()
        {
            throw new NotImplementedException();
        }

        public List<CanchaHorario> ListarPorEstadoReserva(EstadoReserva estado)
        {
            throw new NotImplementedException();
        }

        public List<CanchaHorario> ListarPorRangoTiempo(DateTime date)
        {
            throw new NotImplementedException();
        }

        public bool VerificarDisponible(CanchaHorario canchaHorario)
        {
            throw new NotImplementedException();
        }

        public bool VerificarTiempoRestante(CanchaHorario canchaHorario)
        {
            throw new NotImplementedException();
        }
    }
}
