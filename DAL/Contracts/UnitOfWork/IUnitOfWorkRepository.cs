using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace Patrones_3parcial.UnitOfWork
{
    public interface IUnitOfWorkRepository
    {
        // Repositorios de Raíces de Agregado
        ICanchaRepository CanchaRepository { get; }
        IClienteRepository ClienteRepository { get; }
        ICompeticionRepository CompeticionRepository { get; }
        IFixtureRepository FixtureRepository { get; }
        IClasificacionRepository ClasificacionRepository { get; }
        IEquipoRepository EquipoRepository { get; }
        IJugadorRepository JugadorRepository { get; }         
        ICanchaHorarioRepository CanchaHorarioRepository { get; }
        ICatalogRepository CatalogRepository { get; }

    }
}
