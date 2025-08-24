using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Interfaces;
using DomainModel;

namespace BLL.Interfaces
{
    internal interface ICompeticionService : IGenericService <Competicion>
    {
        void CrearFixture(Competicion competicion);

        List<Competicion> ListarConVacantes(Competicion competicion);

        List<Competicion> ListarPorCliente(Cliente cliente);

        void QuitarEquipo(Competicion competicion, Equipo equipo);

        void AñadirEquipo(Competicion competicion, Equipo equipo);
    }
}
