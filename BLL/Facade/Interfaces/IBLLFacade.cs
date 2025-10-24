using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;

namespace BLL.Facade.Interfaces
{
    public interface IBLLFacade
    {
        ICanchaService CanchaService { get; }
        ICanchaHorarioService CanchaHorarioService { get; }
        IClienteService ClienteService { get; }
        ICompeticionService CompeticionService { get; }
        IEquipoService EquipoService { get; }
        IFixtureService FixtureService { get; }
        IJugadorService JugadorService { get; }
        IClasificacionService ClasificacionService { get; }
    }
}
