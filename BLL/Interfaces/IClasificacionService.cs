using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Interfaces;
using DomainModel;

namespace BLL.Interfaces
{
    internal interface IClasificacionService
    {
        void Crear(Clasificacion clasificacion);

        void Actualizar(Clasificacion clasificacion);

        Clasificacion ObtenerClasificacion(Competicion competicion, Equipo equipo);
    }
}
