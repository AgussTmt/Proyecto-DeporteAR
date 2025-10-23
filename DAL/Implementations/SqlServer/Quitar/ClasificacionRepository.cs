using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DomainModel;

namespace DAL.Implementations.SqlServer
{
    internal class ClasificacionRepository : IClasificacionRepository
    {
        public Clasificacion GetByCompeticionEquipo(Competicion competicion, Equipo equipo)
        {
            throw new NotImplementedException();
        }

        public void Update(Clasificacion clasificacion)
        {
            throw new NotImplementedException();
        }
    }
}
