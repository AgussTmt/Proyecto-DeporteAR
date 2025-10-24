using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace DAL.Interfaces
{
    public interface IClasificacionRepository
    {
        void Update(Clasificacion clasificacion);

        Clasificacion GetByCompeticionEquipo(Competicion competicion, Equipo equipo);

        List<Clasificacion> GetByCompeticion(Guid idCompeticion);

        void Add(Clasificacion clasificacion);
        void Delete(Guid idClasificacion);
    }
}
